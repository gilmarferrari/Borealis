using UnityEngine;

public class PlatformController : MonoBehaviour
{
    private bool _hasBeenTouchedByPlayer;
    private GameObject _player;
    public float Speed;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");

        if (Speed <= 0f )
        {
            Speed = 0.1f;
        }
    }

    void FixedUpdate()
    {
        // Gets the current game object's Position
        Vector3 currentPosition = this.gameObject.transform.position;

        // Moves the game object by reducing its 'x' position
        this.gameObject.transform.position = new Vector3(currentPosition.x - Speed, currentPosition.y, currentPosition.z);

        // Destroys the game object after a certain distance
        if (this.gameObject.transform.position.x <= -60)
        {
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // If the game object collides with another one
        if (collision.gameObject.tag == "Platform")
        {
            Destroy(this.gameObject);
        }

        // If the game object collides with the player
        if (collision.gameObject == _player)
        {
            if (!_hasBeenTouchedByPlayer)
            {
                var playerClass = _player.GetComponent<PlayerController>().Player;

                playerClass.AddScore(1);
                _hasBeenTouchedByPlayer = true;
            }
        }
    }
}
