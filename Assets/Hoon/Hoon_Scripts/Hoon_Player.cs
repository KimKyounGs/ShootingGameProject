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
    public GameObject skill;
    public float effectDashInterval = 0.2f; // 이펙트 생성 간격

    [Header("캐릭터 설정")]
    public float speed = 5f; // 잉어킹은 3f
    public float bulletCooldown = 0.15f;
    public float skillCooldown = 10f;
    public float dashCooldown = 5f;
    
    public bool CanShoot = true;
    public bool CanSkill = true;
    public bool CanDash = true;

    public bool isEvolved = false;
    public bool Firing = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else Destroy(gameObject);       
    }
    protected virtual void Shoot()
    {
        StartCoroutine(BulletCooldown());
        CanShoot = false;
        Instantiate(Bullet[0], pos.transform.position, Quaternion.identity);
    }

    protected virtual void Skill()
    {
        if(Bullet[1] != null)
        {
            StartCoroutine(SkillCooldown());
            CanSkill = false;
            Instantiate(Bullet[1], pos.transform.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("스킬이 없습니다.");
        }
    }

    protected virtual void Dash()
    {
        StartCoroutine(DashCooldown());
        CanDash = false;
    }

    IEnumerator BulletCooldown()
    {
        yield return new WaitForSeconds(0.5f);
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
    void Start()
    {
        ani = GetComponent<Animator>();
        CapsuleCollider = GetComponent<CapsuleCollider2D>();
        sr = GetComponent<SpriteRenderer>();

    }

    void Update()
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

            if(Input.GetKeyDown(KeyCode.E) && CanSkill == true) 
            {
                CanSkill = false;
                StartCoroutine(SkillCooldown());
                Skill();
            }

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
