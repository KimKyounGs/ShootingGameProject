using System.Threading;
using UnityEngine;

public class MJ_PBullet : MonoBehaviour
{
    public float Speed = 4.0f;
    //공격력
    public int Attack = 0;
    //이펙트
    public GameObject effect;


    void Update()
    {
        //미사일 위쪽방향으로 움직이기
        //위의 방향 * 스피드 * 타임
        transform.Translate(Vector2.up * Speed * Time.deltaTime);
    }

    private void OnBecameInvisible()
    {
        //자기 자신 지우기
        Destroy(gameObject);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MJ_Monster"))
        {

            //이펙트생성
            GameObject go = Instantiate(effect, transform.position, Quaternion.identity);
            //1초뒤에 지우기
            Destroy(go, 1);

            //몬스터삭제
            collision.gameObject.GetComponent<MJ_MonsterHP>().TakeDamage(Attack);
            //미사일 삭제
            Destroy(gameObject);

        }


        if (collision.CompareTag("MJ_Monster_Boss"))
        {

            //이펙트생성
            GameObject go = Instantiate(effect, transform.position, Quaternion.identity);
            //1초뒤에 지우기
            Destroy(go, 1);

            //몬스터삭제
            collision.gameObject.GetComponent<MJ_BossHP>().TakeDamage(Attack);
            //미사일 삭제
            Destroy(gameObject);
        }

        if (collision.CompareTag("MJ_BossCircle"))
        {

            //이펙트생성
            GameObject go = Instantiate(effect, transform.position, Quaternion.identity);
            //1초뒤에 지우기
            Destroy(go, 1);

            //몬스터삭제
            collision.gameObject.GetComponent<MJ_Mbullet_Circle>().Damage(Attack);

            //미사일 삭제
            Destroy(gameObject);
        }
    }
}

