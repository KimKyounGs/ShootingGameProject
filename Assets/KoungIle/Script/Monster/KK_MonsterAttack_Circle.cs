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

    void ShootCircle()
    {
        for (int i = 0; i < bulletCount; i ++)
        {
            // 각도를 균등하게 나누고, 보정 각도 추가
            float angle = i * 360f / bulletCount + weightAngle;
            float rad = angle * Mathf.Deg2Rad;

            // 방향 벡터 계산
            Vector2 direction = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)).normalized;
            GameObject b = Instantiate(bullet, transform.position, Quaternion.identity);
            b.GetComponent<KK_MBullet>().Move(direction);
        }
        weightAngle += 1;
    }
}
