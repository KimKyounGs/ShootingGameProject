
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TreeEditor;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class Kyogre : Hoon_Monster
{
    //public Animator ani;
    //public GameObject hit;
    public GameObject waterRing;
    public Transform pos1;
    public Image BossHPUI;
    public GameObject rainEffect;
    public GameObject clampearl;
    public GameObject sharpedo;
    List<GameObject> rain = new List<GameObject>();	

    // 번개 패턴
    public GameObject thunder;
    public GameObject thunderCloud;
    public bool thunderOn = false;
    public bool Cloud = false;
    Vector3 thunderVec = new Vector3(0, 4.5f, 0);

    public GameObject whirl;
    public Vector3 WhirlPos = new Vector3();
    public float bulletSpeed = 1f;
    public bool Pattern2 = false;
    public bool Pattern3 = false;
    public bool Pattern4 = false;
    public GameObject iceBeam;
    public bool Pattern5 = false;
    public bool LastPattern = false;
    public float thunderX;

    protected override void Start()
    {
        base.Start();
        moveSpeed = 0;
        HP = 2000;
        exp = 0;

        InvokeRepeating("CastWaterRing", 1, 10);
        Invoke("WhirlPool", 5);
        Invoke("WhirlPool2", 12);
        InvokeRepeating("SpawnClampearl", 10, 10);
    }

    void CastWaterRing()
    {
        Hoon_AudioManager.instance.SFXSurf();
        Instantiate(waterRing, pos1.position, Quaternion.identity);
    }
    void SpawnClampearl()
    {
        float randomX = Random.Range(-2f, 2f);
        Instantiate(clampearl, new Vector3(randomX, 5, 0f), Quaternion.identity);
    }

    void SpawnSharpedo()
    {
        for(float i = 0; i<6; i++)
        {
            Instantiate(sharpedo, new Vector3(-2.5f + i, 5, 0f), Quaternion.identity);
        }
        
    }
    protected override void Update()
    {
        BossHPUI.fillAmount = (HP-1000) / 1000;
        thunderX = Hoon_Player.instance.transform.position.x;

        if(HP <= 1000)
        {
            BossHPUI.fillAmount = 0;
            WinBoss();
        }
        
        if (HP < 1900 && !Pattern2)
        {
            Hoon_AudioManager.instance.CrySharpedo();
            InvokeRepeating("SpawnSharpedo", 0, 30);
            Pattern2 = true; 
        }
        if (HP < 1800 && !Pattern3)
        {
            Invoke("WhirlPool3", 1);
            Pattern3 = true; 
        }
        if (HP < 1650 && !Pattern4)
        {
            CastIceBeam();
            Pattern4 = true; 
        }

        if (HP < 1500 && !thunderOn)
        {
            Hoon_AudioManager.instance.CryKyogre();
            InvokeRepeating("CastThunder", 1, 4);
            thunderOn = true;
            if (thunderOn == true && Cloud == false)
            {
                Invoke("ThunderCloud", 1);
                Cloud = true;
            }
        }

        if (HP < 1350 && !Pattern5)
        {
            CastIceBeam();
            Pattern5 = true; 
        }

        if (HP < 1200 && !LastPattern)
        {
            CastSheerCold(); 
        }

          
    }
    void CastSheerCold()
    {

    }
    void CastIceBeam()
    {
        StartCoroutine(IceBeam());
    }
    void IceDraw()
    {
        Instantiate(iceBeam, pos1.position, Quaternion.identity);
    }
    IEnumerator IceBeam()
    {
        Vector3 startPos = transform.position;  // 현재 위치 저장
        Vector3 leftPos = new Vector3(-2f, 4.5f, 0f);  // 왼쪽 끝 위치
        Vector3 rightPos = new Vector3(2.1f, 4.5f, 0f);  // 오른쪽 끝 위치
        float moveSpeed = 1f;  // 이동 속도

        // 왼쪽으로 이동
        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * moveSpeed;
            transform.position = Vector3.Lerp(startPos, leftPos, elapsedTime);
            yield return null;
        }

        // 잠시 대기
        yield return new WaitForSeconds(1f);
        
        // 빔 발사 시작
        Hoon_AudioManager.instance.SFXIceBeam();
        InvokeRepeating("IceDraw", 0, 0.1f);

        // 오른쪽으로 천천히 이동하며 빔 발사
        elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * 0.5f;  // 더 천천히 이동
            transform.position = Vector3.Lerp(leftPos, rightPos, elapsedTime);
            yield return null;
        }

        // 빔 발사 중단
        CancelInvoke("IceDraw");
        yield return new WaitForSeconds(1f);

        // 원래 위치로 복귀
        elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * moveSpeed;
            transform.position = Vector3.Lerp(rightPos, startPos, elapsedTime);
            yield return null;
        }
    }

    void ThunderCloud()
    {
        Instantiate(thunderCloud, Vector3.zero, Quaternion.identity);
    }
    public void CastThunder()
    {
        thunderVec = new Vector3(thunderX, 4.5f, 0);
        Hoon_AudioManager.instance.SFXThunder();
        StartCoroutine(thunderTail());
    }

    IEnumerator thunderTail()
    {
        for(int i = 0; i < 21; i++)
        {
            Vector3 tailPos = new Vector3(0, i * -0.5f, 0);
            yield return new WaitForSeconds(0.025f);
            GameObject go = Instantiate(thunder, thunderVec + tailPos, Quaternion.identity);
            Destroy(go, 0.4f);
        }
        
    }

    IEnumerator raindrop()
    {
        if(rain.Count<20)
        {
            float posX = Random.Range(-2.8f, 2.8f);
            float posY = Random.Range(-4.5f, 4.5f);
            Vector3 rainVec = new Vector3(posX, posY, 0);
            GameObject go = Instantiate(rainEffect, rainVec, Quaternion.identity);
            rain.Add(go);
            yield return new WaitForSeconds(1f);
        }
        else if(rain.Count>20)
        {
            for (int i = 0; i < rain.Count; i++)
            {
                Destroy(rain[i]); //게임 오브젝트 지우기
                rain.RemoveAt(i); //게임 오브젝트 관리하는 리스트 지우기
            }
        }
        
    }

    void WhirlPool()
    {
        StartCoroutine(SpiralShoot());
    }

    void WhirlPool2()
    {
        StartCoroutine(CircleShoot());
    }

    void WhirlPool3()
    {
        StartCoroutine(WaveBarrage());
    }


    void Shoot(Vector2 direction)
    {
        GameObject clone = Instantiate(whirl, pos1.position, Quaternion.identity);
        clone.GetComponent<Whirl>().Move(direction * bulletSpeed);
    }
    
    IEnumerator SpiralShoot()
    {
        float weightAngle = 0f;
        float attackRate = 4.5f;
        float intervalAngle = 360 / 24;

        while(true)
        {
            Hoon_AudioManager.instance.SFXWhirlpool();
            for(int i = 0; i< 24; i++)
            {
                GameObject clone = Instantiate(whirl, pos1.transform.position, Quaternion.identity);
                float angle = weightAngle + intervalAngle * i;
                float x = Mathf.Cos(angle * Mathf.Deg2Rad);
                float y = Mathf.Sin(angle * Mathf.Deg2Rad);

                clone.GetComponent<Whirl>().Move(new Vector2(x,y));
                yield return new WaitForSeconds(0.05f);
            }
            weightAngle += 5;
            yield return new WaitForSeconds(attackRate);
        }
    }

    IEnumerator CircleShoot()
    {
        float weightAngle = 0f;
        float attackRate = 12f;
        float intervalAngle = 360 / 18;

        while(true)
        {
            Hoon_AudioManager.instance.SFXWhirlpool();
            
            for(int j = 0; j < 5; j++)
            {
                for(int i = 0; i< 18; i++)
                {            
                    GameObject clone = Instantiate(whirl, pos1.transform.position, Quaternion.identity);
                    float angle = weightAngle + intervalAngle * i;
                    
                    float x = Mathf.Cos(angle * Mathf.Deg2Rad);
                    float y = Mathf.Sin(angle * Mathf.Deg2Rad);

                    clone.GetComponent<Whirl>().Move(new Vector2(x, y));
                }

                if (weightAngle >= 360)
                    weightAngle = 0;
                else { weightAngle += 15; } 
                yield return new WaitForSeconds(0.5f);
            }
 
            yield return new WaitForSeconds(attackRate);
        }
    }
    
    IEnumerator WaveBarrage()
    {
        float frequency = 0.4f;
        float amplitude = 0.4f;
        float speed = 0.5f;
        while (true)
        {
            for (int i = 0; i < 36; i++)
            {
                float x = Mathf.Sin((Time.time + i) * frequency) * amplitude;
                Shoot(new Vector2(x, -speed));
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(5);
        }
    }

    void ClearBattleObjects()
        {
        // PlayerBullet 태그를 가진 모든 오브젝트 제거
        GameObject[] playerBullets = GameObject.FindGameObjectsWithTag("PlayerBullet");
        foreach (GameObject bullet in playerBullets)
        {
            Destroy(bullet);
        }

        // EnemyBullet 태그를 가진 모든 오브젝트 제거
        GameObject[] enemyBullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
        foreach (GameObject bullet in enemyBullets)
        {
            Destroy(bullet);
        }

        // Enemy 태그를 가진 모든 오브젝트 제거 (자신 제외)
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Hoon_Enemy");
        foreach (GameObject enemy in enemies)
        {
            // 자기 자신이 아닌 경우에만 파괴
            if (enemy != gameObject)
            {
                Destroy(enemy);
            }
        }
    }
        
    void ResetPlayerColor()
    {
        // 플레이어 찾기
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            // SpriteRenderer 컴포넌트 가져오기
            SpriteRenderer playerSprite = player.GetComponent<SpriteRenderer>();
            if (playerSprite != null)
            {
                // 스프라이트 색상을 흰색(원래 색상)으로 복구
                playerSprite.color = Color.white;
            }
        }
    }
    void WinBoss()
    {
        CancelInvoke();
        ResetPlayerColor();
        ClearBattleObjects();
        BossBGM.instance.StartWinTimeline();
    }


}