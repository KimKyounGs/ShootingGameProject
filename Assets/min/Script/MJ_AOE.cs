using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class MJ_AOE : MonoBehaviour
{
    public GameObject warningEffect; // 경고 이펙트 (프리팹)
    public GameObject attackEffect;  // 실제 공격 이펙트 (프리팹)
    public float attackDelay = 5f;   // 공격까지 걸리는 시간
    public float ss = -1.5f;
    public float es = 1.5f;

    public float wait = 60f;
    bool swi = true;

    public float RespawnCycle = 5f;
    public float SpawnStop = 10;

    void Start()
    {
        StartCoroutine("AOEAttack");
        Invoke("Stop", SpawnStop);
    }

    IEnumerator AOEAttack()
    {
        yield return new WaitForSeconds(wait);
        while (swi) 
        {
            float x = Random.Range(ss, es);
            //경고 표시
            Vector2 vec = new Vector2(x, transform.position.y);
            GameObject warning = Instantiate(warningEffect, vec, Quaternion.identity);
            yield return new WaitForSeconds(attackDelay);
            //공격 실행
            Destroy(warning); // 경고 제거
            GameObject attack = Instantiate(attackEffect, vec, Quaternion.identity);

            Destroy(attack, 4f);

            yield return new WaitForSeconds(RespawnCycle);
        }
    }

    void Stop()
    {
        swi = false;
        StopCoroutine("AOEAttack");
    }

}
