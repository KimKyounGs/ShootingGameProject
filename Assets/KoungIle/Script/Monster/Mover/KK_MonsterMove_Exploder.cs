using UnityEngine;

public class KK_MonsterMove_Explore : MonoBehaviour, IMonsterMove
{
    public float speed = 3f;
    public GameObject explosionEffect;
    private Transform player;
    

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        // 플레이어가 죽었을 때 바꾸기.
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
            GameObject effect = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(effect, 1f);
            KK_Player player = collision.GetComponent<KK_Player>();
            if (player.bplayerInvincibility == false)
            {
                player.PlayerDie();
            }

            Destroy(gameObject);
        }
    }
}
