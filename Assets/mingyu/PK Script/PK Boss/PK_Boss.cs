using System.Collections;
using TMPro;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class PK_Boss : MonoBehaviour
{
    float B_HP = 100;

    public GameObject mb;
    public GameObject mb2;
    public Transform pos1;
    public Transform pos2;

    public bool boss_attack = true;
    public int attack_chose = 0;

    public bool skill_1 = true;



    public bool skill_2 = true;




    public float cool_time = 0;


    private void Start()
    {

    }

    private void Update()
    {

        cool_time += Time.deltaTime;


        if (boss_attack == true)
        {
            attack_chose = Random.Range(2, 2);
            if (attack_chose > 0)
            {
                boss_attack = false;
            }
        }


        if (attack_chose == 1)
        {
            skill_1 = true;

            if (attack_chose == 1 && skill_1 == true)
            {
                BossAttack1();
            }
        }


        if (attack_chose == 2)
        {
            skill_2 = true;

            if (attack_chose == 2 && skill_2 == true)
            {
                BossAttack2();
            }
        }

        //if (attack_chose > 2)
        //{
        //    cool_time = 0;
        //    boss_attack = false;
        //}


    }





    public void BossAttack1()
    {
        float skill_1_speed = 50;
        float skill_1_target = -2;
        float skill_1_turn = 0;

        transform.Translate(skill_1_target * skill_1_speed * Time.deltaTime, 0, 0);


        if (cool_time > 3)
        {
            skill_1_speed = 50f;
            cool_time = 0;
        }


        if (cool_time > 0.15 && cool_time < 2)
        {
            Instantiate(mb2, pos2.position, Quaternion.identity);
            cool_time = 0;
        }


        if (transform.position.x <= -3)
        {
            skill_1_speed = 1f;
            skill_1_target = 3;
        }

        if (transform.position.x >= 3)
        {
            skill_1_speed = 1f;
            skill_1_target = -3;
            skill_1_turn += 1;
        }

        if (skill_1_turn == 2 && transform.position.x >= 3)
        {
                skill_1_speed = 10f;
            skill_1_target = -3;
        }

        if (skill_1_turn == 2 && transform.position.x <= 0)
        {
            attack_chose = 0;
            skill_1_turn = 0;
            skill_1_speed = 50f;
            skill_1 = false;
            boss_attack = true;
        }

    }



    public void BossAttack2()
    {
        float skill_2_speed = 10;
        float skill_2_target = -2;
        bool skill_2_move = true;
        skill_2 = false;
        if (skill_2_move == true)
        {
            transform.Translate(0, skill_2_target * skill_2_speed * Time.deltaTime, 0);
        }

        if (cool_time > 3)
        {
            skill_2_speed = 50f;
            cool_time = 0;
        }


        if (cool_time > 0.15 && cool_time < 2)
        {
            Instantiate(mb2, pos2.position, Quaternion.identity);
            cool_time = 0;
        }


        if (transform.position.y <= 0f)
        {
            Debug.LogWarning("됨");
            skill_2_move = false;
            skill_2_speed = 1f;
        }

    }















    public void Damage(int attack)  //플레이어에게 데미지를 입는 함수
    {
        B_HP -= attack;
        if (B_HP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
