using UnityEngine;

public class waterfall : MonoBehaviour
{   

    SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Color color = spriteRenderer.color;
    }

    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 60));
        spriteRenderer.color = new Color(1, 1, 1, Mathf.Lerp(spriteRenderer.color.a, 0, Time.deltaTime * 2));    
    }
}
