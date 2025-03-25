using UnityEngine;
using System.Collections;

public class KK_MonsterAttack_Zigzag : MonoBehaviour, IMonsterAttack
{   
    public GameObject bulletPrefab; // 총알 프리팹
    public int bulletCount = 10; // 총알 수 
    public float fireRate = 0.2f; // 슈팅 딜레이

    public void StartAttack()
    {
        StartCoroutine(ZigzagFire());
    }

    IEnumerator ZigzagFire()
    {
        bool direction = true;
        while(true)
        {
            float baseAngle = direction ? 30f : -30f;
            for (int i = 0; i < bulletCount; i ++)
            {
                float angle = baseAngle + i * 5f;
                float rad = angle * Mathf.Deg2Rad;
                Vector2 dir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
                GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                bullet.GetComponent<KK_MBullet>().Move(dir); 
            }
        }

        direction = !direction;
        yield return new WaitForSeconds(fireRate);
    }
}
