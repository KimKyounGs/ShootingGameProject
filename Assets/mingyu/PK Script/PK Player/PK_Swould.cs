using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.LightTransport;
using UnityEngine.UI;

public class PK_Swould : MonoBehaviour
{
    public float Speed = 5.0f;
    public Image Swoard_Gage;
    public int a = 0;

    private void Start()
    {
    }

    void Update()
    {
        if (a >= 0)
        {
            Swoard_Gage.fillAmount += a;
            a = 0;
        }
    }



    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PK_Monster"))
        {
            //몬스터 데미지 주기
            collision.gameObject.GetComponent<PK_Monster>().Damage(+1);
        }

        if (collision.CompareTag("PK_Boss"))
        {
            //몬스터 데미지 주기
            collision.gameObject.GetComponent<PK_Boss>().Damage(+1);
        }

        if (collision.CompareTag("PK_Monster_Bullet"))
        {
            Destroy(collision.gameObject);
            a += 1;
        }
    }
}
