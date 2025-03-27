using System;
using UnityEngine;

public class KK_PBullet : MonoBehaviour
{
    public int attack = 10;
    public float speed = 4.0f;
    //공격력
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    //충돌처리
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Monster"))
        {

            //몬스터에게 데미지
            collision.gameObject.GetComponent<KK_Monster>().Damage(attack);
            
            //미사일 삭제
            Destroy(gameObject);

        }

        if(collision.CompareTag("Boss"))
        {
            collision.gameObject.GetComponent<KK_Boss>().TakeDamage(attack);
            //미사일 삭제
            Destroy(gameObject);
        }
    }
}
