using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class MJ_Pllayer : MonoBehaviour
{
    public static MJ_Pllayer instance;
    public float moveSpeed = 5f;

    Animator animator; //애니메이터 생성
    public AudioClip missile;
    AudioSource audioSource;

    public GameObject[] bullet;  //총알 4개 배열
    public Transform pos;
    public Image clearPanel;
    public TMP_Text clearText;

    public GameObject powerup;

    public int power = 0;

    private void Awake()
    {
        if(instance == null) //인스턴스 있는지 검사
        {
            instance = this; //자기 자신을 담는다.
        }
    }
    void Start()
    {
        animator = GetComponent<Animator>(); //GetComponet로 애니메이터 가져오기
        audioSource = GetComponent<AudioSource>();
        if (clearPanel != null)
        {
            clearPanel.color = new Color(0,0,0,0);
            clearText.color = new Color(0,0,0,0);

        }
        
    }

    
    void Update()
    {
        //방향키로 움직임
        float moveX = moveSpeed * Time.deltaTime * Input.GetAxis("Horizontal"); //설정한 속도 x 보정 시간 x 수평 움직임
        float moveY = moveSpeed * Time.deltaTime * Input.GetAxis("Vertical"); //설정한 속도 x 보정 시간 x 수직 움직임

        //왼쪽
        if (Input.GetAxis("Horizontal") <= -0.5f)
        {
            animator.SetBool("left", true);
        }
        else
            animator.SetBool("left", false);
        //오른쪽
        if (Input.GetAxis("Horizontal") >= 0.5f)
        {
            animator.SetBool("right", true);
        }
        else
            animator.SetBool("right", false);
        //위쪽
        if (Input.GetAxis("Vertical") >= 0.5f)
        {
            animator.SetBool("up", true);
        }
        else
            animator.SetBool("up", false);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bullet[power], pos.position, Quaternion.identity);
            audioSource.PlayOneShot(missile);
        }

        transform.Translate(moveX, moveY, 0); //움직임 표현

        //캐릭터의 월드 좌표를 뷰포트 좌표계로 변환해준다.
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
        viewPos.x = Mathf.Clamp01(viewPos.x); //x값을 0이상, 1이하로 제한한다.
        viewPos.y = Mathf.Clamp01(viewPos.y); //y값을 0이상, 1이하로 제한한다.
        Vector3 worldPos = Camera.main.ViewportToWorldPoint(viewPos);//다시월드좌표로 변환
        transform.position = worldPos; //좌표를 적용한다.
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MJ_Item"))
        {
            
            if (power < 3)
            {
                //파워업
                GameObject go = Instantiate(powerup, transform.position, Quaternion.identity);
                Destroy(go, 1);
                power += 1;
            }

            //아이템 먹은 처리
            Destroy(collision.gameObject);
        }
    }
    public void ClearOn()
    {
        // 클리어 패널 활성화
        if (clearPanel != null)
        {
            clearPanel.color = new Color(1, 1, 1, 1);
            clearText.color = new Color(1,1,1,1);

        }
    }
        
}
