using UnityEngine;

public class PK_S_Damage : MonoBehaviour
{
    public float a = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        a += Time.deltaTime;

        if (a > 1 && a < 2)
        {
            gameObject.SetActive(false);
            a = 0;
        }
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PK_Monster"))
        {
            //몬스터 데미지 주기
            collision.gameObject.GetComponent<PK_Monster>().Damage(+30);
        }

        if (collision.CompareTag("PK_Boss"))
        {
            //몬스터 데미지 주기
            collision.gameObject.GetComponent<PK_Boss>().Damage(+30);

        }

        if (collision.CompareTag("PK_Monster_Bullet"))
        {
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Swould"))
        {
            Destroy(collision.gameObject);
        }
    }
}
