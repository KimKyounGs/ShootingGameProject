using UnityEngine;

public class PK_Monster_Bullet1 : MonoBehaviour
{
    public GameObject target;   //플레이어 찾기
    public float Speed = 8f;
    Vector2 dir;
    Vector2 dirNo;

    float angle;

    void Start()
    {

        target = GameObject.FindGameObjectWithTag("Player");

        dir = target.transform.position - transform.position;

        dirNo = dir.normalized;


        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    }


    void Update()
    {
        transform.Translate(dirNo * Speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }


    private void OnBecameInvisible()
    {
        //자기 자신 지우기
        Destroy(gameObject);
    }
}