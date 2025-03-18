using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.LightTransport;
using UnityEngine.UI;

public class PK_Swould : MonoBehaviour
{
    public float Speed = 5.0f;

    private void Start()
    {

        Destroy(gameObject, 3);
    }

    void Update()
    {
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PK_Monster"))
        {
            //몬스터 데미지 주기
            collision.gameObject.GetComponent<PK_Monster>().Damage(+1);
            gameObject.GetComponent<PK_S_Gage1>().S_Gage(+10);

        }

        if (collision.CompareTag("PK_Boss"))
        {
            //몬스터 데미지 주기
            collision.gameObject.GetComponent<PK_Boss>().Damage(+1);
            gameObject.GetComponent<PK_S_Gage1>().S_Gage(+10);
        }

        if (collision.CompareTag("PK_Monster_Bullet"))
        {
            Destroy(collision.gameObject);
            gameObject.GetComponent<PK_S_Gage1>().S_Gage(+10);
        }
    }
}
