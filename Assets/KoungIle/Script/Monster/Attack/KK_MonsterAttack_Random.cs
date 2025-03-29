using UnityEngine;
using System.Collections;

public class KK_MonsterAttack_Random : MonoBehaviour, IMonsterAttack
{   
    public GameObject bulletPrefab; // 총알 프리팹
    public int bulletPerBurst = 6; // 총알 개수
    public float fireRate = 0.6f; // 쿨타임

    public void StartAttack()
    {
        StartCoroutine(RandomFire());
    }

    public void StopAttack()
    {
        StopCoroutine(RandomFire());
    }

    IEnumerator RandomFire()
    {
        while(true)
        {
            KK_SoundManager.Instance.PlayFX(8); // 발사 효과음
            for (int i = 0; i < bulletPerBurst; i ++)
            {
                float angle = Random.Range(-180, 0);
                float rad = angle * Mathf.Deg2Rad;
                Vector2 dir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
                GameObject tempBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                float visualAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                tempBullet.transform.rotation = Quaternion.Euler(0, 0, visualAngle);
                tempBullet.GetComponent<KK_MBullet>().SetSpeed(Random.Range(3f, 5f));
            }
            yield return new WaitForSeconds(fireRate);
        }
    }
}
