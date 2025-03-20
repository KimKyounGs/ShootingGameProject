using System.Collections;
using TMPro;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class PK_Boss : MonoBehaviour
{
    float flag = -2;
    float B_HP = 100;

    public GameObject mb;
    public GameObject mb2;
    public Transform pos1;
    public Transform pos2;

    public bool boss_attack = true;
    public int attack_chose = 0;


    public bool skill_1_move = false;
    public float skill_1_speed = 100;
    public float skill_1_turn = 0;

    public float cool_time = 0;




    private void Update()
    {

        cool_time += Time.deltaTime;

        if (boss_attack == true)
        {
            attack_chose = Random.Range(1, 1);





            if (attack_chose == 1)
            {
                BossMissle();
            }
            else if (attack_chose > 1)
            {
                boss_attack = true;
            }
        }


    }





    public void BossMissle()
    {
        transform.Translate(flag * skill_1_speed * Time.deltaTime, 0, 0);

        if (cool_time > 3)
        {
            cool_time = 0;
        }

        if (skill_1_move == false)
        {
            if (cool_time > 0.15 && cool_time < 2)
            {
                Instantiate(mb2, pos2.position, Quaternion.identity);
                cool_time = 0;
            }
        }


        if (transform.position.x <= -3)
        {
            skill_1_speed = 1f;
            flag = 3;
        }

        if (transform.position.x >= 3)
        {
            skill_1_speed = 1f;
            flag = -3;
            skill_1_turn += 1;
        }

        if (skill_1_turn == 2)
        {
            if (transform.position.x >= 3)
            {
                skill_1_speed = 10f;
                flag = -3;
                skill_1_move = true;
            }

            if (skill_1_move == true && transform.position.x <= 0)
            {
                boss_attack = false;
            }
        }

    }




    public void Damage(int attack)
    {
        B_HP -= attack;
        if (B_HP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
