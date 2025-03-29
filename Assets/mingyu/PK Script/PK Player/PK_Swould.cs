using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.LightTransport;
using UnityEngine.UI;

public class PK_Swould : MonoBehaviour
{
    public float Speed = 5.0f;
    public Image Swoard_Gage;
    public float a = 0;

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

        if (collision.CompareTag("PK_Monster_Bullet"))
        {
            Destroy(collision.gameObject);
            a += 0.001f;
        }
        if (collision.CompareTag("PK_Boss"))
            {
                PK_Boss boss = collision.gameObject.GetComponent<PK_Boss>();
                if (boss != null)
                {
                    boss.Damage(+1);
                    a += 0.05f;
                }

                PK_MBoss mboss = collision.gameObject.GetComponent<PK_MBoss>();
                if (mboss != null)
                {
                    mboss.Damage(+1);
                    a += 0.05f;
                }
            }


    if (collision.CompareTag("PK_Monster"))
    {

    // PK_Monster 데미지 주기
    PK_Monster monster = collision.gameObject.GetComponent<PK_Monster>();
    if (monster != null)
    {
        monster.Damage(+1);
        a += 0.05f;
    }

    // PK_MShadow 데미지 주기
    PK_MShadow shadow = collision.gameObject.GetComponent<PK_MShadow>();
    if (shadow != null)
    {
        shadow.Damage(+1);
        a += 0.05f;
    }

    // PK_MShadow1 데미지 주기
    PK_MShadow1 shadow1 = collision.gameObject.GetComponent<PK_MShadow1>();
    if (shadow1 != null)
    {
        shadow1.Damage(+1);
        a += 0.05f;
    }

    // PK_MBoos_Bullet3 데미지 주기
    PK_MBoos_Bullet3 bullet3 = collision.gameObject.GetComponent<PK_MBoos_Bullet3>();
    if (bullet3 != null)
    {
        bullet3.Damage(+1);
        a += 0.05f;
    }
    }
}
}
