using System.Collections;
using UnityEngine;

public class PK_Player : MonoBehaviour
{
    public float Speed = 5.0f;

    public int P_HP = 3;

    public GameObject[] bullet;
    public Transform[] pos = null;

    public GameObject Sward;
    public Transform Sward_pos = null;

    public int power = 0;

    [SerializeField]
    private GameObject powerup;


    void Start()
    {
        
    }


    void Update()
    {
        float moveX = Speed * Time.deltaTime * Input.GetAxis("Horizontal");
        float moveY = Speed * Time.deltaTime * Input.GetAxis("Vertical");

        if (Input.GetKeyUp(KeyCode.Space))
        {
            Instantiate(bullet[power], pos[power].position, Quaternion.identity);
        }
       else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Instantiate(Sward, Sward_pos.position, Quaternion.identity);
        }

            transform.Translate(moveX, moveY, 0);



        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
        viewPos.x = Mathf.Clamp01(viewPos.x); //x값을 0이상, 1이하로 제한한다.
        viewPos.y = Mathf.Clamp01(viewPos.y); //y값을 0이상, 1이하로 제한한다.
        Vector3 worldPos = Camera.main.ViewportToWorldPoint(viewPos);//다시월드좌표로 변환
        transform.position = worldPos; //좌표를 적용한다.
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PK_Item"))
        {
            power += 1;
            if (power > 1)
            {
                power = 1;
                //아이템 먹은 처리
                Destroy(collision.gameObject);
            }
        }
    }


    public void Damage(int attack)
    {
        P_HP -= attack;


        if (P_HP <= 0)
        {
            Destroy(gameObject);
        }
    }

}
