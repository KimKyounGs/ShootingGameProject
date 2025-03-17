using Unity.VisualScripting;
using UnityEngine;

public class PK_Swould : MonoBehaviour
{
    public float Speed = 5.0f;
    public GameObject effect;

    private void Start()
    {
        Destroy(gameObject, 0.3f);
    }

    void Update()
    {
        float moveX = Speed * Time.deltaTime * Input.GetAxis("Horizontal");
        float moveY = Speed * Time.deltaTime * Input.GetAxis("Vertical");

        transform.Translate(moveX, moveY, 0);



        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
        viewPos.x = Mathf.Clamp01(viewPos.x); //x값을 0이상, 1이하로 제한한다.
        viewPos.y = Mathf.Clamp01(viewPos.y); //y값을 0이상, 1이하로 제한한다.
        Vector3 worldPos = Camera.main.ViewportToWorldPoint(viewPos);//다시월드좌표로 변환
        transform.position = worldPos; //좌표를 적용한다.
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PK_Monster"))
        {
            //이펙트생성
            GameObject go = Instantiate(effect, transform.position, Quaternion.identity);
            //1초뒤에 지우기
            Destroy(go, 1);

            //몬스터 데미지 주기
            collision.gameObject.GetComponent<PK_Monster>().Damage(+1);
        }

        if (collision.CompareTag("PK_Boss"))
        {
            //이펙트생성
            GameObject go = Instantiate(effect, transform.position, Quaternion.identity);
            //1초뒤에 지우기
            Destroy(go, 1);

            //몬스터 데미지 주기
            collision.gameObject.GetComponent<PK_Boss>().Damage(+1);
        }

        if (collision.CompareTag("PK_Monster_Bullet"))
        {
            //이펙트생성
            GameObject go = Instantiate(effect, transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            //1초뒤에 지우기
            Destroy(go, 1);
        }
    }
}
