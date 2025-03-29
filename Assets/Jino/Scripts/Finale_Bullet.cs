using UnityEngine;

public class Finale_Bullet : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 direction;

    void Start()
    {
        Destroy(gameObject, 2f);
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir;
    }
}
