
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
//using System.Numerics;
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
    public bool thunderOn = false;
    Vector3 thunderVec = new Vector3(0, 4.5f, 0);

    protected override void Start()
    {
        base.Start();
        moveSpeed = 0;
        HP = 1000;
        exp = 0;

        InvokeRepeating("CastWaterRing", 1, 10);
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
            InvokeRepeating("CastThunder", 1, 5);
            thunderOn = true;
        }
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
            yield return new WaitForSeconds(0.05f);
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
}





        //  잔상 지우기
        // else if(direction.x == 0)
        // {
        //     pAnimator.SetBool("Run", false);


        // }