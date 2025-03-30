using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class KK_MonsterAttack_Zigzag : MonoBehaviour, IMonsterAttack
{   
    public GameObject bulletPrefab; // 총알 프리팹
    public int bulletCount = 10; // 총알 수 
    public float fireRate = 0.2f; // 슈팅 딜레이
    public Transform bulletPos; // 발사 위치 지정 가능하게
    private Transform player;
    private Coroutine fireRoutine;

    public void StartAttack()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (fireRoutine == null) // 코루틴 중복 방지
            fireRoutine = StartCoroutine(ZigzagFire());
    }

    public void StopAttack()
    {
        if (fireRoutine != null)
        {
            StopCoroutine(fireRoutine);
            fireRoutine = null;
        }
    }   

    IEnumerator ZigzagFire()
    {
        if (player == null)
        {
            yield break;
        }
        bool flag = false;
        while(true)
        {
            // 플레이어가 죽었거나 꺼져 있으면 멈춤
            if (player == null || !player.gameObject.activeInHierarchy)
                yield break;

            // 1. 플레이어 방향 계산
            Vector2 dirToPlayer = (player.position - transform.position).normalized;

            // 2. 기준 각도 구하기
            float baseAngle = Mathf.Atan2(dirToPlayer.y, dirToPlayer.x) * Mathf.Rad2Deg;

            KK_SoundManager.Instance.PlayFX(6, 0.25f); // 발사 효과음
            
            for (int i = 0; i < bulletCount; i ++)
            {
                // 3. 각도 계산
                float offset = (i * 10f) * (flag ? 1 : -1);
                float angle = baseAngle + offset;
                float rad = angle * Mathf.Deg2Rad;
                Vector2 dir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));

                Vector3 spawnPos = bulletPos != null ? bulletPos.position : transform.position;

                GameObject tempBullet = Instantiate(bulletPrefab, spawnPos, Quaternion.Euler(0, 0, angle));
                tempBullet.GetComponent<KK_MBullet>().SetSpeed(Random.Range(3f, 5f));
                
            }
            flag = !flag;
            yield return new WaitForSeconds(fireRate);
        }
    }
}
