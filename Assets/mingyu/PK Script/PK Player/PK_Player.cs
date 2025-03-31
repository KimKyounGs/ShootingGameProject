using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class PK_Player : MonoBehaviour
{
    // 플레이어 이동 속도
    public float Speed = 5.0f;

    // 플레이어 체력
    public int P_HP = 10000;

    // 총알 및 발사 위치
    public GameObject[] bullet;
    public Transform[] pos = null;

    // 플레이어 레벨 2 상태
    public GameObject Player_LV2;

    // 검 관련 변수
    public Transform swouldtos = null;
    public float PK_Swould_Cooltime = 0; // 검 스킬 쿨타임
    public Image Swoard_Gage; // 검 게이지 UI
    public Image Swoard_cool; // 검 쿨타임 UI
    public GameObject Swould; // 검 오브젝트
    public Animator Swould_Ani; // 검 애니메이터

    // 특수 검 스킬
    public GameObject SPSwould;

    // 미사일 관련 변수
    public Image Missile_Gage; // 미사일 게이지 UI

    public GameObject Missile; // 미사일 오브젝트

    // 플레이어 파워
    public int power = 0;

    // 파워업 아이템
    [SerializeField]
    private GameObject powerup;

    // 검 소리 재생 여부
    public bool swould_sound = true;
    // 미사일 소리 재생 여부
    public bool Missile_sound = true;

    void Start()
    {
        // 검 스킬 초기 쿨타임 설정
        PK_Swould_Cooltime = 2;
    }

    void Update()
    {
        Missile_Gage.fillAmount += Time.deltaTime / 20; // 미사일 게이지 증가

        if (Missile_Gage.fillAmount >= 1 && Missile_sound == true) // 미사일 게이지가 1 이상일 때
        {
            PK_SoundManager.instance.M_Gage_Full(); // 미사일 준비완료 소리 재생
            Missile_sound = false; // 소리 재생 플래그 해제
        }

        HandleMovement(); // 플레이어 이동 처리
        HandleSwould(); // 검 스킬 처리
        HandleSpecialAttacks(); // 특수 공격 처리
        ClampPlayerPosition(); // 화면 밖으로 나가지 않도록 위치 제한
    }

    // 플레이어 이동 처리
    private void HandleMovement()
    {
        float moveX = Speed * Time.deltaTime * Input.GetAxis("Horizontal");
        float moveY = Speed * Time.deltaTime * Input.GetAxis("Vertical");
        transform.Translate(moveX, moveY, 0);
    }

    // 검 스킬 처리
    private void HandleSwould()
    {
        PK_Swould_Cooltime += Time.deltaTime;

        // 검 스킬 발동
        if (Input.GetKeyUp(KeyCode.LeftShift) && PK_Swould_Cooltime >= 2)
        {
            Swould.gameObject.SetActive(true);
            Swould_Ani.SetBool("Swould", true);
            PK_SoundManager.instance.PlayerSwould(); // 검 소리 재생
            PK_Swould_Cooltime = -2;
            swould_sound = true;
            Swoard_cool.fillAmount = 1;
        }

        // 검 스킬 종료
        if (PK_Swould_Cooltime <= 1f && PK_Swould_Cooltime > -1.1f)
        {
            Swould.gameObject.SetActive(false);
        }

        // 검 쿨타임 UI 초기화
        if (PK_Swould_Cooltime >= 2)
        {
            Swoard_cool.fillAmount = 0;
        }
    }

    // 특수 공격 처리
    private void HandleSpecialAttacks()
    {
        // 특수 검 스킬 발동
        if (Input.GetKeyUp(KeyCode.Q) && Swoard_Gage.fillAmount == 1)
        {
            SPSwould.gameObject.SetActive(true);
            Swoard_Gage.fillAmount = 0.2f;
        }

        // 미사일 발사
        if (Input.GetKeyUp(KeyCode.E) && Missile_Gage.fillAmount >= 0.5f)
        {
            Missile.gameObject.SetActive(true);
            Missile_Gage.fillAmount -= 0.5f;
            PK_SoundManager.instance.P_Missile();
            Missile_sound = true; // 미사일 소리 재생 플래그 설정
        }

        // 기본 총알 발사
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Instantiate(bullet[0], pos[0].position, Quaternion.identity);
            PK_SoundManager.instance.P_Bullet(); // 총알 발사 소리 재생

            // 파워가 3 이상일 때 추가 총알 발사
            if (power >= 3)
            {
                Player_LV2.SetActive(true);
                Instantiate(bullet[1], pos[1].position, Quaternion.identity);
                Instantiate(bullet[1], pos[2].position, Quaternion.identity);
            }
        }
    }

    // 화면 밖으로 나가지 않도록 위치 제한
    private void ClampPlayerPosition()
    {
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
        viewPos.x = Mathf.Clamp01(viewPos.x);
        viewPos.y = Mathf.Clamp01(viewPos.y);
        transform.position = Camera.main.ViewportToWorldPoint(viewPos);
    }

    // 아이템 충돌 처리
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PK_Item"))
        {
            power++;
            P_HP = Mathf.Min(P_HP + 1, 3); // 체력 최대값 제한

            if (power > 1)
            {
                PK_SoundManager.instance.UPGRADE();
                Destroy(collision.gameObject); // 아이템 제거
            }
        }
    }

    // 플레이어 데미지 처리
    public void Damage(int attack)
    {
        P_HP -= attack;

        if (P_HP <= 0)
        {
            Destroy(gameObject); // 플레이어 제거
        }
    }
}
