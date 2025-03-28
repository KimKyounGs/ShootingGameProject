using System.Collections;
using UnityEngine;

public class PK_SpownManger : MonoBehaviour
{
    public float ss = -2; // 몬스터 생성 x값 처음
    public float es = 2;  // 몬스터 생성 x값 끝
    public float spawnInterval = 5f; // 몬스터 생성 간격
    public GameObject monster;  // 몬스터1
    public GameObject monster2; // 몬스터2

    void Start()
    {
        StartCoroutine(RandomSpawn());
    }

    // 코루틴으로 랜덤하게 몬스터 생성
    IEnumerator RandomSpawn()
    {
        while (true)
        {
            // 3초마다 실행
            yield return new WaitForSeconds(spawnInterval);

            // x값 랜덤
            float x = Random.Range(ss, es);
            Vector2 spawnPosition = new Vector2(x, transform.position.y);

            // 몬스터1 또는 몬스터2를 랜덤하게 선택
            GameObject selectedMonster = Random.value > 0.5f ? monster : monster2;

            // 몬스터 생성
            Instantiate(selectedMonster, spawnPosition, Quaternion.identity);
        }
    }
}
