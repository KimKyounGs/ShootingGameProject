using UnityEngine;
using System.Collections;

public class KK_MonsterAttack_Random : MonoBehaviour, IMonsterAttack
{   
    public GameObject bulletPrefab; // 총알 프리팹
    public int bulletPerBurst = 6; // 총알 개수
    public float fireRate = 0.6f; // 슈팅 딜레이

    public void StartAttack()
    {
        StartCoroutine(RandomFire());
    }

    IEnumerator RandomFire()
    {
        while(true)
        {
            for (int i = 0; i < bulletPerBurst; i ++)
            {
                float angle = Random.Range(0f, 360f);
                float rad = angle * Mathf.Deg2Rad;
                Vector2 dir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
                GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                bullet.GetComponent<KK_MBullet>().Move(dir);
            }
            yield return new WaitForSeconds(fireRate);
        }
    }
}
