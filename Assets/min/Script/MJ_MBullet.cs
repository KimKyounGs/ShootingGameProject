using UnityEngine;

public class MJ_MBullet : MonoBehaviour
{
    public float Speed = 3f;
    public GameObject effect;
    void Update()
    {
        transform.Translate(Vector3.down * Speed * Time.deltaTime);
    }


    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MJ_Player"))
        {
            GameObject go = Instantiate(effect, transform.position, Quaternion.identity);
            Destroy(go, 1);

            //미사일지우기
            Destroy(gameObject);
        }
    }
}
