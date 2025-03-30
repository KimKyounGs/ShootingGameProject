using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MJ_BossHP : MonoBehaviour
{

    public Image hpBar; // 체력 바 UI
    public float maxHP = 100f;
    private float currentHP;
    private bool isDead = false; // 몬스터가 죽었는지 체크

    public GameObject clearPanel; // 클리어 메시지 패널

    void Start()
    {
        currentHP = maxHP;
        if (clearPanel != null)
        {
            clearPanel.SetActive(false); // 시작 시 비활성화
        }
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return; // 이미 죽었다면 실행하지 않음

        currentHP -= damage;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
        UpdateHPBar();

        if (currentHP <= 0)
        {
            Die(); // 체력이 0이면 Die() 실행
        }
    }

    void UpdateHPBar()
    {
        hpBar.fillAmount = currentHP / maxHP;
    }

    void Die()
    {
        isDead = true;

        // Collider 비활성화
        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            col.enabled = false;
        }

        // 클리어 패널 활성화
        if (clearPanel != null)
        {
            clearPanel.SetActive(true);
        }

        // 3초 후에 다음 씬 로드
        Invoke("LoadNextScene", 3f);

        // 3초 후에 객체 파괴
        Invoke("DestroyObject", 3.1f);
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene("PK_Scene");
    }
    void DestroyObject()
    {
        Destroy(gameObject);
    }
}
