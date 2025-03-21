using UnityEngine;

public class KK_MBullet : MonoBehaviour
{
    public float speed = 3f;
    public Vector2 direction = Vector2.down;

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            //미사일지우기
            Destroy(gameObject);
        }
    }

    public void Move(Vector2 vector2)
    {
        direction = vector2;
    }
}
