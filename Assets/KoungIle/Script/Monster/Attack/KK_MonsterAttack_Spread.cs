using UnityEngine;

public class KK_MonsterAttack_Spread : MonoBehaviour, IMonsterAttack
{
    public GameObject bullet;
    public Transform bulletPos;
    public float delay = 2f;
    public float spreadAngle = 45f;
    public int startCount = 3;
    public int incrementStep = 1;

    private int currentCount;
    private GameObject player;

    public void StartAttack()
    {
        currentCount = startCount;
        player = GameObject.FindGameObjectWithTag("Player");

        InvokeRepeating("ShootSpread", 0f, delay);
    }

    public void StopAttack()
    {
        CancelInvoke("ShootSpread");
    }

    void ShootSpread()
    {
        if (player == null || !player.activeInHierarchy) // 
        {
            player = GameObject.FindGameObjectWithTag("Player");
            // 플레이어가 없으면 공격 안함
            return;
        }
        

        KK_SoundManager.Instance.PlayFX(10, 0.25f); // 발사 효과음
        // 중심 방향 : 플레이어를 바라보는 벡터
        Vector2 baseDir = (player.transform.position - transform.position).normalized;

        // 중심 각도
        float baseAngle = Mathf.Atan2(baseDir.y, baseDir.x) * Mathf.Rad2Deg;

        for (int i = 0; i < currentCount; i++)
        {
            // -spread/2 ~ +spread/2 사이에 균등 분배
            float offset = ((float)i / (currentCount - 1) - 0.5f) * spreadAngle;

            float angle = baseAngle + offset;
            float rad = angle * Mathf.Deg2Rad;
            Vector2 dir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));

            GameObject tempBullet = Instantiate(bullet, bulletPos.position, Quaternion.identity);
            float visualAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            tempBullet.transform.rotation = Quaternion.Euler(0, 0, visualAngle);
            tempBullet.GetComponent<KK_MBullet>().Move(dir); 
        }
        // 탄 수 증가
        currentCount = Mathf.Min(currentCount + incrementStep, 10); // 최대 10발까지 증가
    }
}
