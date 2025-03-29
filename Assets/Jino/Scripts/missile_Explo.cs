using UnityEngine;

public class missile_Explo : MonoBehaviour
{
    public float Speed = 3f;

    public GameObject Effect_1;

    void Start()
    {
        
    }

    
    void Update()
    {
        transform.Translate(Vector3.down * Speed * Time.deltaTime);
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
