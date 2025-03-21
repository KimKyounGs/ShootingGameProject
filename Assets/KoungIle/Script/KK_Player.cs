using System.Linq;
using UnityEngine;

public class KK_Player : MonoBehaviour
{   public float speed = 5f;

    public GameObject[] bullet;  //총알배열
    public Transform bulletPos = null;
    public int power = 0;

    [SerializeField]
    private float attackCoolTime;
    [SerializeField]
    private float attackMaxCoolTime = 0.5f;

    private Vector2 minBounds;
    private Vector2 maxBounds;
    // 총

    [SerializeField]
    private GameObject powerUpEffect;


    Animator ani; //애니메이터를 가져올 변수

    //레이져
    public GameObject lazer;
    public float gValue = 0;

    void Start()
    {
        ani = GetComponent<Animator>();
        // 화면의 경계를 설정
        Camera cam = Camera.main;
        Vector3 bottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, 0));

        minBounds = new Vector2(bottomLeft.x, bottomLeft.y);
        maxBounds = new Vector2(topRight.x, topRight.y);
        
    }

    void Update()
    {
        Move();
        Shoot();

    }

    void Move()
    {
        // 플레이어 이동
        float moveX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float moveY = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        // -1   0   1
        if (Input.GetAxis("Horizontal") <= -0.05f)
            ani.SetBool("left", true);
        else
            ani.SetBool("left", false);


        if (Input.GetAxis("Horizontal") >= 0.05f)
            ani.SetBool("right", true);
        else
            ani.SetBool("right", false);

        Vector3 newPosition = transform.position + new Vector3(moveX, moveY, 0);

        // 경계를 벗어나지 않도록 위치 제한
        newPosition.x = Mathf.Clamp(newPosition.x, minBounds.x, maxBounds.x);
        newPosition.y = Mathf.Clamp(newPosition.y, minBounds.y, maxBounds.y);

        transform.position = newPosition;

    }

    void Shoot()
    {
        //스페이스
        if(Input.GetKey(KeyCode.Space) && attackCoolTime >= attackMaxCoolTime)
        {
            //프리팹 위치 방향 넣고 생성
            Instantiate(bullet[power], bulletPos.position, Quaternion.identity);
            attackCoolTime = 0;
        }
        attackCoolTime += Time.deltaTime;
    }

    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     if(collision.CompareTag("Item"))
    //     {
    //         power += 1;

    //         if (power >= bullet.Length)
    //             power = 3;
    //         else
    //         {
    //             //파워업
    //             GameObject go = Instantiate(powerUpEffect, transform.position, Quaternion.identity);
    //             Destroy(go, 1);
    //         }

    //         //아이템 먹은 처리
    //         Destroy(collision.gameObject);
    //     }
    // }
}
