using UnityEngine;

public class MJ_Mbullet_Homing : MonoBehaviour
{
    public GameObject target;  //플레이어
    public GameObject effect;
    public float Speed = 3f;
    Vector2 dir;
    Vector2 dirNo;
    void Start()
    {
        //플레이어 태그로 찾기
        target = GameObject.FindGameObjectWithTag("MJ_Player");

        //A - B  A바라보는 벡터     플레이어 - 미사일 
        dir = target.transform.position - transform.position;
        //방향벡터만 구하기 단위벡터 정규화 노말 1의 크기로 만든다.
        dirNo = dir.normalized;

    }


    void Update()
    {

        transform.Translate(dirNo * Speed * Time.deltaTime);

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "MJ_Player")
        {
            GameObject go = Instantiate(effect, transform.position, Quaternion.identity);
            Destroy(go, 1);

            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
