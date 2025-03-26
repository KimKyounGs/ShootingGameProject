using UnityEngine;




public class PK_Monster_Bullet2 : MonoBehaviour
{
    public float Speed = 6f;


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
            collision.gameObject.GetComponent<PK_Player>().Damage(+1);

            //미사일지우기
            Destroy(gameObject);
        }
    }
}
