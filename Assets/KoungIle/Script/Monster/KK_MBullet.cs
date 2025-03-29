using UnityEngine;

public class KK_MBullet : MonoBehaviour
{
    public float speed = 3f;
    public Vector2 direction;

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            KK_Player player = collision.GetComponent<KK_Player>();
            if (player.bplayerInvincibility == false)
            {
                player.PlayerDie();
                if (GetComponentInParent<KK_LaserController>()) return;
                Destroy(gameObject);
            }
        }
    }

    public void Move(Vector2 vector2)
    {
        direction = vector2;
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

}
