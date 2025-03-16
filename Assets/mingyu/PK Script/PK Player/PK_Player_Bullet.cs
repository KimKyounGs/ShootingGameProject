using UnityEngine;

public class PK_Player_Bullet : MonoBehaviour
{
    public float Speed = 4.0f;

    public GameObject effect;



    void Start()
    {

    }


    void Update()
    {
        transform.Translate(Vector2.up * Speed * Time.deltaTime);
    }



    private void OnBecameInvisible()
    {
        //�ڱ� �ڽ� �����
        Destroy(gameObject);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PK_Monster"))
        {
            //����Ʈ����
            GameObject go = Instantiate(effect, transform.position, Quaternion.identity);
            //1�ʵڿ� �����
            Destroy(go, 1);

            //���� ������ �ֱ�
            collision.gameObject.GetComponent<PK_Monster>().Damage(+1);

            //�̻��� ����
            Destroy(gameObject);

        }
    }
}
