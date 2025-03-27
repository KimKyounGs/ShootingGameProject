using System.Collections;
using UnityEngine;

public class KK_Boss : MonoBehaviour
{
    [Header("보스 체력")]
    public int maxHP = 1000;
    private int currentHP;

    [Header("공격 쿨타임")]
    public float patternCooldown = 3f;

    [Header("페이즈 체력 비율")]
    public float phase2Threshold = 0.7f; // 70%
    public float phase3Threshold = 0.4f; // 40%

    private int currentPhase = 1;
    private bool isDead = false;

    private IMonsterAttack[] attackPatterns;
    private bool isAttacking = false;

    void Start()
    {
        currentHP = maxHP;

        // 모든 공격 패턴 자동 수집
        attackPatterns = GetComponents<IMonsterAttack>();

        StartCoroutine(PatternRoutine());
    }

    IEnumerator PatternRoutine()
    {
        yield return new WaitForSeconds(1f); // 시작 전 약간 대기

        while (!isDead)
        {
            if (!isAttacking)
            {
                isAttacking = true;

                // 랜덤 공격 선택
                // int rand = Random.Range(0, attackPatterns.Length);
                int rand = 3;
                attackPatterns[rand].StartAttack();

                yield return new WaitForSeconds(patternCooldown);
                isAttacking = false;
            }

            yield return null;
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHP -= damage;

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
            Debug.Log("Phase 3 돌입!");
            // 패턴 속도 증가, 추가 공격 등
        }
        else if (hpRatio < phase2Threshold && currentPhase < 2)
        {
            currentPhase = 2;
            Debug.Log("Phase 2 돌입!");
        }
    }

    void Die()
    {
        isDead = true;
        Debug.Log("보스 사망!");
        // 폭발 이펙트, 클리어 UI 등 처리
        Destroy(gameObject);
    }

    public int GetPhase()
    {
        return currentPhase;
    }
}
