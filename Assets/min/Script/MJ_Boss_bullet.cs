using UnityEngine;

public class MJ_Boss_bullet : MonoBehaviour
{
    public GameObject effect;
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


    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MJ_Player"))
        {
            GameObject go = Instantiate(effect, transform.position, Quaternion.identity);
            Destroy(go, 1);

            Destroy(gameObject);
        }
    }
}
