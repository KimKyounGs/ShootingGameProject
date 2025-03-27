using UnityEngine;

public class PK_Missile : MonoBehaviour
{
    public GameObject target1;   // 적 찾기
    public GameObject target2;   // 보스 찾기

    public float Speed = 8f;

    private Vector2 dirNo;       // 방향 벡터 (정규화)
    private float angle;         // 회전 각도

    public GameObject effect;
    
    void Start()
    {
       
    }

    void Update()
    {
        // 타겟 설정 (우선순위: target1 -> target2)
        GameObject target = FindTarget();

         if (target != null)
        {
            // 방향 계산 및 정규화
            Vector2 dir = (Vector2)(target.transform.position - transform.position);
            dirNo = dir.normalized;

            // 각도 계산
            angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            // 미사일의 회전 방향 설정
            transform.rotation = Quaternion.Euler(0f, 0f, angle-90);
        }
        else
        {
            // 타겟이 없을 경우 미사일 제거
            Destroy(gameObject);
        }

        // 이동 처리
        transform.Translate(dirNo * Speed * Time.deltaTime, Space.World);
    }

    private GameObject FindTarget()
    {
        // 적(target1)과 보스(target2) 중 하나를 타겟으로 설정
        if (GameObject.FindGameObjectWithTag("PK_Monster") != null)
        {
            return GameObject.FindGameObjectWithTag("PK_Monster");
        }
        else if (GameObject.FindGameObjectWithTag("PK_Boss") != null)
        {
            return GameObject.FindGameObjectWithTag("PK_Boss");
        }
        return null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 적 또는 보스와 충돌 시 미사일 제거
        if (collision.CompareTag("PK_Monster"))
        {
            //이펙트생성
            GameObject go = Instantiate(effect, transform.position, Quaternion.identity);
            //1초뒤에 지우기
            Destroy(go, 1);

            collision.gameObject.GetComponent<PK_Monster>().Damage(+3);
            Destroy(gameObject);
        }

        if (collision.CompareTag("PK_Boss"))
        {
            //이펙트생성
            GameObject go = Instantiate(effect, transform.position, Quaternion.identity);
            //1초뒤에 지우기
            Destroy(go, 1);

            collision.gameObject.GetComponent<PK_Boss>().Damage(+3);
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        // 화면 밖으로 나가면 미사일 제거
        Destroy(gameObject);
    }
}
