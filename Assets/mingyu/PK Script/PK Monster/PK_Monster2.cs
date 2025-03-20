using UnityEngine;

public class PK_Monster2 : MonoBehaviour
{
    public float Speed = 5f;

    public GameObject target;

    Vector2 dir;
    Vector2 dirNo;


    void Start()
    {
        
    }


    void Update()
    {
        transform.Translate(dirNo * Speed * Time.deltaTime);


        ////Update에다가 넣으면 플레이어를 계속 찾는다
        //플레이어 태그로 찾기
        target = GameObject.FindGameObjectWithTag("Player");
        //A - B A를 바라보는 벡터      플레이어 - 미사일
        dir = target.transform.position - transform.position;
        //방향벡터만 구하기 단위벡터 정규화 노말 1의 크기로 만든다.
        dirNo = dir.normalized;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
