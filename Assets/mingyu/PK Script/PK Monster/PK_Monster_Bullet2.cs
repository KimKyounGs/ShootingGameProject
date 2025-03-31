using UnityEngine;




public class PK_Monster_Bullet2 : MonoBehaviour
{
    public float Speed = 6f;
    void Start()
    {
        PK_SoundManager.instance.M_Bullet2(); // 미사일 발사 소리 재생
    }

    void Update()
    {
        transform.Translate(Vector2.down * Speed * Time.deltaTime);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            //미사일지우기
            Destroy(gameObject);
        }
    }
}
