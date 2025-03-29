using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class iceEffect : MonoBehaviour
{
    float speed = 7f;
    public float rotateSpeed = 270f; // 초당 회전 속도 (도)
    private Vector3 moveDirection;

    void Start()
    {
        moveDirection = Vector3.down;
    }

    void Update()
    {
        transform.position += moveDirection * speed * Time.deltaTime;
        transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Hoon_Player player = collision.gameObject.GetComponent<Hoon_Player>();
            if (player != null)
            {
                player.Damage(1);
            }
            Destroy(gameObject);
        }
    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
