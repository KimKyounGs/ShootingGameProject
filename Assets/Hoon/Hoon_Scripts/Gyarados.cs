using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Gyarados : MonoBehaviour
{
    public static Gyarados instance;
    private CapsuleCollider2D CapsuleCollider;
    private Vector2 horizontalSize = new Vector2(1.7f, 1.1f);  // 가로로 긴 히트박스
    private Vector2 verticalSize = new Vector2(0.5f, 1.4f);    // 세로로 긴 히트박스
    private Vector2 lastMoveDirection = Vector2.up; // 마지막으로 본 방향


    Animator ani;
    public GameObject[] Bullet;
    public GameObject waterfall;
    public float effectDashInterval = 0.2f; // 이펙트 생성 간격

    public Transform pos = null;

    [Header("캐릭터 설정")]
    public float Speed = 5f;
    public float bulletCooldown = 0.15f;
    public float skillCooldown = 10f;
    public float dashCooldown = 5f;
    
    public bool CanShoot = true;
    public bool CanSkill = true;
    public bool CanDash = true;

    public bool isEvolved = true;
    public bool Firing = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else Destroy(gameObject);
    }
    IEnumerator DragonRage()
    {
        Hoon_AudioManager.instance.SFXDragonRage1();
        yield return new WaitForSeconds(0.5f);
        Instantiate(Bullet[1], pos.position, Quaternion.identity);
    }
    IEnumerator Waterfall()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        
        if (moveX != 0 || moveY != 0)
        {
            lastMoveDirection = new Vector2(moveX, moveY).normalized;
        }

        DisableHitbox();
        
        float dashSpeed = Speed * 2f;
        float dashTime = 0.2f;
        float startTime = Time.time;
        float lastEffectTime = startTime;

        while (Time.time - startTime < dashTime)
        {
            // 현재 위치에 이펙트 생성
            if (Time.time - lastEffectTime >= effectDashInterval)
            {
                GameObject effect = Instantiate(waterfall, transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0), Quaternion.identity);
                Destroy(effect, 0.5f); // 0.5초 후 이펙트 제거
                lastEffectTime = Time.time;
            }

            transform.Translate(lastMoveDirection * dashSpeed * Time.deltaTime);
            yield return null;
        }
        
        EnableHitbox();
        yield return new WaitForSeconds(0.3f);
    }


    IEnumerator BulletCooldown()
    {
        yield return new WaitForSeconds(bulletCooldown);
        CanShoot = true;
    }

    IEnumerator SkillCooldown()
    {
        yield return new WaitForSeconds(skillCooldown);
        CanSkill = true;
    }

    IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(dashCooldown);
        CanDash = true;
    }

    void Start()
    {
        ani = GetComponent<Animator>();
        CapsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("PK_Item"))
        {
            Destroy(collision.gameObject);
            // GameObject go = Instantiate(PowEffect, transform.position, Quaternion.identity);
            // Destroy(go, 1);
        }
    }
    
    public void DisableHitbox()
    {
        CapsuleCollider.enabled = false;
    }

    public void EnableHitbox()
    {
        CapsuleCollider.enabled = true;
    }

    void HorizontalHitbox()
    {
        CapsuleCollider.direction = CapsuleDirection2D.Horizontal;
        CapsuleCollider.size = horizontalSize;
    }
    void VerticalHitbox()
    {
        CapsuleCollider.direction = CapsuleDirection2D.Vertical;
        CapsuleCollider.size = verticalSize;
    }

    void Update()
    {
        ani.SetBool("Down", false);
        VerticalHitbox();

        if(Time.timeScale == 1)
        {
            #region 이동
            float moveX = Input.GetAxis("Horizontal") * Speed * Time.deltaTime;
            float moveY = Input.GetAxis("Vertical") * Speed * Time.deltaTime;

            if(Input.GetAxis("Horizontal") <= -0.1)
            {
                ani.SetBool("Left", true);
                HorizontalHitbox();
            }    
            else
                ani.SetBool("Left", false);

            if(Input.GetAxis("Horizontal") >= 0.1)
            {
                ani.SetBool("Right", true);
                HorizontalHitbox();
            }
            else
                ani.SetBool("Right", false);

            if(Input.GetAxis("Vertical") <= -0.1)
            {
                ani.SetBool("Down", true);
                VerticalHitbox();
            }
            else
                ani.SetBool("Down", false);    
            
            transform.Translate(moveX, moveY, 0);
            #endregion

            if(Input.GetKeyDown(KeyCode.Q) && CanSkill == true) 
            {
                CanSkill = false;
                StartCoroutine(SkillCooldown());
                StartCoroutine(DragonRage());
            }

            if(Input.GetKeyDown(KeyCode.LeftShift) && CanDash == true) 
            {
                CanDash = false;
                StartCoroutine(DashCooldown());
                Hoon_AudioManager.instance.SFXWaterfall();
                StartCoroutine(Waterfall());
            }

            if(Input.GetKey(KeyCode.Space) && CanShoot == true)
            {
                ani.SetBool("Firing", true);
                ani.SetBool("Down", false);
                ani.SetBool("Right", false);
                ani.SetBool("Left", false);
                VerticalHitbox();
                Shoot();
            }
            else if (Input.GetKey(KeyCode.Space))
            {
                ani.SetBool("Firing", true);
                VerticalHitbox();
            }
            else { ani.SetBool("Firing", false); }
            }
           
        //캐릭터의 월드 좌표를 뷰포트 좌표계로 변환해준다.
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
        viewPos.x = Mathf.Clamp01(viewPos.x); //x값을 0이상, 1이하로 제한한다.
        viewPos.y = Mathf.Clamp01(viewPos.y); //y값을 0이상, 1이하로 제한한다.
        Vector3 worldPos = Camera.main.ViewportToWorldPoint(viewPos);//다시월드좌표로 변환
        transform.position = worldPos; //좌표를 적용한다.
    }

    public void Shoot()
    {
        StartCoroutine(BulletCooldown());
        CanShoot = false;
        Hoon_AudioManager.instance.SFXDragonBreath();
        Instantiate(Bullet[0], pos.transform.position, Quaternion.identity);
    }




}

