using UnityEngine;

public class dragonRage : MonoBehaviour
{
    public float bulletSpeed = 8f;
    public GameObject dragonRageEffect;

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
            enemy.Damage(45f);
            }
            Destroy(gameObject);
            
            for(int i = 0; i < 6; i++)
            {
                Hoon_AudioManager.instance.SFXDragonRage2();
                Vector3 explosionPos = collision.transform.position + new Vector3(Random.Range(-0.8f, 0.8f), Random.Range(-0.8f, 0.8f), 0);
                GameObject go = Instantiate(dragonRageEffect, explosionPos, Quaternion.identity);
                Destroy(go, 0.5f);
            }
            
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
