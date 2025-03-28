using UnityEngine;

public class MJ_Monster : MonoBehaviour
{
    public int HP = 100;
    public float Speed = 3;
    public float Delay = 1f;
    public int x = 1;
    public int y = 1;
    public int item_max_pb = 100;
    public AudioClip death;


    public Transform ms1;
    public Transform ms2;
    public GameObject bullet;
    public GameObject item = null;

    void Start()
    {
        Invoke("CreateBullet", Delay);
    }
    void CreateBullet()
    {
        Instantiate(bullet, ms1.position, Quaternion.identity);
        if (ms2 != null)
        {
            Instantiate(bullet, ms2.position, Quaternion.identity);
        }

        Invoke("CreateBullet", Delay);
    }

    void Update()
    {
        Vector3 vector = new Vector3(x, y, 0);

        transform.Translate(vector * Speed * Time.deltaTime);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    //미사일에 따른 데미지 입는 함수
    public void Damage(int attack)
    {
        HP -= attack;

        if (HP <= 0)
        {
            ItemDrop();
            Destroy(gameObject);
        }
    }

    public void ItemDrop()
    {
        int prob = Random.Range(1, 101);

        if (prob < item_max_pb)
        {
            Instantiate(item, transform.position, Quaternion.identity);
        }
    }

}
