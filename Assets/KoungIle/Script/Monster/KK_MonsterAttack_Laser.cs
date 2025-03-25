using System.Collections;
using UnityEngine;

public class KK_MonsterAttack_Laser : MonoBehaviour, IMonsterAttack
{
    public GameObject laserPrefab;          // 레이저 프리팹 (LaserController 포함)
    public GameObject aimLinePrefab;        // 조준선 프리팹
    public Transform firePoint;             // 발사 위치
    public float chargeTime = 0.5f;         // 충전 시간(조준선 보여주는 시간)
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
        if (player == null) yield break;

        // 방향 계산
        Vector2 direction = (player.position - firePoint.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);

        // 1. 조준선 생성
        GameObject aim = Instantiate(aimLinePrefab, firePoint.position, rotation);
        LineRenderer line = aim.GetComponent<LineRenderer>();
        if (line != null)
        {
            line.SetPosition(0, firePoint.position);
            line.SetPosition(1, firePoint.position + (Vector3)(direction * laserLength));
        }

        // 2. 조준선 유지
        yield return new WaitForSeconds(chargeTime);
     
        Destroy(aim);

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
