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

    public GameObject Player_LV2;


    public Transform swouldtos = null;
    public float PK_Swould_Cooltime = 0;
    public Image Swoard_Gage;
    public Image Swoard_cool;
    public GameObject Swould;
    public Animator Swould_Ani;

    public GameObject SPSwould;



    public Image Missile_Gage;
    public GameObject Missile;
    public Transform[] Mis_pos = null;


    public int power = 0;

    [SerializeField]
    private GameObject powerup;



    void Start()
    {
        PK_Swould_Cooltime = 2;
    }

    public bool swould_sound = true;

    void Update()
    {
        float moveX = Speed * Time.deltaTime * Input.GetAxis("Horizontal");
        float moveY = Speed * Time.deltaTime * Input.GetAxis("Vertical");

        PK_Swould_Cooltime += Time.deltaTime;

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            if (PK_Swould_Cooltime >= 2)
            {
                Swould.gameObject.SetActive(true);
                Swould_Ani.SetBool("Swould", true);
                PK_SoundManager.instance.PlayerSwould();
                PK_Swould_Cooltime = -2;
                swould_sound = true;
                Swoard_cool.fillAmount = 1;
            }
        }


        if (PK_Swould_Cooltime <= 1f && PK_Swould_Cooltime > -1.1f)
        {
            Swould.gameObject.SetActive(false);
        }

        if (PK_Swould_Cooltime >= 2)
        {
            Swoard_cool.fillAmount = 0;
        }


        if (Input.GetKeyUp(KeyCode.Z) && Swoard_Gage.fillAmount == 1)
        {
            SPSwould.gameObject.SetActive(true);
            Swoard_Gage.fillAmount = 0;
        }


        if (Input.GetKeyUp(KeyCode.C) && Missile_Gage.fillAmount == 1)
        {
            Instantiate(Missile, Mis_pos[0].position, Quaternion.identity); // i 써서 5까지 만들고 딜레이 및 소리 넣기   유도미사일 스크립트 넣고  플레이어에 유도미사일 지정해주기
            Missile_Gage.fillAmount = 0;
        }


        if (Input.GetKeyUp(KeyCode.Space))
        {
            Instantiate(bullet[0], pos[0].position, Quaternion.identity);
            
            if (power >= 3)
            {
                Player_LV2.SetActive(true);
                Instantiate(bullet[1], pos[1].position, Quaternion.identity);
                Instantiate(bullet[1], pos[2].position, Quaternion.identity);
            }
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
            P_HP += 1;

            if (power > 1)
            {
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

        if (P_HP >= 4)
        {
            P_HP = 3;
        }

    }

}
