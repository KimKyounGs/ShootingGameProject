using Unity.VisualScripting;
using UnityEngine;

public class PK_Sworld : MonoBehaviour
{

    void Start()
    {
        
    }


    void Update()
    {
        Destroy(gameObject,0.3f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PK_Monster"))
        {
            //적 데미지
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        if (collision.CompareTag("PK_Monster_Bullet"))
        {
            Destroy(collision.gameObject);
        }

    }
}
