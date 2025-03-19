using UnityEngine;

public class missile_Explo : MonoBehaviour
{
    public float Speed = 3f;

    void Start()
    {
        
    }

    
    void Update()
    {
        transform.Translate(Vector3.down * Speed * Time.deltaTime);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
