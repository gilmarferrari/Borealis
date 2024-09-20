using System.Linq;
using TMPro;
using UnityEngine;

public class MapGenerationController : MonoBehaviour
{
    private float _platformGenerationCooldown = 0;
    private readonly float[] yPositions = new float[] { -3.2f, -1.9f, -0.6f };
    private float _speed;
    private GameObject _player;
    private Platform _previousPlatform;
    public GameObject MediumPlatformPrefab;
    public GameObject LargePlatformPrefab;
    public TextMeshProUGUI PlatformSpeedLabel;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _speed = GetPlatformSpeed();
    }

    void FixedUpdate()
    {
        _platformGenerationCooldown -= Time.unscaledDeltaTime;
        PlatformSpeedLabel.text = $"Speed: {_speed * 100} m/s";

        if (_platformGenerationCooldown <= 0)
        {
            GeneratePlatform();
        }
    }

    void GeneratePlatform()
    {
        _platformGenerationCooldown = GameSettings.PlatformGenerationCooldown / GameSettings.PlatformGenerationSpeedFactor;

        var random = 0;

        do
        {
            random = Random.Range(0, yPositions.Count());
        }
        while (yPositions[random] == _previousPlatform?.Position.y && GameSettings.PlatformGenerationSpeedFactor != _previousPlatform?.Speed);

        var initialPosition = new Vector3(30, yPositions[random], 0);
        _speed = GetPlatformSpeed();
        var platform = new Platform(initialPosition, _speed, _previousPlatform?.Size);
        _previousPlatform = platform;

        // Instantiates the Platform's game object in the world
        switch (platform.Size)
        {
            case PlatformSize.Medium:
                MediumPlatformPrefab.GetComponent<PlatformController>().Speed = _speed;
                Instantiate(MediumPlatformPrefab, platform.Position, Quaternion.Euler(0, 0, 0));
                break;
            case PlatformSize.Large:
                LargePlatformPrefab.GetComponent<PlatformController>().Speed = _speed;
                Instantiate(LargePlatformPrefab, platform.Position, Quaternion.Euler(0, 0, 0));
                break;
        }
    }

    private float GetPlatformSpeed()
    {
        var speed = 0.1f;
        var playerClass = _player.GetComponent<PlayerController>().Player;

        if (playerClass.Score > 40)
        {
            speed = 0.24f;
        }
        else if (playerClass.Score > 30)
        {
            speed = 0.20f;
        }
        else if (playerClass.Score > 20)
        {
            speed = 0.16f;
        }
        else if (playerClass.Score > 10)
        {
            speed = 0.12f;
        }

        return speed * GameSettings.PlatformGenerationSpeedFactor;
    }
}