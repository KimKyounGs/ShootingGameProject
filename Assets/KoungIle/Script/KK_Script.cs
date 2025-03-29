using UnityEngine;

public class KK_Script : MonoBehaviour
{
    // 제목 : vector2.right vs transform.right
    // ✅ 시각적으로 생각하면
    // 벡터	의미	회전에 영향 받음?
    // Vector2.right	씬 기준 오른쪽	❌ 회전에 무관
    // transform.right	오브젝트 기준 오른쪽	✅ 회전에 따라 달라짐

    // 예제
    // transform.Translate(Vector3.right * speed * Time.deltaTime, Space.Self);
    // ➡ 이 코드는:

    // Vector3.right = 기본 방향 (오른쪽 = (1, 0, 0))

    // 하지만, Space.Self를 썼기 때문에
    // → 이 방향은 월드의 오른쪽이 아닌,
    // → 오브젝트가 ‘바라보는 오른쪽 방향’ 기준으로 이동해
    //     ✅ 예시 상황
    // 총알이 45도 회전돼 있다면:

    // 기준	Translate 방향
    // Space.World + Vector3.right	오른쪽으로 직선 이동
    // Space.Self + Vector3.right	총알이 회전한 방향으로 이동 ✅




    // 제목 : 라디안, 도
    // 라디안은 삼각함수 계산할 때 쓰고, 도는 사람이 보기 편하게 쓰기 때문에,
    // Unity에서는 각도 → 방향 벡터 계산 시 라디안으로 바꾸고,
    // 방향 벡터 → 회전 계산 시 도(degree)로 다시 바꾼다.

    // 즉,
    // 👉 Mathf.Atan2() → 라디안 → * Mathf.Rad2Deg → 도
    // 👉 도 → * Mathf.Deg2Rad → 삼각함수에 넣기

    // 그래서 라디안/도 변환은 방향 계산 + 회전 표현을 연결하는 다리라고 보면 돼 💡


}
