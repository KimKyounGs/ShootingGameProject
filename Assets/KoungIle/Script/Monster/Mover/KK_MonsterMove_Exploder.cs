using UnityEngine;

public class KK_MonsterMove_Explore : MonoBehaviour, IMonsterMove
{
    public float speed = 3f;
    public GameObject explosionEffect;
    public GameObject target;
    
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        // 플레이어가 죽었을 때 바꾸기.
    }

    public void Move()
    {
        if (target != null)
        {
            Vector3 direction = (target.transform.position - transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime);
        }
        else
        {
            Debug.Log("플레이어 찾는중~");
            target = GameObject.FindGameObjectWithTag("Player");
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
