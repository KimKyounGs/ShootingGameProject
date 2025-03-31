using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private float spawnInterval = 2f; // 몬스터 생성 간격
    [SerializeField] private float waveDuration = 60f; // 각 웨이브 지속 시간 (1분)
    private float elapsedTime = 0f;


    [Header("Monster 프리팹")]
    [SerializeField] private GameObject[] monster1; // 1~3분 몬스터
    [SerializeField] private GameObject[] monster2; // 3~6분 몬스터
    [SerializeField] private GameObject[] monster3; // 6~9분 몬스터
    [SerializeField] private GameObject Boss; // 보스

    [Header("BossWarning Text")]    
    [SerializeField] GameObject textBossWarning;

    private bool isBossSpawned = false;


    void Start()
    {
        StartCoroutine(ManageWaves());
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
    }

    IEnumerator ManageWaves()
    {
        // 1~3분: monster1 스폰
        yield return StartCoroutine(SpawnWave(monster1, waveDuration*3));

        // 3~6분: monster2 스폰
        yield return StartCoroutine(SpawnWave(monster2, waveDuration*3));

        // 6~9분: monster3 스폰
        yield return StartCoroutine(SpawnWave(monster3, waveDuration*3));

        // 9~10분:  스폰
        yield return StartCoroutine(SpawnRushWave(waveDuration));

        SpawnBoss();
    }

    IEnumerator SpawnWave(GameObject[] monsterPrefabs, float duration)
    {
        float waveTime = 0f;

        while (waveTime < duration)
        {
            GameObject prefab = monsterPrefabs[Random.Range(0, monsterPrefabs.Length)];
            SpawnMonsterWithLocation(prefab);

            waveTime += spawnInterval;
            yield return new WaitForSeconds(spawnInterval);
        }
    }
    IEnumerator SpawnRushWave(float duration)
    {
        float waveTime = 0f;
        List<GameObject> allMonsters = new List<GameObject>();
        allMonsters.AddRange(monster1);
        allMonsters.AddRange(monster2);
        allMonsters.AddRange(monster3);

        while (waveTime < duration)
        {
            GameObject prefab = allMonsters[Random.Range(0, allMonsters.Count)];
            SpawnMonsterWithLocation(prefab);

            waveTime += spawnInterval * 0.5f; // 러쉬는 더 빠르게 생성
            yield return new WaitForSeconds(spawnInterval * 0.5f);
        }
    }

    void SpawnMonsterWithLocation(GameObject monsterPrefab)
    {
        int spawnLocation = monsterPrefab.GetComponent<KK_Monster>().spawnLocation;
        Vector2 spawnPos = Vector2.zero;

        if (spawnLocation == 1) // 위쪽
        {
            float x = Random.Range(spawnUpStartX, spawnUpEndX);
            spawnPos = new Vector2(x, 5.5f); // 위 고정 위치
        }
        else if (spawnLocation == 2) // 중앙
        {
            float x = Random.Range(spawnCenterStartX, spawnCenterEndX);
            float y = Random.Range(spawnCenterStartY, spawnCenterEndY);
            spawnPos = new Vector2(x, y);
        }

        Instantiate(monsterPrefab, spawnPos, Quaternion.identity);
    }

    void SpawnBoss()
    {
        if (isBossSpawned) return;

        KK_SoundManager.Instance.PlayBGM(1); // 보스 소환 효과음
        isBossSpawned = true;
        textBossWarning.SetActive(true);
        Destroy(textBossWarning, 2.5f);

        Vector3 pos = new Vector3(0, 2.97f, 0);
        Instantiate(Boss, pos, Quaternion.identity);
    }
}