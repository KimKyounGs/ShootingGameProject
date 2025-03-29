using UnityEngine;

public class KK_MonsterAttack_Circle : MonoBehaviour, IMonsterAttack
{
    public GameObject bullet;
    public int bulletCount = 8;
    public float delay = 2f;
    private float weightAngle = 0f;
    
    public void StartAttack()
    {
        InvokeRepeating("ShootCircle", delay, delay);
    }

    public void StopAttack()
    {
        CancelInvoke("ShootCircle");
    }

    void ShootCircle()
    {
        KK_SoundManager.Instance.PlayFX(9); // 발사 효과음
        for (int i = 0; i < bulletCount; i ++)
        {
            // 각도를 균등하게 나누고, 보정 각도 추가
            float angle = i * 360f / bulletCount + weightAngle;
            float rad = angle * Mathf.Deg2Rad;

            // 방향 벡터 계산
            Vector2 dir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)).normalized;
            GameObject tempBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            float visualAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            tempBullet.transform.rotation = Quaternion.Euler(0, 0, visualAngle);
        }
        weightAngle += 1;
        
    }   
}
