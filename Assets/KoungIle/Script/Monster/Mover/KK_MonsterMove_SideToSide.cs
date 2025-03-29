using UnityEngine;

public class KK_MonsterMove_SideToSide : MonoBehaviour, IMonsterMove
{
    public float moveSpeed = 2f;      // 이동 속도
    public float moveRange = 3f;      // 이동 범위 (가로)
    private Vector2 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    public void Move()
    {
        float offset = Mathf.PingPong(Time.time * moveSpeed, moveRange * 2) - moveRange;
        transform.position = new Vector2(startPos.x + offset, startPos.y);
    }
}
