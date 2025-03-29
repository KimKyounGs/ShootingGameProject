
using UnityEngine;

public class Thunder : MonoBehaviour
{
    void Update()
    {

    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (playerRb != null)
            {
                // 밀어낼 방향 계산 (충돌 지점 기준으로)
                Vector2 pushDirection =
                    new Vector2(collision.transform.position.x - transform.position.x, 0).normalized;
                
                float pushForce = 5f; // 밀어내기 힘의 크기
                playerRb.AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
            }

            Hoon_Player player = collision.gameObject.GetComponent<Hoon_Player>();
            if (player != null)
            {
                player.Damage(4);
            }

            Hoon_AudioManager.instance.SFXHit2();
        }
    }

    

}
