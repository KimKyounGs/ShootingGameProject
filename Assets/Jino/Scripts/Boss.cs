using UnityEngine;

public class Boss : MonoBehaviour
{
    int flag = 1;
    int speed = 2;

    void Start()
    {
        
    }

    
    void Update()
    {
        if (transform.position.x >= 1)
        {
            flag *= -1;
        }
        if (transform.position.x <= -1)
        {
            flag *= -1;
        }

        transform.Translate(flag * speed * Time.deltaTime, 0, 0);
    }
}
