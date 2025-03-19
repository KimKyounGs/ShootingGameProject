using UnityEngine;

public class Boss_Bullet : MonoBehaviour
{
    public float Speed = 5f;
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
