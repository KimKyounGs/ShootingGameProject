using UnityEngine;

public class PK_Monster_Bullet2 : MonoBehaviour
{
    public GameObject target;   //플레이어 찾기
    public float Speed = 3f;
    Vector2 dir;
    Vector2 dirNo;


    void Start()
    {
        //플레이어를 찾아서 생성된 그 위치만 쫒기
        //플레이어 태그로 찾기
        target = GameObject.FindGameObjectWithTag("Player");
        //A - B A를 바라보는 벡터      플레이어 - 미사일
        dir = target.transform.position - transform.position;
        //방향벡터만 구하기 단위벡터 정규화 노말 1의 크기로 만든다.
        dirNo = dir.normalized;
    }


    void Update()
    {
        transform.Translate(dirNo * Speed * Time.deltaTime);


        //////Update에다가 넣으면 플레이어를 계속 찾는다
        ////플레이어 태그로 찾기
        //target = GameObject.FindGameObjectWithTag("Player");
        ////A - B A를 바라보는 벡터      플레이어 - 미사일
        //dir = target.transform.position - transform.position;
        ////방향벡터만 구하기 단위벡터 정규화 노말 1의 크기로 만든다.
        //dirNo = dir.normalized;


        ////유니티가 제공하는 유도      3D에서는 다양하게 불가능하니 벡터를 잘 알자!
        //transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
