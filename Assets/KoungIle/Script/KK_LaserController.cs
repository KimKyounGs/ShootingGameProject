using UnityEngine;
using System.Collections;

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

        // 4. 회전된 기준의 오른쪽 방향 기준으로 쌓음
        Vector3 offset = transform.right * bodySegmentLength;

        // 5. 몸통 복제 및 배치.
        StartCoroutine(GrowLaser(bodyCount, offset));

        // 6. 머리 위치 설정
        laserHead.localPosition = offset * (bodyCount + 1);

    }

    public IEnumerator GrowLaser(int bodyCount, Vector3 offset)
    {
        for (int i = 0; i < bodyCount; i++)
        {
            GameObject segment = Instantiate(laserBody.GetChild(0).gameObject, laserBody);
            Vector3 worldPos = transform.position + offset * (i + 1);
            segment.transform.position = worldPos;
            segment.transform.rotation = transform.rotation; // 세그먼트도 회전 정렬

            yield return new WaitForSeconds(0.005f); // 0.005초 간격으로 생성
        }
    }

    public void DestroyAfter(float duration)
    {
        Destroy(gameObject, duration);
    }
}
