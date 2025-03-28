
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
    }

    void CastWaterRing()
    {
        Hoon_AudioManager.instance.SFXSurf();
        Instantiate(waterRing, pos1.position, Quaternion.identity);
    }

    protected override void Update()
    {
        BossHPUI.fillAmount = HP / 1000;
        

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
        if (Input.GetKeyDown(KeyCode.Q) && !Pattern3)
        {
            Invoke("WhirlPool3", 1);
            Pattern3 = true; 
        } 
        // if (Input.GetKeyDown(KeyCode.Alpha1) && !Pattern4)
        // {
        //     Invoke("WhirlPool4", 1);
        //     Pattern4 = true; 
        // }
        // if (Input.GetKeyDown(KeyCode.Alpha2) && !Pattern5)
        // {
        //     Invoke("WhirlPool5", 1);
        //     Pattern5 = true; 
        // }

          
    }

    void ThunderCloud()
    {
        Instantiate(thunderCloud, Vector3.zero, Quaternion.identity);
    }
    public void CastThunder()
    {
        float thunderX = Random.Range(-2.8f, 2.8f);
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

    void WhirlPool4()
    {
        StartCoroutine(SpiralCombo());
    }

    void WhirlPool5()
    {
        StartCoroutine(RandomSpread());
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
                yield return new WaitForSeconds(1);
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

    IEnumerator SpiralCombo()
    {
        float weightAngle = 0f;
        while (true)
        {
            for (int i = 0; i < 12; i++)
            {
                float angle = weightAngle + (360 / 12) * i;
                Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
                Shoot(direction);
            }
            weightAngle += 10;
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator RandomSpread()
    {
        while (true)
        {
            for (int i = 0; i < 10; i++)
            {
                float angle = Random.Range(0f, 360f);
                Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
                Shoot(direction);
            }
            yield return new WaitForSeconds(1f);
        }
    }

    // void SineWaveShot(float speed, float frequency, float amplitude)
    // {
    //     for (int i = 0; i < 10; i++)
    //     {
    //         GameObject bullet = Instantiate(whirl, transform.position, Quaternion.identity);
    //     float x = Mathf.Sin(Time.time * frequency) * amplitude;
    //     bullet.GetComponent<Whirl>().Move(new Vector3(x, speed * Time.deltaTime, 0));
    //     }       
    // }


    // IEnumerator TrapShot()
    // {
    //     float weightAngle = 0f;
    //     float attackRate = 8f;
    //     float intervalAngle = 360 / 8;

    //     while(true)
    //     {
    //         Hoon_AudioManager.instance.SFXWhirlpool();
    //         for(int i = 0; i < 8; i++)
    //         {
    //             Hoon_AudioManager.instance.SFXWhirlpool();
    //             GameObject player = GameObject.FindWithTag("Player");

    //             WhirlPos = player.transform.position;
    //             GameObject clone = Instantiate(whirl, WhirlPos, Quaternion.identity);
    //             //발사체 이동 방향(각도)
    //             clone.GetComponent<CircleCollider2D>().radius = 0;
    //             float angle = weightAngle + intervalAngle * i;
    //             //발사체 이동 방향(벡터)
    //             float x = Mathf.Cos(angle * Mathf.Deg2Rad);
    //             float y = Mathf.Sin(angle * Mathf.Deg2Rad);
            
    //             clone.GetComponent<Whirl>().Move(new Vector2(x,y));
    //             // }
    //             // else if(distance >=1.5f)
    //             // {
    //             //     clone.GetComponent<CircleCollider2D>().radius = 0.3885524f;
    //             //     clone.GetComponent<Whirl>().Homing(WhirlPos);
    //             // }
    //         }
    //         weightAngle += 1;
    //         yield return new WaitForSeconds(attackRate);
            

    //     }
    // }
}