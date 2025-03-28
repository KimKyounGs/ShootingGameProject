using UnityEngine;

public class MJ_Mbullet_Circle : MonoBehaviour
{
    public int HP = 100;
    public GameObject target;
    public GameObject effect;
    public float speed = 3f;
    Vector2 dir;
    Vector2 dirNo;
    void Update()
    {
        //플레이어 태그로 찾기
        target = GameObject.FindGameObjectWithTag("MJ_Player");

        //A - B  A바라보는 벡터     플레이어 - 미사일 
        dir = target.transform.position - transform.position;
        //방향벡터만 구하기 단위벡터 정규화 노말 1의 크기로 만든다.
        dirNo = dir.normalized;

        transform.Translate(dirNo * speed * Time.deltaTime);
    }

    public void Damage(int attack)
    {
        HP -= attack;

        if (HP <= 0)
        {
            Destroy(gameObject);
        }
    }


    // 충돌 처리 (예: 플레이어와 충돌 시)
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("MJ_Player"))
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
