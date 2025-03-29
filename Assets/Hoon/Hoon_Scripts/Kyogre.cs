
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
    public bool Pattern5 = false;
    protected override void Start()
    {
        base.Start();
        moveSpeed = 0;
        HP = 1000;
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
        for(float i = 0; i<3; i+=0.5f)
        {
            Instantiate(sharpedo, new Vector3(-2.5f + i, 5, 0f), Quaternion.identity);
        }
        
    }
    protected override void Update()
    {
        BossHPUI.fillAmount = HP / 1000;
        if(BossHPUI.fillAmount <= 0)
        {
            BossHPUI.fillAmount = 0;
            WinBoss();
        }
        
        if (HP < 900 && !Pattern2)
        {
            InvokeRepeating("SpawnSharpedo", 0, 15);
            Pattern2 = true; 
        }
        if (HP < 800 && !Pattern3)
        {
            Invoke("WhirlPool3", 1);
            Pattern3 = true; 
        }
        if (HP < 650 && !thunderOn)
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
        if (HP < 500 && !Pattern4)
        {
            
            Pattern4 = true; 
        }

          
    }


    void ThunderCloud()
    {
        Instantiate(thunderCloud, Vector3.zero, Quaternion.identity);
    }
    public void CastThunder()
    {
        float thunderX = Hoon_Player.instance.transform.position.x;
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

    void WinBoss()
    {

    }


}