using UnityEngine;

public class Air_Monster_GD : MonoBehaviour
{
    public float FlySpeed = 0.282f;
    public float Radius = 2.8f;
    private float angle;
    private Vector3 startPosition;
    bool half;

    // 총알 발사에 필요한 부분
    public Transform ms1;
    public GameObject bullet;

    void Start()
    {
        half = false;
        angle = 90f;
        startPosition = transform.position;
        Invoke("Afterhalf", 1f);
        Destroy(gameObject, 2f);
    }

    void Update()
    {
        if (!half)
        {
            MoveInSineWave();
        }
        else if (half)
        {
            MoveInCosineWave();
        }
    }

    void MoveInSineWave()
    {
        // 각도 증가
        angle += FlySpeed * Time.deltaTime;

        float radian = angle * Mathf.Deg2Rad;

        // 새로운 위치 계산
        float x = Mathf.Cos(radian) * -Radius + startPosition.x;
        float y = Mathf.Sin(radian) * Radius + startPosition.y;

        transform.position = new Vector3(x, y, transform.position.z);
    }

    void MoveInCosineWave()
    {
        // 각도 증가
        angle += FlySpeed * Time.deltaTime;

        float radian = angle * Mathf.Deg2Rad;

        // 새로운 위치 계산
        float x = Mathf.Cos(radian) * -Radius + startPosition.x;
        float y = Mathf.Sin(radian) * -Radius + startPosition.y;

        transform.position = new Vector3(x, y, transform.position.z);
    }

    void Afterhalf()
    {
        startPosition = -startPosition;
        angle = 0;
        half = true;
        FireBullet();
    }

    void FireBullet()
    {
        Instantiate(bullet, ms1.position, Quaternion.identity);
    }
}