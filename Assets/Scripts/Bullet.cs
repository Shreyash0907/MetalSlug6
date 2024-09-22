
using UnityEngine;

public class bullet : MonoBehaviour
{

    private float speed = 5f;
    private float life = 30f;

    private Rigidbody2D body;
    public Transform capsule;
    public SpriteRenderer player;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        Destroy(gameObject, life);
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(capsule)
        {
            Vector2 direction = capsule.up; 
            body.velocity = direction * speed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the bullet collided with a soldier
        
        if(collision.gameObject.CompareTag("Slope") || collision.gameObject.CompareTag("Bullet")){
            return;
        }
        Destroy(gameObject);
        
    }
}
