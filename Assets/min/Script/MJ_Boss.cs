using System.Collections;
using UnityEngine;

public class MJ_Boss : MonoBehaviour
{
    int flag = 1;
    public float speed = 2f;
    public float fire_delay_nom = 0.5f;
    public float fire_delay_hom = 8f;
    public float fire_delay_cir = 8f;

    public GameObject ms;
    public GameObject ms2;
    public GameObject ms3;
    public GameObject ms4;

    public Transform pos1;
    public Transform pos2;
    public Transform pos3;
    public Transform pos4;


    void Start()
    {
        StartCoroutine(BossMissle());
        StartCoroutine(CircleFire());
        StartCoroutine(BossHoming());
        StartCoroutine(BossCircle());
    }
    IEnumerator BossMissle()
    {
        while (true)
        {
            //미사일 두개
            Instantiate(ms, pos1.position, Quaternion.identity);
            Instantiate(ms, pos2.position, Quaternion.identity);

            yield return new WaitForSeconds(fire_delay_nom);
        }
    }
    IEnumerator BossHoming()
    {
        while (true)
        {
            Instantiate(ms3, pos3.position, Quaternion.identity);

            yield return new WaitForSeconds(fire_delay_hom);
        }
    }

    IEnumerator BossCircle()
    {
        while (true)
        {
            Instantiate(ms4, pos4.position, Quaternion.identity);

            yield return new WaitForSeconds(fire_delay_hom);
        }
    }
    
    IEnumerator CircleFire()
    {
        //공격주기
        float attackRate = 5;
        //발사체 생성갯수
        int count = 30;
        //발사체 사이의 각도
        float intervalAngle = 360 / count;
        //가중되는 각도(항상 같은 위치로 발사하지 않도록 설정
        float weightAngle = 0f;

        //원 형태로 방사하는 발사체 생성(count 갯수 만큼)
        while (true)
        {

            for (int i = 0; i < count; ++i)
            {
                //발사체 생성
                GameObject go = Instantiate(ms2, transform.position, Quaternion.identity);

                //발사체 이동 방향(각도)
                float angle = weightAngle + intervalAngle * i;
                //발사체 이동 방향(벡터)
                //Cos(각도)라디안 단위의 각도 표현을 위해 pi/180을 곱함
                float x = Mathf.Cos(angle * Mathf.Deg2Rad);
                //sin(각도)라디안 단위의 각도 표현을 위해 pi/180을 곱함
                float y = Mathf.Sin(angle * Mathf.Deg2Rad);

                //발사체 이동 방향 설정
                go.GetComponent<MJ_Boss_bullet>().Move(new Vector2(x, y));
            }
            //발사체가 생성되는 시작 각도 설정을 위한변수
            weightAngle += 1;

            //3초마다 미사일 발사
            yield return new WaitForSeconds(attackRate);

        }

    }
    void Update()
    {
        //보스 좌우로 움직이게 하기
        if (transform.position.x >= 1)
            flag *= -1;
        if (transform.position.x <= -1)
            flag *= -1;


        transform.Translate(flag * speed * Time.deltaTime, 0, 0);
    }
}
