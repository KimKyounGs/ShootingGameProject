using System.Collections;
using UnityEngine;

public class PK_MShadow1 : MonoBehaviour
{
    public GameObject Bullet_2; // 발사체 프리팹
    public float attackRate = 3f; // 발사 간격
    public int count = 3; // 발사체 개수

    public int M_HP = 5;

    public GameObject Item = null;
     public float Speed = 3f;

    void Start()
    {
        StartCoroutine(CircleFireTowardsPlayer());
    }

    void Update()
    {
        transform.Translate(Vector2.left * Speed * Time.deltaTime);
    }


    IEnumerator CircleFireTowardsPlayer()
    {
        while (true)
        {
            // 플레이어 찾기
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (player != null)
            {
                // 플레이어와의 방향 계산
                Vector2 directionToPlayer = (player.transform.position - transform.position).normalized;
                float baseAngle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;

                // 발사체를 원형으로 발사
                float intervalAngle = 50 / count;
                for (int i = 0; i < count; ++i)
                {
                    // 각도 계산
                    float angle = baseAngle-15 + intervalAngle * i;
                    float x = Mathf.Cos(angle * Mathf.Deg2Rad);
                    float y = Mathf.Sin(angle * Mathf.Deg2Rad);

                    // 총알 생성
                    GameObject clone = Instantiate(Bullet_2, transform.position, Quaternion.identity);

                    // 총알 이동 설정
                    clone.GetComponent<PK_MBoss_Bullet1>().Move(new Vector2(x, y));
                }
            }

            // 발사 간격 대기
            yield return new WaitForSeconds(attackRate);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

        private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    public void Damage(int attack)
    {
        M_HP -= attack;
        if (M_HP <= 0)
        {
            ItemDrop();
            Destroy(gameObject);
        }
    }






    public int item_Random = 0;

    public void ItemDrop()
    {
        item_Random = Random.Range(0, 11);

        if (item_Random >= 9)
        {
            //아이템 생성
            Instantiate(Item, transform.position, Quaternion.identity);
        }
    }
}
