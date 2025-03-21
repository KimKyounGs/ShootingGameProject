using UnityEngine;

public class KK_MonsterAttack_Default : MonoBehaviour, IMonsterAttack
{
    public GameObject bulletPrefab;         // 어떤 총알이든 할당 가능
    public Transform bulletPos;
    public float delay = 1f;

    public void StartAttack()
    {
        InvokeRepeating("Shoot", delay, delay);
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, bulletPos.position, Quaternion.identity);
    }
}
