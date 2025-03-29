using UnityEngine;

public class KK_MonsterAttack_Homing : MonoBehaviour, IMonsterAttack
{
    public GameObject bulletPrefab; // 어떤 총알이든 할당 가능
    public Transform bulletPos;
    public float delay = 1f; // 쿨타임

    private GameObject target;

    void Start()
    {
        
    }

    public void StartAttack()
    {
        InvokeRepeating("Shoot", delay, delay);
    }

    public void StopAttack()
    {
        CancelInvoke("Shoot");
    }

    void Shoot()
    {
        // activeInHierarchy : 게임오브젝트가 씬에서 "실제로 활성화되어 있는가"를 알려주는 bool 속성 
        if (target == null || !target.activeInHierarchy) // 
        {
            target = GameObject.FindGameObjectWithTag("Player");
            // 플레이어가 없으면 공격 안함
            return;
        }
        GameObject tempBullet = Instantiate(bulletPrefab, bulletPos.position, Quaternion.identity);
        Vector2 dir = target.transform.position - transform.position; // 쏠 때마다 방향 갱신
        // 플레이어가 죽엇을 때 바꾸기.
        float visualAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        tempBullet.transform.rotation = Quaternion.Euler(0, 0, visualAngle);
        //gameObject.GetComponent<KK_MBullet>().Move(direction.normalized);


        // 폭탄이 들어있으면?
        if(tempBullet.GetComponent<KK_MBoom>())
        {
            tempBullet.GetComponent<KK_MBoom>().targetPos = target.transform.position;
        }
    }
}

