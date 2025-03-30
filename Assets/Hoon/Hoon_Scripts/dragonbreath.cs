using UnityEngine;

public class dragonbreath : MonoBehaviour
{
    public float bulletSpeed = 4f;

    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(Vector2.up * bulletSpeed * Time.deltaTime);
        Destroy(gameObject, 0.75f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Hoon_Enemy"))
        {
            Hoon_Monster enemy = collision.GetComponent<Hoon_Monster>();
            Hoon_AudioManager.instance.SFXBurn();
            if (enemy != null)
            {
            enemy.Damage(1f);
            }
            Destroy(gameObject);
        }
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
