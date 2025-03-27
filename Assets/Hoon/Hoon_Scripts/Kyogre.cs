
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TreeEditor;
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

    protected override void Start()
    {
        base.Start();
        moveSpeed = 0;
        HP = 1000;
        exp = 0;

        InvokeRepeating("CastWaterRing", 1, 10);
        Invoke("WhirlPool", 5);
    }

    void CastWaterRing()
    {
        Hoon_AudioManager.instance.SFXSurf();
        Instantiate(waterRing, pos1.position, Quaternion.identity);
    }

    protected override void Update()
    {
        BossHPUI.fillAmount = HP / 1000;
        

        if (HP < 990 && !thunderOn)
        {
            Hoon_AudioManager.instance.CryKyogre();
            InvokeRepeating("CastThunder", 1, 7);
            thunderOn = true;
            if (thunderOn == true && Cloud == false);
            {
                Invoke("ThunderCloud", 1);
                Cloud = true;
            }
        }

            
          
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
        StartCoroutine(CircleShoot());
    }
    IEnumerator CircleShoot()
    {
        float weightAngle = 0f;
        float attackRate = 4.5f;
        float intervalAngle = 360 / 16;

        while(true)
        {
            for(int i = 0; i< 16; i++)
            {
                Hoon_AudioManager.instance.SFXWhirlpool();
                //WhirlPos = GameObject.FindWithTag("Player").transform.position;
                GameObject clone = Instantiate(whirl, pos1.transform.position, Quaternion.identity);
                //발사체 이동 방향(각도)
                float angle = weightAngle + intervalAngle * i;
                //발사체 이동 방향(벡터)
                // cos(각도) 라디안 단위의 각도 표현을 위해 pi/180을 곱함
                float x = Mathf.Cos(angle * Mathf.Deg2Rad);
                // sin(각도) 라디안 단위의 각도 표현을 위해 pi/180을 곱함
                float y = Mathf.Sin(angle * Mathf.Deg2Rad);

                clone.GetComponent<Whirl>().Move(new Vector2(x,y));
                yield return new WaitForSeconds(0.05f);
            }

            weightAngle += 1;

            yield return new WaitForSeconds(attackRate);
        }
    }
}