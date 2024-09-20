using System.Linq;
using UnityEngine;

public class PotionGenerationController : MonoBehaviour
{
    private float _potionGenerationCooldown = GameSettings.PotionGenerationCooldown;
    private readonly float[] yPositions = new float[] { 0.5f, 1.5f, 2.5f };
    public GameObject PotionPrefab;

    void Update()
    {
        _potionGenerationCooldown -= Time.unscaledDeltaTime;

        if (_potionGenerationCooldown <= 0)
        {
            GeneratePotion();
        }
    }

    void GeneratePotion()
    {
        _potionGenerationCooldown = GameSettings.PotionGenerationCooldown;

        var random = Random.Range(0, yPositions.Count());
        var initialPosition = new Vector3(20, yPositions[random], 0);

        PotionPrefab.transform.position = initialPosition;

        // Instantiates the Potion's game object in the world
        Instantiate(PotionPrefab, initialPosition, Quaternion.Euler(0, 0, 0));
    }
}
