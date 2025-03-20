using UnityEngine;
using System.Collections;

public class MJ_Spawner : MonoBehaviour
{
    public float ss = -2; //몬스터 생성 x값 처음
    public float es = 2;  //몬스터 생성 x값 끝
    public float StartTime = 1; //시작
    public float RespawnCycle = 1;
    public float SpawnStop = 10; //스폰끝나는시간
    public GameObject monster;

    bool swi = true;

    void Start()
    {
        StartCoroutine("RandomSpawn");
        Invoke("Stop", SpawnStop);
    }

    //코루틴으로 랜덤하게 생성하기
    IEnumerator RandomSpawn()
    {
        yield return new WaitForSeconds(StartTime);
        while (swi)
        {
            //x값 랜덤
            float x = Random.Range(ss, es);
            //x값은 랜덤 y값은 자기자신값
            Vector2 r = new Vector2(x, transform.position.y);
            //몬스터 생성
            Instantiate(monster, r, Quaternion.identity);

            yield return new WaitForSeconds(RespawnCycle);
        }
    }

    void Stop()
    {
        swi = false;
        StopCoroutine("RandomSpawn");
    }
}
