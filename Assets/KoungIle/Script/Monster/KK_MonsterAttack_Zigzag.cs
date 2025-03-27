using UnityEngine;
using System.Collections;

public class KK_MonsterAttack_Zigzag : MonoBehaviour, IMonsterAttack
{   
    public GameObject bulletPrefab; // 총알 프리팹
    public int bulletCount = 10; // 총알 수 
    public float fireRate = 0.2f; // 슈팅 딜레이
    private Transform player;

    public void StartAttack()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        StartCoroutine(ZigzagFire());
    }

    IEnumerator ZigzagFire()
    {
        bool flag = false;
        while(true)
        {
            // 1. 플레이어 방향 계산
            Vector2 dirToPlayer = (player.position - transform.position).normalized;

            // 2. 기준 각도 구하기
            float baseAngle = Mathf.Atan2(dirToPlayer.y, dirToPlayer.x) * Mathf.Rad2Deg;
            for (int i = 0; i < bulletCount; i ++)
            {
                float angle;
                if (flag) angle = baseAngle + i * 5f; 
                else angle = baseAngle + i * -5f; // 5f는 간격
                float rad = angle * Mathf.Deg2Rad;
                Vector2 dir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
                GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                bullet.GetComponent<KK_MBullet>().Move(dir); 
            }
            flag = !flag;
            yield return new WaitForSeconds(fireRate);
        }
    }
}
