using UnityEngine;

public class Mj_WarningSign : MonoBehaviour
{
    
    private SpriteRenderer spriteRenderer;
    public float blinkSpeed = 2f;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        float alpha = Mathf.PingPong(Time.time * blinkSpeed, 1f);
        spriteRenderer.color = new Color(1f, 0f, 0f, alpha);
    }
}
