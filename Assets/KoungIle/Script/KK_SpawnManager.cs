using System.Collections;
using UnityEngine;

public class KK_SpawnManaer : MonoBehaviour
{
    [Header("Spawn1 포인트 위치 변수 값(위에)")]
    [SerializeField] private float spawnUpStartX = -2; 
    [SerializeField] private float spawnUpEndX = 2;  

    [Header("Spawn2 포인트 위치 변수 값(중앙)")]
    [SerializeField] private float spawnCenterStartX = -2.5f; 
    [SerializeField] private float spawnCenterEndX = 2.5f;  
    [SerializeField] private float spawnCenterStartY = 0f;
    [SerializeField] private float spawnCenterEndY = 4.5f;

    [Header("스폰 시간")]
    [SerializeField] private float StartTime = 1; //시작
    [SerializeField] private float SpawnStop = 10; //스폰 끝나는시간

    [Header("Monster 프리팹")]
    [SerializeField] private GameObject[] monster1;
    [SerializeField] private GameObject[] monster2;
    [SerializeField] private GameObject[] monster3;
    [SerializeField] private GameObject Boss;


    [SerializeField]
    GameObject textBossWarning;

    bool swi = true;
    bool swi2 = true;


void Start()
{
    StartCoroutine(RandomSpawn(monster1, spawnUpStartX, spawnUpEndX, 5.5f, StartTime, true));
    Invoke("Stop", SpawnStop);
}

// 랜덤 스폰을 하나의 코루틴으로 통합
IEnumerator RandomSpawn(GameObject[] monsterPrefab, float startX, float endX, float yPos, float spawnDelay, bool isFirstWave)
{
    bool isRunning = true;

    while (isRunning)
    {
        yield return new WaitForSeconds(spawnDelay);

        float x = Random.Range(startX, endX);
        Vector2 spawnPos = new Vector2(x, yPos);
        //Instantiate(monsterPrefab, spawnPos, Quaternion.identity);

        // 첫 번째 웨이브 종료 시 flag 변경
        if (!isFirstWave) isRunning = swi2;
        else isRunning = swi;
    }
}

// 첫 번째 몬스터 웨이브 종료 & 두 번째 몬스터 웨이브 시작
void Stop()
{
    swi = false;

    //StartCoroutine(RandomSpawn(monster2, ss, es, transform.position.y, StartTime + 2, false));
    Invoke("Stop2", SpawnStop + 20);
}

// 두 번째 몬스터 웨이브 종료 & 보스 등장
void Stop2()
{
    swi2 = false;

    textBossWarning.SetActive(true);
    Vector3 pos = new Vector3(0, 2.97f, 0);
    Instantiate(Boss, pos, Quaternion.identity);
}
}