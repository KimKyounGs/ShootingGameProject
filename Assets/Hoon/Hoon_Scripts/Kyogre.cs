
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
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
        Instantiate(waterRing, pos1.position, Quaternion.identity);
    }

    protected override void Update()
    {
        BossHPUI.fillAmount = HP / 1000;
        if (HP < 990)
        {
            StartCoroutine(raindrop());
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