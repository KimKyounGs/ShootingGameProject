using System.Collections;
using UnityEngine;

public class KK_MonsterAttack_Laser : MonoBehaviour, IMonsterAttack
{
    public GameObject laserPrefab;          // 레이저 프리팹 (LaserController 포함)
    public Transform firePoint;             // 발사 위치
    public float chargeTime = 1f;           // 충전 시간 (예고 효과 등)
    public float laserDuration = 2f;        // 레이저 유지 시간
    public float fireInterval = 5f;         // 다음 레이저 발사 간격
    public float laserLength = 6f;          // 레이저 길이

    private Transform player;

    public void StartAttack()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        InvokeRepeating(nameof(TriggerLaser), 0f, fireInterval);
    }

    void TriggerLaser()
    {
        StartCoroutine(FireLaser());
    }

    IEnumerator FireLaser()
    {
        // 1. 충전 시간 대기 (사운드/이펙트 추가 가능)
        yield return new WaitForSeconds(chargeTime);

        if (player == null) yield break;

        // 2. 방향 계산
        Vector2 direction = (player.position - firePoint.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);

        // 3. 레이저 생성
        GameObject laser = Instantiate(laserPrefab, firePoint.position, rotation);

        // 4. 레이저 길이 설정
        KK_LaserController controller = laser.GetComponent<KK_LaserController>();
        if (controller != null)
        {
            controller.laserLength = laserLength;
            controller.SetupLaser(direction); // 방향도 전달해줌
            controller.DestroyAfter(laserDuration);
        }
    }
}
