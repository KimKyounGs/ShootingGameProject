using JetBrains.Annotations;
using System.Collections;
using System.Security.Cryptography;
using TMPro;
#if UNITY_EDITOR
using UnityEditor.Search;
#endif
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class PK_MBoss : MonoBehaviour
{
    public GameObject Boss_HPB; //보스 피바를 위한 오브젝트
    private SpriteRenderer spriteRenderer; // 스프라이트 렌더러
    public float fadeSpeed = 1f; // 색깔이 줄어드는 속도
    public GameObject targetObject1; // 타겟 오브젝트
    public GameObject targetObject2;
    public GameObject targetObject3;
    public GameObject targetObject4;

    public int c = 1;

    public bool Move = false; // 플레이어 이동 여부
    public bool Move1 = false;

    public GameObject bullet1;
    public GameObject bullet2;

    public GameObject bullet3;

    private float time = 0f;
    public bool playerMove = true; // 플레이어 이동 여부

    bool BPP = true;



    float B_HP = 300;
    bool Boos_Blood = false;
    bool boss_attack = true;
    public int attack_chose = 0;
    public float cool_time1 = 0;
    public float cool_time2 = 0;

    public float cool_time3 = 0;

    float boss_attack_time = 3; //보스 공격 주기

    public bool co = true; // 색상 감소 여부

    public float fadeDuration = 1f; // 투명해지는 시간
    public float visibleDuration = 2f; // 보이는 시간

    void Start()
    {
        // SpriteRenderer 컴포넌트 가져오기
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    bool Bd = true;
    bool Bdc = true;

    bool s1 = true;
    bool s2 = true;
    bool shd = true;
    void Update()
    {
        time += Time.deltaTime;

        // 현재 오브젝트의 색상 감소
        if (spriteRenderer != null)
        {
            if (co == true)
            {
                // 현재 색상 가져오기
            Color currentColor = spriteRenderer.color;

            // RGB 값을 점진적으로 감소시키기
            float newR = Mathf.Max(0, currentColor.r - fadeSpeed * Time.deltaTime);
            float newG = Mathf.Max(0, currentColor.g - fadeSpeed * Time.deltaTime);
            float newB = Mathf.Max(0, currentColor.b - fadeSpeed * Time.deltaTime);

            // 새로운 색상 적용
            spriteRenderer.color = new Color(newR, newG, newB, currentColor.a);
            
            if (Bd == true)
            {
                PK_SoundManager.instance.MB_Shadow_off();
                Bd = false;
            }
            }
        }

        // 플레이어 이동 처리
        if (time > 1f)
        {
            if (shd == true)
            {
                Instantiate(targetObject1, transform.position, Quaternion.identity);
                Instantiate(targetObject2, transform.position, Quaternion.identity);
                Instantiate(targetObject3, transform.position, Quaternion.identity);
                Instantiate(targetObject4, transform.position, Quaternion.identity);
                shd = false;
            }
            if (playerMove)
            {
                
                transform.Translate(0, 6 * Time.deltaTime, 0);
                if (Bdc == true)
            {
                PK_SoundManager.instance.B_Bullet3();
                Bdc = false;
            }
            }

            if (transform.position.y >= 6f)
            {
                playerMove = false;
            }

        }




    

        cool_time1 += Time.deltaTime;
        cool_time2 += Time.deltaTime;
        cool_time3 += Time.deltaTime;


if(time > 3f)
{
    co = false;
    // 현재 색상 가져오기
            Color currentColor = spriteRenderer.color;

            // RGB 값을 점진적으로 감소시키기
            float newR = Mathf.Max(0, currentColor.r + fadeSpeed * Time.deltaTime);
            float newG = Mathf.Max(0, currentColor.g + fadeSpeed * Time.deltaTime);
            float newB = Mathf.Max(0, currentColor.b + fadeSpeed * Time.deltaTime);

            // 새로운 색상 적용
            spriteRenderer.color = new Color(newR, newG, newB, currentColor.a);

        if (B_HP <= 0)
        {
            Destroy(gameObject);
        }


        if (B_HP <= 150)
        {
            Boos_Blood = true;
            c = 3;
            if (BPP == true)
            {
                Boss_HPB.SetActive(true); //보스 피바를 위한 오브젝트 활성화
                BPP = false; //보스 피바를 위한 오브젝트 활성화
            }
        }


        if(Boos_Blood == true)
        {
            boss_attack_time = 1.5f; //보스 공격 주기
        }

        if (Move == true)   //내려옴
        {
            StartCoroutine(Fade(0f, 1f)); // 보이기
            if (s1 == true)
            {
                PK_SoundManager.instance.MB_Shadow_on();
                s1 = false;
            }

            transform.Translate(0, -3, 0);
            Move = false;
            s1 = true;
            Move1 = true;
        }

        if (Move1 == true && cool_time3 >= 3f)  //올라감
        {
            StartCoroutine(Fade(1f, 0f)); // 투명해지기

            if (s2 == true)
            {
                PK_SoundManager.instance.MB_Shadow_off();
                s2 = false;
            }

            if (cool_time3 >= 4f)
            {
            transform.Translate(0, 3, 0);
            Move1 = false;
            s2 = true;
            cool_time3 = 0;
            boss_attack = true;
            }
        }


        if (boss_attack == true && cool_time1 >= boss_attack_time)
        {
            
            attack_chose = Random.Range(c, 3);

            if (attack_chose > 0)
            {
                cool_time1 = 0;
                cool_time2 = 0;
                boss_attack = false;
            }
        }



        if (attack_chose == 1)
        {
            attack1();
            shot_a = true;
        }


        if (attack_chose == 2)
        {
            attack2();
            shot_b = true;
        }

        if (attack_chose == 3)
        {
            attack1();
            attack2();
        }
    }
}


    bool shot_a = true;

    int LR = 0; // 1 왼쪽, 2 오른쪽
    public void attack1()
    {
        
        float a = Random.Range(0.5f, 2f);

        if (Boos_Blood == true)
        {
            a = Random.Range(0.5f, 1f);
        }

        if (shot_a == true && cool_time1 >= a)
        {
            int rnd = Random.Range(-5, 4);
            LR = Random.Range(1, 3); // 1 왼쪽, 2 오른쪽
            if (LR == 1)
            {
                Instantiate(bullet1, new Vector3(-4, rnd, 0), Quaternion.identity);
                PK_SoundManager.instance.MB_Bullet_R();
                LR = 0;
                cool_time1 = 0;
            }
            if (LR == 2)
            {
                Instantiate(bullet2, new Vector3(4, rnd, 0), Quaternion.identity);
                PK_SoundManager.instance.MB_Bullet_L();
                LR = 0;
                cool_time1 = 0;
            }
        }

        if (cool_time2 >= 10f)
        {
            shot_a = false;
            cool_time1 = 0;
            cool_time2 = 0;
            attack_chose = 0;
            Move = true;
            cool_time3 = 0;
        }
        
    }


    bool shot_b = true;

    public void attack2()
    {
        float b = Random.Range(2f, 4f);

        if (Boos_Blood == true)
        {
            b = Random.Range(0.5f, 2f);
        }

        if (shot_b == true && cool_time1 >= b)
        {
            int rnd = Random.Range(-2, 2);
            Instantiate(bullet3, new Vector3(rnd, 4, 0), Quaternion.identity);
            cool_time1 = 0;
        }

        if (cool_time2 >= 10f)
        {
            shot_b = false;
            cool_time1 = 0;
            cool_time2 = 0;
            attack_chose = 0;
            if (Boos_Blood == false)
            {
                cool_time3 = 0;
                Move = true;
            }
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


    
    public void Damage(int attack)  //플레이어에게 데미지를 입는 함수
    {
        B_HP -= attack;
        Debug.Log("보스 체력 : " + B_HP);
        if (B_HP <= 0)
        {
            Destroy(gameObject);
        }
    }

    [System.Obsolete]
    private void OnDestroy()
    {
        // 중간보스가 파괴될 때 PK_SpownManger에 알림
        PK_SpownManger spawner = FindObjectOfType <PK_SpownManger>();
        if (spawner != null)
        {
            spawner.Stop4(); // 다음 소환
        }
    }
}

