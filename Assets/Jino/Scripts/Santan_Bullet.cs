using UnityEngine;

public class Santan_Bullet : MonoBehaviour
{
    public float Speed = 7f;
    Vector2 vec2 = Vector2.down;

    public GameObject Effect_1;

    void Start()
    {
        
    }

    
    void Update()
    {
        transform.Translate(vec2 * Speed * Time.deltaTime);
    }

    public void Move(Vector2 vec)
    {
        vec2 = vec;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameObject go = Instantiate(Effect_1, transform.position, Quaternion.identity);
            Destroy(go, 1f);
            Destroy(gameObject);
        }
    }
}
