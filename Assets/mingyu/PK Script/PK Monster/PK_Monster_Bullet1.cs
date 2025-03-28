using UnityEngine;

public class PK_Monster_Bullet1 : MonoBehaviour
{
    public GameObject target;   // 플레이어 찾기
    public float Speed = 8f;

    private Vector2 targetPosition; // 플레이어의 처음 위치
    private Vector2 dirNo;          // 방향 벡터 (정규화)

    void Start()
    {
        PK_SoundManager.instance.M_Bullet1(); // 총알 소리 재생

        // 플레이어 태그로 대상 찾기
        target = GameObject.FindGameObjectWithTag("Player");

        if (target != null)
        {
            // 플레이어의 처음 위치 저장
            targetPosition = target.transform.position;

            // 방향 계산 및 정규화
            Vector2 dir = targetPosition - (Vector2)transform.position;
            dirNo = dir.normalized;

            // 각도 계산 및 회전 설정
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle+90);
        }
        else
        {
            // 플레이어가 없을 경우 총알 제거
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // 저장된 방향으로 이동 처리
        transform.Translate(dirNo * Speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어와 충돌 시 총알 제거
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        // 화면 밖으로 나가면 총알 제거
        Destroy(gameObject);
    }
}