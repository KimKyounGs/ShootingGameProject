
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyBullet : MonoBehaviour
{
    public float Speed = 5.5f;
    public GameObject hitEffect;
    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(Vector3.down * Speed * Time.deltaTime);        
    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Hoon_Player player = collision.gameObject.GetComponent<Hoon_Player>();
            if (player != null)
            {
                player.Damage(1);
            }

            Vector3 vec = new Vector3(0,-0.2f,0);
            Hoon_AudioManager.instance.CryRemoraid();
            GameObject go = Instantiate(hitEffect, transform.position + vec, Quaternion.identity);
            Destroy(go,0.2f);
            Destroy(gameObject);
        }

    }
}