using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class KK_Player : MonoBehaviour
{   
    public int life = 3; // 플레이어 생명
    public float speed = 5f;
    public GameObject[] bullet;  // 총알배열
    public Transform bulletPos = null; // 총알 발사 위치
    public int power = 1; // 현재 총알 강화 단계
    public GameObject playerDieEffect; // 사망 효과
    public bool bplayerInvincibility = false; // 무적 상태
    public float playerInvincibilityTime = 2f; // 무적 시간

    [SerializeField] private float attackCoolTime;
    [SerializeField] private float attackMaxCoolTime = 0.5f;
    private Dictionary<int, int[]> powerPatterns;

    private Vector2 minBounds;
    private Vector2 maxBounds;
    Animator ani; //애니메이터를 가져올 변수


    void Start()
    {
        ani = GetComponent<Animator>();
        // 화면의 경계를 설정
        Camera cam = Camera.main;
        Vector3 bottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, 0));

        minBounds = new Vector2(bottomLeft.x, bottomLeft.y);
        maxBounds = new Vector2(topRight.x, topRight.y);
        

        // 각 파워 레벨에 대한 총알 인덱스 패턴 설정
        powerPatterns = new Dictionary<int, int[]>
        {
            { 1, new int[] { 0 } },
            { 2, new int[] { 0, 0 } },
            { 3, new int[] { 0, 1, 0 } },
            { 4, new int[] { 1, 1, 1 } },
            { 5, new int[] { 1, 2, 1 } },
            { 6, new int[] { 2, 2, 2 } },
            { 7, new int[] { 2, 3, 2} },
            { 8, new int[] { 3, 3, 3} },
            { 9, new int[] { 3, 4, 3} },
            { 10, new int[] { 4, 4, 4} }
        };

        // UI 업데이트
        KK_UIManager.Instance.UpdateLifeUI(life); 
        KK_UIManager.Instance.UpdatePowerUI(power); 
    }


    void Update()
    {
        Move();
        Shoot();

        // 무적 모드
        if (Input.GetKeyDown(KeyCode.I) && !bplayerInvincibility)
        {
            // 파워업
            for (int i = 0; i < 10; i++)
            {
                IncreasePower();
            }

            // 무적 상태
            bplayerInvincibility = true;
        }
        
        if (KK_Boss.instance.isDead == true)
        {
            StartCoroutine(SceneLoader());
        }
    }
    IEnumerator SceneLoader()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Hoon_Stage");
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
        if (power <= 0 || !powerPatterns.ContainsKey(power)) return;

        attackCoolTime += Time.deltaTime;

        if (Input.GetKey(KeyCode.Space) && attackCoolTime >= attackMaxCoolTime)
        {
            int[] pattern = powerPatterns[power];

            for (int i = 0; i < pattern.Length; i++)
            {
                float offset = (i - (pattern.Length - 1) / 2f) * 0.25f;
                Vector3 spawnPos = bulletPos.position + new Vector3(offset, 0f, 0f);

                int bulletIndex = Mathf.Clamp(pattern[i], 0, bullet.Length - 1); // 에러 방지 코드
                GameObject prefab = bullet[bulletIndex];
                Vector3 offsetFromPrefab = prefab.GetComponent<KK_PBulletData>()?.spawnOffset ?? Vector3.zero;
                spawnPos += offsetFromPrefab;
                Instantiate(prefab, spawnPos, Quaternion.identity);
            }
            KK_SoundManager.Instance.PlayFX(0, 0.25f); // 공격 브금 재생 및 사운드 조절
            attackCoolTime = 0f;
        }
    }


    public void IncreasePower()
    {
        power = Mathf.Min(power + 1, 10);
        KK_SoundManager.Instance.PlayFX(2, 1f); // 레벨업 효과음
        // 파워업 이팩트!
        KK_UIManager.Instance.UpdatePowerUI(power);
    }

    public void PlayerDie()
    {
        gameObject.SetActive(false); // 비활성화 후
        GameObject effct = Instantiate(playerDieEffect, transform.position, Quaternion.identity); // 사망 이펙트 생성
        Destroy(effct, 1f); // 1초 후에 이펙트 삭제
        KK_SoundManager.Instance.PlayFX(3, 1f); // 사망 효과음

        power = 1; // 파워 초기화
        KK_UIManager.Instance.UpdatePowerUI(power);
        life--; // 생명 감소
        KK_UIManager.Instance.UpdateLifeUI(life);

        if (life <= 0)
        {
            // 게임 오버 처리
            // KK_SoundManager.Instance.PlayFX(4, 1f); // 게임 오버 효과음
            gameObject.SetActive(false); // 비활성화 후
            return;
        }
        Invoke("PlayerRevive", 2f); // 2초 후에 PlayerRevive 호출
    }

    public void PlayerRevive()
    {
        transform.position = new Vector3(0, -4, 0); // 초기 위치로 이동
        gameObject.SetActive(true); // 활성화 후
        
        StartCoroutine(InvincibilityCoroutine()); // 무적 상태 시작
    }

    IEnumerator InvincibilityCoroutine()
    {
        bplayerInvincibility = true;

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        float elapsed = 0f;
        float blinkInterval = 0.15f;

        while (elapsed < playerInvincibilityTime)
        {
            if (spriteRenderer != null)
                spriteRenderer.enabled = !spriteRenderer.enabled;

            yield return new WaitForSeconds(blinkInterval);
            elapsed += blinkInterval;
        }

        if (spriteRenderer != null)
            spriteRenderer.enabled = true;

        bplayerInvincibility = false;
    }

}
