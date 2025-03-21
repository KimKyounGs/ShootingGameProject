using System.Collections;
using TMPro;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class PK_Boss : MonoBehaviour
{
    float B_HP = 100;

    public GameObject mb1;
    public GameObject mb2;
    
    public Transform pos1;

    public bool boss_attack = true;
    public int attack_chose = 0;

    public bool skill_1 = true;
    public float skill_1_speed = 9;
    public float skill_1_target = -2;
    public float skill_1_turn = 0;


    public bool skill_2 = true;
    public bool skill_2_move1 = true;
    public bool skill_2_move2 = false;

    public GameObject Bullet_3;
    public bool skill_3 = true;


    public float cool_time = 0;


    private void Start()
    {

    }

    private void Update()
    {
        cool_time += Time.deltaTime;


        if (boss_attack == true && cool_time >= 3)
        {
            attack_chose = Random.Range(3, 3);
            Debug.LogWarning(attack_chose);
            if (attack_chose > 0)
            {
                boss_attack = false;
            }
        }



        if (attack_chose == 1 && skill_1 == true)
        {
            BossAttack1();
        }




        if (attack_chose == 2 && skill_2 == true)
        {
            BossAttack2();
        }


        if (attack_chose == 3 && skill_2 == true)
        {
            BossAttack3();
        }

        //if (attack_chose > 2)
        //{
        //    cool_time = 0;
        //    boss_attack = false;
        //}


    }





    public void BossAttack1()
    {

        transform.Translate(skill_1_target * skill_1_speed * Time.deltaTime, 0, 0);


        if (cool_time > 3)
        {
            cool_time = 0;
        }


        if (cool_time > 0.15 && cool_time < 2)
        {
            Instantiate(mb1, pos1.position, Quaternion.identity);
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
            skill_1_speed = 9f;
            cool_time = 0;
            boss_attack = true;
        }

    }



    public void BossAttack2()
    {
        float skill_2_speed = 2;
        float skill_2_target = -2;
        bool skill_2_bullet = false;

        if (skill_2_move1 == true)  // 플레이어 중앙 이동후 움직임 멈추기
        {
            transform.Translate(0, skill_2_target * skill_2_speed * Time.deltaTime, 0);

            if (transform.position.y <= 0f)
            {
                skill_2_move1 = false;
                skill_2_bullet = true;  //공격 킴
            }
        }


        if (skill_2_bullet == true)    //공격 한번만 반복되게 하기
        {
            StartCoroutine(CircleFire());
            cool_time = 0;
            skill_2_bullet = false; //공격 끔
        }


        if (skill_2_move2 == true)  //공격이 멈추면 플레이어 원래 위치로 이동후 움직임 멈추기
        {
            skill_2_target = 4;
            transform.Translate(0, skill_2_target * skill_2_speed * Time.deltaTime, 0);

            if (transform.position.y >= 4f) //원래 위치로 이동했다면
            {
                cool_time = 0;
                skill_2_move2 = false;  //움직임 멈추기
                skill_2_move1 = true;
                attack_chose = 0;
                boss_attack = true; //보스 주사위 굴리기
            }
        }




        IEnumerator CircleFire()
        {
            bool a = true;

            //공격주기
            float attackRate = 0.01f;
            //발사체 생성갯수
            int count = 3;
            //발사체 사이의 각도
            float intervalAngle = 360 / count;
            //가중되는 각도(항상 같은 위치로 발사하지 않도록 설정
            float weightAngle = 0f;

            //원 형태로 방사하는 발사체 생성(count 갯수 만큼)
            while (a)
            {

                for (int i = 0; i < count; ++i)
                {
                    //발사체 생성
                    GameObject clone = Instantiate(mb2, transform.position, Quaternion.identity);

                    //발사체 이동 방향(각도)
                    float angle = weightAngle + intervalAngle * i;
                    //발사체 이동 방향(벡터)
                    //Cos(각도)라디안 단위의 각도 표현을 위해 pi/180을 곱함
                    float x = Mathf.Cos(angle * Mathf.Deg2Rad);
                    //sin(각도)라디안 단위의 각도 표현을 위해 pi/180을 곱함
                    float y = Mathf.Sin(angle * Mathf.Deg2Rad);

                    //발사체 이동 방향 설정
                    clone.GetComponent<PK_Boss_Bullet2>().Move(new Vector2(x, y));
                }
                //발사체가 생성되는 시작 각도 설정을 위한변수
                weightAngle += 3;

                //3초마다 미사일 발사
                yield return new WaitForSeconds(attackRate);

                if (cool_time >= 10)        //10초가 지나면 발사를 멈추고 제자리에 돌아가서 공격 뭐할지 정하게 하기
                {
                    a = false;  //총알 발사 멈추기
                    skill_2_move2 = true;   //제자리로 이동시키기
                }
            }



        }




    }






    public void BossAttack3()
    {

        int RandomY = Random.Range(-5, 6); //y
        int RandomX = Random.Range(-3, 4); //x


        Vector3 a = new Vector3(3, RandomY, 0); // 오른쪽
        Vector3 b = new Vector3(-3, RandomY, 0);   //왼쪽
        Vector3 c = new Vector3(RandomX, 5, 0);   //위쪽
        Vector3 d = new Vector3(RandomX, -5, 0);   //아래쪽



        if (cool_time >= 3)
        {
            cool_time = 0;
        }


        if (cool_time >= 0.5)
        {
            cool_time = 0;
            GameObject bulletA = Instantiate(Bullet_3, a, Quaternion.identity);
            GameObject bulletB = Instantiate(Bullet_3, b, Quaternion.identity);
            GameObject bulletC = Instantiate(Bullet_3, c, Quaternion.identity);
            GameObject bulletD = Instantiate(Bullet_3, d, Quaternion.identity);
            bulletB.transform.localScale = new Vector3(-1, 1, 1);
            bulletC.transform.localScale = new Vector3(-1, 1, 1);
        }
    }



    //public void BossAttack() 이걸로 폭죽처럼 만들수 있게 가능함
    //{

    //    Vector3 a = new Vector3(0, -5, 0);
    //    Vector3 b = new Vector3(0, 4, 0);

    //    if (skill_3_Attack_Head == true)
    //    {
    //        Instantiate(Bullet_3, a, Quaternion.identity);   //머리가 되는거 한개만 발사
    //        skill_3_Attack_Head = false;
    //    }
    //    Vector3 LastPosition = Bullet_3.transform.position;


    //    if (cool_time >= 3)
    //    {
    //        cool_time = 0;
    //    }

    //    if (cool_time >= 0.1)
    //    {

    //        Instantiate(Bullet_3_1, LastPosition, Quaternion.identity);
    //        cool_time = 0;
    //    }

    //}




    public void Damage(int attack)  //플레이어에게 데미지를 입는 함수
    {
        B_HP -= attack;
        if (B_HP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
