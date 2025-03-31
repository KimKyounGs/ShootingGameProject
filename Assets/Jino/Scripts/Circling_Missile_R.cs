using UnityEngine;

public class Circling_Missile_R : MonoBehaviour
{
    public float FlySpeed = 1f;
    public float Radius = 5f;
    private float angle;
    private Vector3 startPosition;
    public bool isDown = false;

    public GameObject Effect_1;

    void Start()
    {
        angle = -90f;
        startPosition = transform.position;
        Destroy(gameObject, 2f);
    }

    
    void Update()
    {
        if (!isDown)
        {
            Circling();
        }

        if (isDown)
        {
            Circling_Down();
        }
    }

    void Circling()
    {
        //각도 증가
        angle += FlySpeed * Time.deltaTime;

        //각도를 라디안으로 변환
        float radian = angle * Mathf.Deg2Rad;

        //새로운 위치 계산
        float x = Mathf.Cos(radian) * -Radius + startPosition.x;
        float y = Mathf.Sin(radian) * Radius + startPosition.y;

        transform.position = new Vector3(x, y, transform.position.z);
    }

    void Circling_Down()
    {
        // 각도 증가
        angle += FlySpeed * Time.deltaTime;

        // 각도를 라디안으로 변환
        float radian = angle * Mathf.Deg2Rad;

        // 새로운 위치 계산
        float x = Mathf.Cos(radian) * -Radius + startPosition.x; // 반지름 방향 수정
        float y = Mathf.Sin(radian) * -Radius + startPosition.y;

        transform.position = new Vector3(x, y, transform.position.z);
    }

    public void SetDown(bool Down)
    {
        isDown = Down;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameObject go = Instantiate(Effect_1, transform.position, Quaternion.identity);
            Destroy(go, 1f);
            Destroy(gameObject);
        }
    }
}
