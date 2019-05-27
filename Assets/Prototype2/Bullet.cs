using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D bulletRigidbody;
    public Vector2 velocity;

    private void Start()
    {
        bulletRigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print(collision.gameObject.name);
        if (collision.gameObject.name.Contains("Bullet") || collision.gameObject.tag == "Floor")
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        bulletRigidbody.velocity = this.velocity;
    }
}
