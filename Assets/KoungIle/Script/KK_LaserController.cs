using UnityEngine;

public class KK_LaserController : MonoBehaviour
{
public Transform laserHead;
    public Transform laserBody;
    public Transform laserTail;

    public float laserLength = 5f;          // 전체 레이저 길이
    public float bodySegmentLength = 0.5f;    // 몸통 1개 길이 (스프라이트 기준)

    public void SetupLaser(Vector2 direction)
    {
        // 1. 꼬리 위치 고정 (시작점)
        laserTail.localPosition = Vector3.zero;

        // 2. 몸통 개수 계산
        int bodyCount = Mathf.FloorToInt(laserLength / bodySegmentLength);

        // 3. 기존 몸통 제거
        foreach (Transform child in laserBody)
        {
            if (child != laserBody.GetChild(0)) // 복제 템플릿은 유지
                Destroy(child.gameObject);
        }

        // 4. 방향 벡터 기준 offset 계산
        Vector3 offset = direction.normalized * bodySegmentLength;

        // 5. 몸통 복제 및 배치
        for (int i = 0; i < bodyCount; i++)
        {
            GameObject segment = Instantiate(laserBody.GetChild(0).gameObject, laserBody);
            segment.transform.localPosition = offset * (i + 1);
        }

        // 6. 머리 위치 설정
        laserHead.localPosition = offset * (bodyCount + 1);
    }

    public void DestroyAfter(float duration)
    {
        Destroy(gameObject, duration);
    }
}
