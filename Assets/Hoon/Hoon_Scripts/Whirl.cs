using UnityEngine;

public class Whirl : MonoBehaviour
{   
    public float Speed = 2f;
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
    
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }


}
