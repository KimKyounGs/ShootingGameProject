using UnityEngine;
using System.Collections;

public class PK_Boss_Bullet4 : MonoBehaviour
{
    public GameObject target;
    public GameObject Bullet;
    public float Speed = 3f;
    public float cool_time = 0;
    public bool a = true;
    public int bullet_count = 0;    //발사 최대 실행 개수
    Vector2 dirNo;
    Vector2 dir;
    Vector2 LastPlayer;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        dir = target.transform.position - transform.position;   //플레이어 위치 쫒기
        LastPlayer = target.transform.position; //마지막 플레이어 위치 저장
        dirNo = dir.normalized;
    }

    void Update()
    {
        cool_time += Time.deltaTime;
        transform.Translate(dirNo * Speed * Time.deltaTime);
        float Random_Time = Random.Range(1.5f, 2f);

        float End = Vector2.Distance(transform.position, LastPlayer);

        if (End < 0.1f)
        {
            Speed = 0;
            if (cool_time >= Random_Time && a == true)
            {
                StartCoroutine(CircleFire());
                a = false;
            }
        }

        if (bullet_count >= 6)
        {
            Destroy(gameObject);
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


    IEnumerator CircleFire()
    {

        //공격주기
        float attackRate = 1f;
        //발사체 생성갯수
        int count = 10;
        //발사체 사이의 각도
        float intervalAngle = 360 / count;
        //가중되는 각도(항상 같은 위치로 발사하지 않도록 설정
        float weightAngle = 0f;

        //원 형태로 방사하는 발사체 생성(count 갯수 만큼)
        while (bullet_count < 6)
        {

            for (int i = 0; i < count; ++i)
            {
                //발사체 생성
                GameObject clone = Instantiate(Bullet, transform.position, Quaternion.identity);

                //발사체 이동 방향(각도)
                float angle = weightAngle + intervalAngle * i;
                //발사체 이동 방향(벡터)
                //Cos(각도)라디안 단위의 각도 표현을 위해 pi/180을 곱함
                float x = Mathf.Cos(angle * Mathf.Deg2Rad);
                //sin(각도)라디안 단위의 각도 표현을 위해 pi/180을 곱함
                float y = Mathf.Sin(angle * Mathf.Deg2Rad);

                //발사체 이동 방향 설정
                clone.GetComponent<PK_Boss_Bullet4_1>().Move(new Vector2(x, y));
            }
            weightAngle += 90;
            bullet_count++;

            //1초마다 미사일 발사
            yield return new WaitForSeconds(attackRate);
        }
    }
}
