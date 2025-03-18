using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PK_Swould : MonoBehaviour
{
    public Image Swoard_Gage;
    public bool starts;

    private void Start()
    {
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
            Swoard_Gage.fillAmount += 10;
            
        }

        if (collision.CompareTag("PK_Boss"))
        {
            //몬스터 데미지 주기
            collision.gameObject.GetComponent<PK_Boss>().Damage(+1);
            Swoard_Gage.fillAmount += 10;
        }

        if (collision.CompareTag("PK_Monster_Bullet"))
        {
            Destroy(collision.gameObject);
            Swoard_Gage.fillAmount += 10;
        }
    }
}
