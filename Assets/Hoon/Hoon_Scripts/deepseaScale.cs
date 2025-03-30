using UnityEngine;

public class deepseaScale : MonoBehaviour
{
    public float speed = 2f;

    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime); 
    }

    protected virtual void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))  // Player 태그 확인
        {
            // 아이템 획득 처리
            Destroy(gameObject);
            Hoon_AudioManager.instance.SFXGetItem();
            ItemManager.instance.ObtainItem("Deep Sea Scale");
        }
    }
}
