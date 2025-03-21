using UnityEngine;

public class KK_Monster : MonoBehaviour
{
    public int HP = 100; // 체력
    public int spawnLocation;
    //아이템 가져오기
    public GameObject Item = null;


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
        HP -= attack;

        if(HP <=0)
        {
            // ItemDrop();
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    // public void ItemDrop()
    // {
    //     //아이템 생성
    //     Instantiate(Item, transform.position, Quaternion.identity);
    // }

}

public interface IMonsterAttack
{
    void StartAttack();
}

public interface IMonsterMove
{
    void Move();
}