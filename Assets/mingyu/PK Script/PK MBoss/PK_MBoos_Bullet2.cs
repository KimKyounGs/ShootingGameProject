using System.Collections;
using UnityEngine;

public class PK_MBoos_Bullet_2 : MonoBehaviour
{
    public float Speed = 5f;
    public GameObject bullet;

    void Start()
    {
        StartCoroutine(CircleFire());
    }


    void Update()
    {
        transform.Translate(Vector2.down * Speed * Time.deltaTime);
    }



     IEnumerator CircleFire()
        {
            //공격주기
            float attackRate = 0.1f;
            //발사체 생성갯수
            int count = 2;
            //발사체 사이의 각도
            float intervalAngle = 360 / count;

            float weightAngle = 0f;

            //원 형태로 방사하는 발사체 생성(count 갯수 만큼)
            while (true)
            {
                for (int i = 0; i < count; ++i)
                {
                    PK_SoundManager.instance.MB_Bullet2_1();
                    //발사체 생성
                    GameObject clone = Instantiate(bullet, transform.position, Quaternion.identity);

                    //발사체 이동 방향(각도)
                    float angle = intervalAngle * i;
                    //발사체 이동 방향(벡터)
                    //Cos(각도)라디안 단위의 각도 표현을 위해 pi/180을 곱함
                    float x = Mathf.Cos(angle * Mathf.Deg2Rad);
                    //sin(각도)라디안 단위의 각도 표현을 위해 pi/180을 곱함
                    float y = Mathf.Sin(angle * Mathf.Deg2Rad);

                    //발사체 이동 방향 설정
                    clone.GetComponent<PK_Boss_Bullet1>().Move(new Vector2(x, y));
                }

                 weightAngle += 3;

                //3초마다 미사일 발사
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
}
