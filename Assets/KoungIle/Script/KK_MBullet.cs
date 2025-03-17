using UnityEngine;

public class KK_MBullet : MonoBehaviour
{
    public float speed = 3f;

    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
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
}
