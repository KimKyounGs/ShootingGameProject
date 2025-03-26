using UnityEngine;

public class Air_Monster_G : MonoBehaviour
{
    public float FlySpeed = 1f;
    public float Radius = 5f;
    private float angle;
    private Vector3 startPosition;
    bool half;

    void Start()
    {
        half = false;
        angle = -90f;
        startPosition = transform.position; // 초기 위치 설정
        Invoke("Afterhalf", 1f);
        //Destroy(gameObject, 2f);
    }

    void Update()
    {
        if(!half)
        {
            MoveInSineWave();
        }
        else if(half)
        {
            MoveInCosineWave();
        }
    }

    void MoveInSineWave()
    {
        // 각도 증가
        angle += FlySpeed * Time.timeScale;

        float radian = angle * Mathf.Deg2Rad;

        // 새로운 위치 계산
        float x = Mathf.Cos(radian) * Radius + startPosition.x;
        float y = Mathf.Sin(radian) * Radius + startPosition.y;

        transform.position = new Vector3(x, y, transform.position.z);
    }

    void MoveInCosineWave()
    {
        // 각도 증가
        angle += FlySpeed * Time.timeScale;

        float radian = angle * Mathf.Deg2Rad;

        // 새로운 위치 계산
        float x = Mathf.Cos(radian) * -Radius + startPosition.x;
        float y = Mathf.Sin(radian) * Radius + startPosition.y;

        transform.position = new Vector3(x, y, transform.position.z);
    }

    void Afterhalf()
    {
        startPosition = new Vector3(2.8f, 0, 0);
        angle = 0;
        half = true;
    }
}