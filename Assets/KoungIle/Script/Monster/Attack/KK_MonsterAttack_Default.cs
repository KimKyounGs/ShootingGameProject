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

    public void StopAttack()
    {
        CancelInvoke("Shoot");
    }

    void Shoot()
    {
        KK_SoundManager.Instance.PlayFX(10, 0.25f); // 발사 효과음
        GameObject tempBullet = Instantiate(bulletPrefab, bulletPos.position, Quaternion.identity);
        tempBullet.transform.rotation = Quaternion.Euler(0, 0, -90);
    }
}
