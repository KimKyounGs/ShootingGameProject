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

    public void Move(Vector3 vec)
    {
        vec2 = vec;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Hoon_Player player = collision.gameObject.GetComponent<Hoon_Player>();
            if (player != null)
            {
                player.Damage(2);
            }
            Destroy(gameObject);
        }

    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }


}
