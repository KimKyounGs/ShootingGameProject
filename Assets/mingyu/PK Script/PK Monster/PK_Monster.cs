using UnityEngine;

public class PK_Monster : MonoBehaviour
{
    public float Speed = 3;
    public float Delay = 1f;
    public int M_HP = 5;

    public Transform ms1;
    public GameObject bullet;
    //아이템 가져오기
    public GameObject Item = null;

    public float delay = 0;

    Animator pAnimator;

    void Start()
    {
        pAnimator = GetComponent<Animator>();
    }


    void Update()
    {
        //아래 방향으로 움직여라
        transform.Translate(Vector3.down * Speed * Time.deltaTime);

        delay += Time.deltaTime;

        if (delay > 1f)
        {
            pAnimator.SetBool("Attack", true);

            if (delay > 1.5f)
            {
                pAnimator.SetBool("Attack", false);

                delay = 0;
            }
        }


    }



    public void Shoot()
    {
        Instantiate(bullet, ms1.position, Quaternion.identity);
    }






    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }



    //미사일에 따른 데미지 입는 함수
    public void Damage(int attack)
    {
        M_HP -= attack;
        if (M_HP <= 0)
        {
            ItemDrop();
            Destroy(gameObject);
        }
    }






    public int item_Random = 0;

    public void ItemDrop()
    {
        item_Random = Random.Range(0, 11);

        if (item_Random >= 9)
        {
            //아이템 생성
            Instantiate(Item, transform.position, Quaternion.identity);
        }
    }
}
