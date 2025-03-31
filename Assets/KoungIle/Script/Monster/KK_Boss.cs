using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class KK_Boss : MonoBehaviour
{
    public static KK_Boss instance;

    [Header("보스 체력")]
    public int maxHP = 1000;
    private int currentHP;

    [Header("공격 활성화 1.회전 2.지그재그 3.랜덤 4.플레이어 추적")]
    public bool[] canAttack = {false, false, false, false};
    public int attackCount = 2;

    [Header("페이즈 체력 비율")]
    public float phase2Threshold = 0.7f; // 70%
    public float phase3Threshold = 0.4f; // 40%

    private int currentPhase = 1;
    public bool isDead = false;

    private IMonsterAttack[] attackPatterns;
    private IMonsterMove movePattern;
    public Image screenUI;
    public Text clearText;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        currentHP = maxHP;

        // 모든 공격 패턴 자동 수집
        attackPatterns = GetComponents<IMonsterAttack>();
        movePattern = GetComponent<IMonsterMove>();
        StartCoroutine(PatternRoutine());
    }

    void Update()
    {
        if (movePattern != null)
        {
            movePattern.Move();
        }
    }

    IEnumerator PatternRoutine()
    {
        while (!isDead)
        {
            yield return new WaitForSeconds(1.5f); // 공격 전 약간 대기
            int cnt = 0;
            while(cnt < attackCount)
            {
                int idx = Random.Range(0, 4);
                if (canAttack[idx]) continue;
                canAttack[idx] = true;
                cnt ++;
            }
            for (int i = 0; i < 4; i ++)
            {
                if (canAttack[i])
                {
                    attackPatterns[i].StartAttack();
                }
            }

            yield return new WaitForSeconds(10f); // 10초간 공격

            for (int i = 0; i < 4; i ++)
            {
                if (canAttack[i])
                {
                    attackPatterns[i].StopAttack();
                    canAttack[i] = false;
                }
            }
        }
        yield return null;
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHP -= damage;
        KK_SoundManager.Instance.PlayFX(4, 0.1f);

        if (currentHP <= 0)
        {
            currentHP = 0;
            Die();
        }

        UpdatePhase();
    }

    void UpdatePhase()
    {
        float hpRatio = (float)currentHP / maxHP;

        if (hpRatio < phase3Threshold && currentPhase < 3)
        {
            currentPhase = 3;
            attackCount = 4;
            Debug.Log("Phase 3 돌입!");
        }
        else if (hpRatio < phase2Threshold && currentPhase < 2)
        {
            currentPhase = 2;
            attackCount = 3;
            Debug.Log("Phase 2 돌입!");
        }
    }

    void Die()
    {
        isDead = true;
        Debug.Log("보스 사망!");
        
        // 폭발 이펙트, 클리어 UI 등 처리
        StartCoroutine("DestroyBoss");
    }

    IEnumerator DestroyBoss()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
        NextStage();
    }

    public void NextStage()
    {
        SceneManager.LoadScene("Hoon_Stage"); // 다음 스테이지로 이동
    }


    public int GetPhase()
    {
        return currentPhase;
    }
}
