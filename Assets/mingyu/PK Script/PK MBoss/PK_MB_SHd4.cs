using UnityEngine;

public class PK_MB_SHd4 : MonoBehaviour
{
    
    float cool_time = 0;


    
    void Update()
    {
        transform.Translate(-6* Time.deltaTime, 6* Time.deltaTime, 0);

        if(cool_time >= 4f)
        {
            Destroy(gameObject);
        }
    }
}
