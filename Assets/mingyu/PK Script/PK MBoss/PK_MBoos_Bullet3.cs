using System.Collections;
using UnityEngine;

public class PK_MBoos_Bullet3 : MonoBehaviour
{
    public int M_HP = 8;
    public GameObject Bullet; // 발사체 프리팹
    private SpriteRenderer spriteRenderer; // SpriteRenderer 컴포넌트
    public float fadeDuration = 1f; // 투명해지는 시간
    public float visibleDuration = 2f; // 보이는 시간

    public GameObject Item = null;

    void Start()
    {
        // SpriteRenderer 컴포넌트 가져오기
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer가 이 오브젝트에 없습니다.");
            return;
        }

        // 투명해졌다가 나타나는 코루틴 시작
        StartCoroutine(FadeInOut());
    }


    bool a = true;
    IEnumerator FadeInOut()
    {
        while (a)
        {


            // 다시 나타나기
            yield return StartCoroutine(Fade(0f, 1f));
            StartCoroutine(shot());
            // 보이는 상태 유지
            yield return new WaitForSeconds(visibleDuration);

            // 투명해지기
            yield return StartCoroutine(Fade(1f, 0f));

            a = false;
            
            Destroy(gameObject); // 0.5초 후에 오브젝트 삭제
        }
    }

    IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;

        // 현재 색상 가져오기
        Color color = spriteRenderer.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            spriteRenderer.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        // 최종 알파 값 설정
        spriteRenderer.color = new Color(color.r, color.g, color.b, endAlpha);
    }


    IEnumerator shot()
    {
        yield return new WaitForSeconds(1f);
        PK_SoundManager.instance.MB_Bullet2();
        Instantiate(Bullet, transform.position, Quaternion.identity);
    }

      private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

        private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

   public void Damage(int attack)
    {
        M_HP -= attack;
        if (M_HP <= 0)
        {
            ItemDrop();
            Destroy(gameObject);
        }

        
    }

    public int item_Random = 0;

    public void ItemDrop()
{
    item_Random = Random.Range(0, 11);

    if (item_Random >= 9)
    {
        // 아이템 생성
        Instantiate(Item, transform.position, Quaternion.identity);
    }
}
}
