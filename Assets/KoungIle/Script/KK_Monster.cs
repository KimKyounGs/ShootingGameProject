using UnityEngine;

public class KK_Monster : MonoBehaviour
{
    public int HP = 100;
    public float speed = 3;
    public float delay = 1f; // shoot delay
    public Transform bulletPos;
    public GameObject bullet;
    public int spawnLocation;
    //아이템 가져오기
    public GameObject Item = null;

    
    void Start()
    {
        //한번함수호출
        Invoke("CreateBullet", delay);
    }

    void CreateBullet()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
        //재귀호출
        Invoke("CreateBullet", delay);
    }

    void Update()
    {
        //아래 방향으로 움직여라
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    //미사일에 따른 데미지 입는 함수
    public void Damage(int attack)
    {
        HP -= attack;

        if(HP <=0)
        {
            // ItemDrop();
            Destroy(gameObject);
        }
    }

    // public void ItemDrop()
    // {
    //     //아이템 생성
    //     Instantiate(Item, transform.position, Quaternion.identity);
    // }

}