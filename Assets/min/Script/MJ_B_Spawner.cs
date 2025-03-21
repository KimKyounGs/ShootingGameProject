using System.Collections;
using UnityEngine;

public class MJ_B_Spawner : MonoBehaviour
{
    public float StartTime = 1; //시작
    public float RespawnCycle = 1;
    public float SpawnStop = 10; //스폰끝나는시간
    public GameObject monster;

    bool swi = true;


    [SerializeField]
    GameObject textBossWarning;

    //Start보다 먼저 시작
    private void Awake()
    {
        textBossWarning.SetActive(false);
    }


    void Start()
    {
        StartCoroutine("RandomSpawn");
        Invoke("Stop", SpawnStop);
    }

    //코루틴으로 랜덤하게 생성하기
    IEnumerator RandomSpawn()
    {
        yield return new WaitForSeconds(StartTime);
        textBossWarning.SetActive(true);
        yield return new WaitForSeconds(RespawnCycle);
        textBossWarning.SetActive(false);
        while (swi)
        {
            //몬스터 생성
            Instantiate(monster, transform.position, Quaternion.identity);
            //Debug.Log("몬스터 생성");
            yield return new WaitForSeconds(RespawnCycle);
        }
    }

    void Stop()
    {
        swi = false;
        StopCoroutine("RandomSpawn");
        //Debug.Log("몬스터 생성 끄기");
    }
}
