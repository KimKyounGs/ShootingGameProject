using UnityEngine;
using UnityEngine.UI;

public class MJ_MonsterHP : MonoBehaviour
{

    public Image hpBar; // 체력 바 UI
    public float maxHP = 100f;
    private float currentHP;
    private bool isDead = false; // 몬스터가 죽었는지 체크
    public int item_max_pb = 100;

    public GameObject item = null;

    void Start()
    {
        currentHP = maxHP;
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

        ItemDrop();
        // 🔹 1. 몬스터 오브젝트 삭제
        Destroy(gameObject);

        // 🔹 3. 콜라이더 비활성화 (공격받지 않도록)
        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            col.enabled = false;
        }
    }

    public void ItemDrop()
    {
        int prob = Random.Range(1, 101);

        if (prob < item_max_pb)
        {
            Instantiate(item, transform.position, Quaternion.identity);
        }
    }
}
