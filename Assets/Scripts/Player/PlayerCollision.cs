using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
       PlayerEntity.instance.health.CollisionDetected(collision.gameObject);
    }
}
