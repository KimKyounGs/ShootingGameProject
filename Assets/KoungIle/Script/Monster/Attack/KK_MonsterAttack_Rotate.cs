using UnityEngine;
using System.Collections;

public class KK_MonsterAttack_Rotate : MonoBehaviour, IMonsterAttack
{
    public GameObject bulletPrefab;
    public int bulletCount = 12; // 탄 수
    public float fireRate = 0.5f; // 슈팅 딜레이
    public float rotationSpeed = 30f; // 총알 속도
    private float currentAngle = 0f; // 현재 각도

    public void StartAttack()
    {
        StartCoroutine(RotateFire());
    }

    public void StopAttack()
    {
        StopCoroutine(RotateFire());
    }

    IEnumerator RotateFire()
    {
        while(true)
        {
            for(int i = 0; i < bulletCount; i ++)
            {
                float angle = i * 360f / bulletCount + currentAngle;
                float rad = angle * Mathf.Deg2Rad;
                Vector2 dir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
                GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                float visualAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                bullet.transform.rotation = Quaternion.Euler(0, 0, visualAngle);
                // bullet.GetComponent<KK_MBullet>().Move(dir);
                yield return new WaitForSeconds(fireRate);
            }

            currentAngle += rotationSpeed;
        }
    }

}
