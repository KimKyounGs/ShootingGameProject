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
        if (collision.CompareTag("PK_Monster"))
        {
            //이펙트생성
            GameObject go = Instantiate(effect, transform.position, Quaternion.identity);
            //1초뒤에 지우기
            Destroy(go, 1);

            

            //몬스터 데미지 주기
            collision.gameObject.GetComponent<PK_Monster>().Damage(+1);

            //미사일 삭제
            Destroy(gameObject);

        }

        if (collision.CompareTag("PK_Boss"))
        {
            //이펙트생성
            GameObject go = Instantiate(effect, transform.position, Quaternion.identity);
            //1초뒤에 지우기
            Destroy(go, 1);

            //몬스터 데미지 주기
            collision.gameObject.GetComponent<PK_Boss>().Damage(+1);

            //미사일 삭제
            Destroy(gameObject);

        }
    }
}


