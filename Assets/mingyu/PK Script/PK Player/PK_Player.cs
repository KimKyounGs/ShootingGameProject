using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class PK_Player : MonoBehaviour
{
    public float Speed = 5.0f;

    public int P_HP = 3;

    public GameObject[] bullet;
    public Transform[] pos = null;

    public GameObject swould;
    public Transform swouldtos = null;
    public float PK_Swould_Cooltime = 0;

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


        if (Input.GetKeyUp(KeyCode.LeftShift))
        {

            Instantiate(swould, swouldtos.position, Quaternion.identity);
        }



        if (Input.GetKeyUp(KeyCode.Space) && (power == 0))
        {
            Instantiate(bullet[0], pos[0].position, Quaternion.identity);
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
