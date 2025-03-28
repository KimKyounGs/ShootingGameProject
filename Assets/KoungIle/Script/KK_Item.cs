using UnityEngine;

public class KK_Item : MonoBehaviour
{
    public float fallSpeed = 2f;

    void Update()
    {
        transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<KK_Player>()?.IncreasePower();
            Destroy(gameObject);
        }
    }
    
}
