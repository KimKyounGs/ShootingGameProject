using UnityEngine;

public class Enemy_Bullet : MonoBehaviour
{
    public float Speed = 7f;
    Vector2 vec2 = Vector2.down;

    void Start()
    {

    }


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
}
