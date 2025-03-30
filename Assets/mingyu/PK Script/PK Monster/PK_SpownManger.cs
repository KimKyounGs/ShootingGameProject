using System.Collections;
using UnityEngine;

public class PK_SpownManger : MonoBehaviour
{
    public float ss = -2; //몬스터 생성 x값 처음
    public float es = 2;  //몬스터 생성 x값 끝
    public float StartTime = 1; //시작
    public float SpawnStop = 10; //스폰끝나는시간
    public float SpawnStop2 = 10; //몬스터2 스폰 끝나는 시간

    public float SpawnStop3 = 20;

    public GameObject monster;
    public GameObject monster2;

    public GameObject MBoss;
    public GameObject Boss;

    public GameObject Story2;

    public GameObject Story2_1;
    public GameObject Story3;


    bool swi = true;
    bool swi2 = true;

    void Start()
    {
        StartCoroutine("RandomSpawn");
        Invoke("Stop", SpawnStop);
    }

    //코루틴으로 랜덤하게 생성하기
    IEnumerator RandomSpawn()
    {
        while (swi)
        {
            //1초마다
            yield return new WaitForSeconds(StartTime);
            //x값 랜덤
            float x = Random.Range(ss, es);
            //x값은 랜덤 y값은 자기자신값
            Vector2 r = new Vector2(x, transform.position.y);
            //몬스터 생성
            Instantiate(monster, r, Quaternion.identity);
        }
    }
    //코루틴으로 랜덤하게 생성하기
    IEnumerator RandomSpawn2()
    {
        while (swi2)
        {
            //1초마다
            yield return new WaitForSeconds(StartTime + 2);
            //x값 랜덤
            float x = Random.Range(ss, es);
            //x값은 랜덤 y값은 자기자신값
            Vector2 r = new Vector2(x, transform.position.y);
            //몬스터 생성
            Instantiate(monster2, r, Quaternion.identity);
        }
    }
    void Stop()
    {
        swi = false;
        StopCoroutine("RandomSpawn");
        //두번째 몬스터 코루틴
        StartCoroutine("RandomSpawn2");

        //30초뒤에 2번째 몬스터 호출
        Invoke("Stop2", SpawnStop2);

    }

    void Stop2()
    {
        Story2.SetActive(true);
        swi2 = false;
        Invoke("Stop3", 0.5f);
    }
    void Stop3()
    {
        swi2 = false;
        StopCoroutine("RandomSpawn2");

        //중간보스
        Vector3 pos = new Vector3(0, 3.66f, 0);
        Instantiate(MBoss, pos, Quaternion.identity);

    }

    public void Stop4()
    {
        Story2_1.SetActive(true);

        swi = true;
        swi2 = true;
        StartCoroutine("RandomSpawn");
        //두번째 몬스터 코루틴
        StartCoroutine("RandomSpawn2");

        Invoke("Stop5", SpawnStop3);
    }

        public void Stop5()
    {
        Story3.SetActive(true);
        swi = false;
        swi2 = false;
        Vector3 pos = new Vector3(0, 4f, 0);
        Instantiate(Boss, pos, Quaternion.identity);
    }
}
