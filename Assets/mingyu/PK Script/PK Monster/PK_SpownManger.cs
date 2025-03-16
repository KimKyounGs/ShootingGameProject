using System.Collections;
using UnityEngine;

public class PK_SpownManger : MonoBehaviour
{
    public float ss = -2; //몬스터 생성 x값 처음
    public float es = 2;  //몬스터 생성 x값 끝
    public float StartTime = 1; //시작
    public float SpawnStop = 10; //스폰끝나는시간
    public float SpawnStop2 = 10; //몬스터2 스폰 끝나는 시간
    public GameObject monster;
    public GameObject monster2;


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
    void RandomSpawn2()
    {
            //x값 랜덤
            float x = Random.Range(ss, es);
            //x값은 랜덤 y값은 자기자신값
            Vector2 r = new Vector2(transform.position.x, transform.position.y);
            //몬스터 생성
            Instantiate(monster2, r, Quaternion.identity);
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
        swi2 = false;
        StopCoroutine("RandomSpawn2");
        //보스

    }
}
