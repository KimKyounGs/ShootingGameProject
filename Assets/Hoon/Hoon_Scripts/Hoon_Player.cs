using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
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
    public float speed = 3f;
    public float bulletCooldown = 0.5f;
    public float dashCooldown = 8f;
    
    public bool CanShoot = true;
    public bool CanDash = true;

    public bool isEvolved = false;
    public bool Firing = false;

    [Header("UI")]
    public Image dashUI;
    public TMP_Text dashNameUI;

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
         
        dashUI.fillAmount = 1f;
        dashNameUI.color = new Color(1, 1, 1, 1f);
        
        CanDash = true;
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
    // HP -= attack;

    // if(HP <= 0)
    // {
    //     Vector3 hitPos = transform.position;
    //     if (hit != null)
    //     {
    //         GameObject go = Instantiate(hit, hitPos, Quaternion.identity);
    //         Destroy(go, 0.2f);
    //     }
        
    //     if(droprate > 0)
    //     {
    //         ItemManager.instance.ItemDrop(hitPos);
    //     }

    //     Destroy(gameObject);
    //     GameManager.instance.ExpGain(exp);
    // }    
    }
    protected virtual IEnumerator Hit()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sr.color = Color.white;
    }
}
