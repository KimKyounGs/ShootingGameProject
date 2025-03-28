using JetBrains.Annotations;
using System;

using UnityEngine;

public class KK_MBoom : MonoBehaviour
{
    [Header("폭탄 설정")]
    public GameObject explosionBulletPrefab; // 폭발 시 나올 총알 프리팹
    public GameObject explosionEffect; // 폭발 이펙트 프리팹

    [Header("폭탄 이동 & 폭발")]
    public float speed = 3f;
    public float explodeDelay = 2.5f;
    public int explodeCount = 8; // 폭발 시 발사할 탄 수
    public float explodeDistance = 0.1f; // 도착 거리 판정 오차
    public Vector2 targetPos;

    private Vector2 direction;

    private enum BombState { Moving, Waiting, Exploded }
    private BombState state = BombState.Moving;

    void Update()
    {
        if (state == BombState.Moving)
        {
            float distance = Vector2.Distance(transform.position, targetPos);
            if (distance <= explodeDistance)
            {
                // 도착 시 정지 + 타이머 시작
                GetComponent<KK_MBullet>().SetSpeed(0);
                state = BombState.Waiting;
                Invoke("Explode", explodeDelay);
            }
        }
    }

    void Explode()
    {
        if (state == BombState.Exploded) return;
        state = BombState.Exploded;

        float baseAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        // 이펙트 생성
        if (explosionEffect != null)
        {
            GameObject effect = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(effect, 1f);
        }

        for (int i = 0; i < explodeCount; i ++)
        {
            float angle = baseAngle + (i * 360 / explodeCount);
            float rad = angle * Mathf.Deg2Rad;
            Vector2 dir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
            GameObject bullet = Instantiate(explosionBulletPrefab, transform.position, Quaternion.identity);
            float visiualAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.Euler(0, 0, visiualAngle);
            bullet.GetComponent<KK_MBullet>().Move(dir);
        }

        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
