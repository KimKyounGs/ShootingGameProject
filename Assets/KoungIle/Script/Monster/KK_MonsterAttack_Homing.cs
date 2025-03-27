using UnityEngine;

public class KK_MonsterAttack_Homing : MonoBehaviour, IMonsterAttack
{
    public GameObject bulletPrefab; // 어떤 총알이든 할당 가능
    public Transform bulletPos;
    public float delay = 1f;

    public GameObject target;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    public void StartAttack()
    {
        InvokeRepeating("Shoot", delay, delay);
    }

    void Shoot()
    {
        GameObject gameObject = Instantiate(bulletPrefab, bulletPos.position, Quaternion.identity);
        Vector2 direction = target.transform.position - transform.position; // 쏠 때마다 방향 갱신
        gameObject.GetComponent<KK_MBullet>().Move(direction.normalized);

        if (gameObject.GetComponent<KK_MBoom>())
        {
            gameObject.GetComponent<KK_MBoom>().targetPos = target.transform.position;
        }
    }
}

