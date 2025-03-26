using UnityEngine;

public class PK_Monster_Bullet1 : MonoBehaviour
{
    public GameObject target;   //플레이어 찾기
    public float Speed = 8f;
    Vector2 dir;
    Vector2 dirNo;


    void Start()
    {

        target = GameObject.FindGameObjectWithTag("Player");

        dir = target.transform.position - transform.position;

        dirNo = dir.normalized;
    }


    void Update()
    {
        transform.Translate(dirNo * Speed * Time.deltaTime);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}