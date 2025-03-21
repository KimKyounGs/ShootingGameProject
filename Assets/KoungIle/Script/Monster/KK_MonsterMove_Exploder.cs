using UnityEngine;

public class KK_MonsterMove_Explore : MonoBehaviour, IMonsterMove
{
    public float speed = 3f;
    public GameObject explosionEffect;
    private Transform player;
    

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void Move()
    {
        if (player != null)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 플레이어에게 데미지 주기 등 처리 가능
            if (explosionEffect != null)
            {
                GameObject effect = Instantiate(explosionEffect, transform.position, Quaternion.identity);
                Destroy(effect, 0.5f);
            }
                


            Destroy(gameObject);
        }
    }
}
