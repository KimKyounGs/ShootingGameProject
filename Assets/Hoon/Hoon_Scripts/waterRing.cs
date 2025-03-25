using JetBrains.Annotations;
using System.Collections;
using UnityEngine;

public class waterRing : MonoBehaviour
{
    public float scaleSpeed = 1.5f; // 커지는 속도 조절
    public float maxScale = 4.5f;
    public bool destroyWave = false;
    public float pushForce = 2f; // 밀어내는 힘 조절

    void Start()
    {
        transform.localScale = Vector3.zero;
    }

    void Update()
    {
        if (transform.localScale.x < maxScale)
            {
                transform.localScale += Vector3.one * scaleSpeed * Time.deltaTime;
            }
            else
            {
                Destroy(gameObject);
            }     
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (playerRb != null)
            {
                // 밀어낼 방향 계산 (충돌 지점 기준으로)
                Vector2 pushDirection = (collision.transform.position - transform.position).normalized;

                playerRb.AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
            }
        }
    }
}

