using UnityEngine;

public class PK_Boss_Bullet4_1 : MonoBehaviour
{
    public float Speed = 3f;
    public float cool_time = 0;
    Vector2 vec2 = Vector2.down;



    void Update()
    {
        transform.Translate(vec2 * Speed * Time.deltaTime);
        cool_time += Time.deltaTime;

        if (Speed >= -1)
        {
            if (cool_time >= 0.1)
            {
                Speed -= 0.5f;
                cool_time = 0;
            }
        }
    }


    public void Move(Vector2 vec)
    {
        vec2 = vec;
    }


    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
