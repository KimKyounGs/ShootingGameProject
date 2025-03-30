using UnityEngine;

public class KK_Monster : MonoBehaviour
{
    public int HP = 100; // 체력
    public int spawnLocation;

    public GameObject monsterDieEffect; // 몬스터 타입
    //아이템 가져오기
    public GameObject item = null;
    [Range(0f, 1f)]public float dropChance = 0.3f;

    private IMonsterAttack attackPattern;
    private IMonsterMove movePattern;
    
    void Start()
    {
        attackPattern = GetComponent<IMonsterAttack>();
        if (attackPattern != null)
        {
            attackPattern.StartAttack();
        }
        movePattern = GetComponent<IMonsterMove>();
    }
    void Update()
    {
        if (movePattern != null)
        {
            movePattern.Move();
        }
    }

    //미사일에 따른 데미지 입는 함수
    public void Damage(int attack)
    {
        KK_SoundManager.Instance.PlayFX(4, 0.1f); // 피격 효과음
        HP -= attack;

        if(HP <=0)
        {
            ItemDrop();
            KK_SoundManager.Instance.PlayFX(5); // 사망 효과음
            GameObject effect = Instantiate(monsterDieEffect, transform.position, Quaternion.identity);
            Destroy(effect, 1f); // 이펙트 삭제
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void ItemDrop()
    {
        if (item != null && Random.value < dropChance)
        {
            Instantiate(item, transform.position, Quaternion.identity);
        }
    }

}

public interface IMonsterAttack
{
    void StartAttack();
    void StopAttack();
}

public interface IMonsterMove
{
    void Move();
}