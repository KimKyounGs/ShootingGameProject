using UnityEngine;

public class PBullet : MonoBehaviour
{
    public float Speed = 4.0f;
    public GameObject Effect_1;

    void Start()
    {
           
    }

    
    void Update()
    {
        transform.Translate(Vector2.up * Speed * Time.deltaTime);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Monster"))
        {
            GameObject go = Instantiate(Effect_1, transform.position, Quaternion.identity);
            Destroy(go, 1);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }

        if (collision.CompareTag("Boss"))
        {
            GameObject go = Instantiate(Effect_1, transform.position, Quaternion.identity);
            Destroy(go, 1);
            Destroy(gameObject);
        }
    }
}
