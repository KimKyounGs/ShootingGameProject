using TMPro;
using UnityEngine;

public class PK_Boss_Bullet3 : MonoBehaviour
{

    public GameObject target;   //플레이어 찾기
    public float Speed = 3f;
    Vector2 dir;
    Vector2 dirNo;

    void Start()
    {

        target = GameObject.FindGameObjectWithTag("Player");

        if (target != null)
        {
            Vector2 dir = target.transform.position - transform.position;
            dirNo = dir.normalized;
        }
        else
        {
            Destroy(gameObject); // 타겟이 없으면 총알 삭제
        }

    }


    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
        }

        transform.Translate(dirNo * Speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
