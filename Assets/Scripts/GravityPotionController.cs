using UnityEngine;

public class GravityPotionController : MonoBehaviour
{
    private float _speed = 0.2f;

    void FixedUpdate()
    {
        // Gets the current game object's Position
        Vector3 currentPosition = this.gameObject.transform.position;

        // Moves the game object by reducing its 'x' position
        this.gameObject.transform.position = new Vector3(currentPosition.x - _speed, currentPosition.y, currentPosition.z);

        // Destroys the game object after a certain distance
        if (this.gameObject.transform.position.x <= -60)
        {
            Destroy(this.gameObject);
        }
    }
}
