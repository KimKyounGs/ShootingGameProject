using System.Runtime.CompilerServices;
using UnityEngine;

public class PK_Player_Bullet : MonoBehaviour
{
    public float Speed = 4.0f;

    public GameObject effect;
    


    void Start()
    {
    }


    void Update()
    {
        transform.Translate(Vector2.up * Speed * Time.deltaTime);
    }



    private void OnBecameInvisible()
    {
        //자기 자신 지우기
        Destroy(gameObject);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        

        if (collision.CompareTag("PK_Boss"))
        {
            //이펙트생성
            GameObject go = Instantiate(effect, transform.position, Quaternion.identity);
            //1초뒤에 지우기
            Destroy(go, 1);

            PK_Boss boss = collision.gameObject.GetComponent<PK_Boss>();
            if (boss != null)
            {
                boss.Damage(+1);
            }

            PK_MBoss mboss = collision.gameObject.GetComponent<PK_MBoss>();
            if (mboss != null)
            {
                mboss.Damage(+1);
            }

            //미사일 삭제
            Destroy(gameObject);

        }

    if (collision.CompareTag("PK_Monster"))
    {
    // 이펙트 생성
    GameObject go = Instantiate(effect, transform.position, Quaternion.identity);
    // 1초 뒤에 이펙트 삭제
    Destroy(go, 1);

    // PK_Monster 데미지 주기
    PK_Monster monster = collision.gameObject.GetComponent<PK_Monster>();
    if (monster != null)
    {
        monster.Damage(+1);
    }

    // PK_MShadow 데미지 주기
    PK_MShadow shadow = collision.gameObject.GetComponent<PK_MShadow>();
    if (shadow != null)
    {
        shadow.Damage(+1);
    }

    // PK_MShadow1 데미지 주기
    PK_MShadow1 shadow1 = collision.gameObject.GetComponent<PK_MShadow1>();
    if (shadow1 != null)
    {
        shadow1.Damage(+1);
    }

    // PK_MBoos_Bullet3 데미지 주기
    PK_MBoos_Bullet3 bullet3 = collision.gameObject.GetComponent<PK_MBoos_Bullet3>();
    if (bullet3 != null)
    {
        bullet3.Damage(+1);
    }

    // 미사일 삭제
    Destroy(gameObject);
}
    }
}


