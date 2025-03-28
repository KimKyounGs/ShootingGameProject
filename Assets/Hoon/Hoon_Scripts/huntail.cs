using UnityEngine;

public class huntail : MonoBehaviour
{
    public float bulletSpeed = 6f;
    public GameObject waterfallEffect;

    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(Vector2.up * bulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Hoon_Enemy"))
        {
            Hoon_Monster enemy = collision.GetComponent<Hoon_Monster>();
            if (enemy != null)
            {
                Hoon_AudioManager.instance.SFXWaterfall2();
                enemy.Damage(70f);
                for(int i = 0; i < 3; i++)
                {
                    GameObject go = Instantiate(waterfallEffect, transform.position + new Vector3(Random.Range(-0.4f, 0.4f), Random.Range(0, 0.5f), 0), Quaternion.identity);
                    Destroy(go, 0.5f);
                }
            }
            Destroy(gameObject, 0.5f);
        }
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
