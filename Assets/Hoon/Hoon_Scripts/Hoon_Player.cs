using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
public class Hoon_Player : MonoBehaviour
{
    
    public static Hoon_Player instance;
    public CapsuleCollider2D CapsuleCollider;
    public Vector2 horizontalSize = new Vector2(1.7f, 1.1f);  // 가로로 긴 히트박스
    public Vector2 verticalSize = new Vector2(0.5f, 1.4f);    // 세로로 긴 히트박스

    public SpriteRenderer sr;

    public Animator ani;
    public Transform pos = null;
    public Vector2 lastMoveDirection = Vector2.up;
    public GameObject[] Bullet;
    public float effectDashInterval = 0.2f; // 이펙트 생성 간격

    [Header("캐릭터 설정")]
    public float HP = 20;
    public bool HPDanger = false;
    public float maxHP = 20;
    public float speed = 3f;
    public float bulletCooldown = 0.5f;
    public float dashCooldown = 8f;
    
    public bool CanShoot = true;
    public bool CanDash = true;

    public bool isEvolved = false;
    public bool Firing = false;

    [Header("UI")]
    public Image dashUI;
    public Image HPUI;
    public TMP_Text dashNameUI;
    public TMP_Text HPLeftUI;

    void Awake()
    {
        if (instance == null || instance == this)  // 현재 객체가 instance이면 유지
        {
            instance = this;
        }
    }
    protected virtual void Shoot()
    {
        StartCoroutine(BulletCooldown());
        CanShoot = false;
        Instantiate(Bullet[0], pos.transform.position, Quaternion.identity);
    }



    protected virtual void Dash()
    {
        StartCoroutine(DashCooldown());
        CanDash = false;
    }

    IEnumerator BulletCooldown()
    {
        yield return new WaitForSeconds(bulletCooldown);
        CanShoot = true;
    }

    IEnumerator DashCooldown()
    {
        dashUI.fillAmount = 0;
        dashNameUI.color = new Color(1, 1, 1, 0.5f);

        float elapsedTime = 0f;
        
        while (elapsedTime < dashCooldown)
        {
            elapsedTime += Time.deltaTime;
            float fillAmount = elapsedTime / dashCooldown;
            dashUI.fillAmount = fillAmount;
            yield return null;
        }
        
        // 쿨타임 회복 시 효과
        dashUI.fillAmount = 1f;
        dashNameUI.color = new Color(1, 1, 1, 1f);
        
        // 효과음 재생
        Hoon_AudioManager.instance.SFXcooldownRecover();
        
        // UI 점프 애니메이션
        StartCoroutine(UIJumpAnimation(dashUI.gameObject));
        
        CanDash = true;
    }

    // UI 점프 애니메이션을 위한 새로운 코루틴
    public IEnumerator UIJumpAnimation(GameObject uiElement)
    {
        Vector3 baseScale = new Vector3(2,2,1);
        float jumpDuration = 0.2f;
        float jumpHeight = 1.2f;
        
        // 커지는 애니메이션
        float elapsedTime = 0f;
        while (elapsedTime < jumpDuration / 2)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / (jumpDuration / 2);
            float currentScale = Mathf.Lerp(1f, jumpHeight, progress);
            uiElement.transform.localScale = baseScale * currentScale;
            yield return null;
        }
        
        // 돌아오는 애니메이션
        elapsedTime = 0f;
        while (elapsedTime < jumpDuration / 2)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / (jumpDuration / 2);
            float currentScale = Mathf.Lerp(jumpHeight, 1f, progress);
            uiElement.transform.localScale = baseScale * currentScale;
            yield return null;
        }
        
        // 정확히 (1,1,1)로 복귀
        uiElement.transform.localScale = baseScale;
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
    protected virtual void Start()
    {
        ani = GetComponent<Animator>();
        CapsuleCollider = GetComponent<CapsuleCollider2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    protected virtual void Update()
    {
        ani.SetBool("Down", false);
        VerticalHitbox();
        HPUI.fillAmount = 1 - HP / maxHP;
        if (HPUI.fillAmount <= 0) {HPUI.fillAmount = 0;}

        if(Time.timeScale == 1)
        {
            #region 이동
            float moveX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            float moveY = Input.GetAxis("Vertical") * speed * Time.deltaTime;

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



            if(Input.GetKeyDown(KeyCode.LeftShift) && CanDash == true) 
            {
                CanDash = false;
                StartCoroutine(DashCooldown());
                Dash();
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

            Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
            viewPos.x = Mathf.Clamp01(viewPos.x);
            viewPos.y = Mathf.Clamp01(viewPos.y);
            Vector3 worldPos = Camera.main.ViewportToWorldPoint(viewPos);
            transform.position = worldPos;
    }


    


    public void Damage(float attack)
    {
        StartCoroutine(Hit());
        HP -= attack;
        HPLeftUI.text = ($"{HP} / {maxHP}");

        if(HP <= 0)
        {
            HPLeftUI.text = ($"DEAD: {HP} / {maxHP}");
        }

        if(HP <= HP*0.2f && HPDanger == false )
        {
            Hoon_AudioManager.instance.SFXDanger();
            HPDanger = true;
        }
    }
    protected virtual IEnumerator Hit()
    {
        Hoon_AudioManager.instance.SFXHit1();
        sr.color = Color.red;
        DisableHitbox();
        yield return new WaitForSeconds(0.5f);
        sr.color = Color.white;
        EnableHitbox();
    }
}
