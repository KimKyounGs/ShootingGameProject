using UnityEngine;

public class PK_MBoss_Bullet1 : MonoBehaviour
{
     public float Speed = 3f;
    Vector2 vec2 = Vector2.down;



    void Update()
    {
        transform.Translate(vec2 * Speed * Time.deltaTime);
    }


    public void Move(Vector2 vec)
    {
        vec2 = vec;
    }


    public void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
