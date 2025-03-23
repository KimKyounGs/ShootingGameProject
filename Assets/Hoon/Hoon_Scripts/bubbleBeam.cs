using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class bubbleBeam : MonoBehaviour
{
    public float bulletSpeed = 0.5f;
    public GameObject bubblePop;
    Animator ani;

    void Start()
    {
        ani = GetComponent<Animator>();
    }

    void Update()
    {
        transform.Translate(Vector2.up * bulletSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Hoon_Enemy"))
        {
            Hoon_Monster enemy = collision.GetComponent<Hoon_Monster>();
            if (enemy != null)
            {
            enemy.Damage(1);
            }

            Hoon_AudioManager.instance.SFXBubblePop();
            Destroy(gameObject);
            GameObject go = Instantiate(bubblePop, collision.transform.position, Quaternion.identity);
            Destroy(go, 0.2f);

        }
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);       
    }

    IEnumerator BubblePop()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }
}
