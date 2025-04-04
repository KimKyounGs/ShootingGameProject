using System.Collections;
using TMPro;
#if UNITY_EDITOR
using UnityEditor.Timeline.Actions;
#endif
using UnityEngine;
using UnityEngine.Playables;


public class PK_Boss : MonoBehaviour
{
    public float B_HP = 800;
    public bool Boos_Blood = false; //보스 피가 반이하로 떨어지면 true로 바뀜
    public PlayableDirector EndTimeline;

    public GameObject End;
    public GameObject Boss_HPB; //보스 피바를 위한 오브젝트
    public GameObject Bullet_1;
    public GameObject Bullet_2;
    public GameObject Bullet_3;
    public GameObject Bullet_4;
    public GameObject Bullet_5;

    public GameObject Boss_Spawn;


    public Transform pos1;

    public bool boss_attack = true;
    public int attack_chose = 0;
    public float cool_time1 = 0;
    public float cool_time2 = 0;
    bool B_HPP = true; //보스 피바를 위한 오브젝트 활성화
    float boss_attack_time = 3; //보스 공격 주기

    private void Update()
    {
        cool_time1 += Time.deltaTime;
        cool_time2 += Time.deltaTime;



        if(Boos_Blood == true)
        {
            boss_attack_time = 1.5f; //보스 공격 주기
        }




        if (boss_attack == true && cool_time1 >= boss_attack_time)
        {
            attack_chose = Random.Range(1, 6);

            if (attack_chose > 0)
            {
                cool_time1 = 0;
                cool_time2 = 0;
                boss_attack = false;
            }
        }



        if (attack_chose == 1)
        {
            BossAttack1();
        }


        if (attack_chose == 2)
        {
            BossAttack2();
        }


        if (attack_chose == 3)
        {
            BossAttack3();
        }


        if (attack_chose == 4)
        {
            BossAttack4();
        }


        if (attack_chose == 5)
        {
            BossAttack5();
        }


        if (B_HP <= 400)
        {
            Boos_Blood = true;
            Boss_Spawn.gameObject.SetActive(true);
            if (B_HPP == true)
            {
                Boss_HPB.SetActive(true); //보스 피바를 위한 오브젝트 활성화
                B_HPP = false; //보스 피바를 위한 오브젝트 활성화
            }
            
        }

    }




    public float skill_1_speed = 7;
    public float skill_1_target = -2;
    public float skill_1_turn = 0;

    float skill_1_SP_cool_time = 0.15f;
    int skill_1_SP_Turn = 2;

    public void BossAttack1()
    {

        transform.Translate(skill_1_target * skill_1_speed * Time.deltaTime, 0, 0);

        if (Boos_Blood == true)
        {
            skill_1_SP_cool_time = 0.08f;
            skill_1_SP_Turn = 4;
        }


        if (cool_time1 > skill_1_SP_cool_time && cool_time1 < 2)
        {
            Instantiate(Bullet_1, pos1.position, Quaternion.identity);
            PK_SoundManager.instance.B_Bullet1(); // 총알 발사 소리 재생
            cool_time1 = 0;
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

        if (skill_1_turn == skill_1_SP_Turn && transform.position.x >= 3)
        {
            skill_1_speed = 10f;
            skill_1_target = -3;
        }

        if (skill_1_turn == skill_1_SP_Turn && transform.position.x <= 0)
        {
            attack_chose = 0;
            skill_1_turn = 0;
            skill_1_speed = 9f;
            cool_time1 = 0;
            boss_attack = true;
        }

    }



    public bool skill_2_move1 = true;
    public bool skill_2_move2 = false;
    public float skill_2_speed = 2;

    public void BossAttack2()
    {
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
            PK_SoundManager.instance.B_Bullet2(); // 총알 발사 소리 재생
            cool_time1 = 0;
            skill_2_bullet = false; //공격 끔
        }

        if (cool_time1 >= 10)        //10초가 지나면 발사를 멈추고 제자리에 돌아가서 공격 뭐할지 정하게 하기
        {
            skill_2_move2 = true;   //제자리로 이동시키기
        }


        if (skill_2_move2 == true)  //공격이 멈추면 플레이어 원래 위치로 이동후 움직임 멈추기
        {
            skill_2_target = 4;
            transform.Translate(0, skill_2_target * skill_2_speed * Time.deltaTime, 0);

            if (transform.position.y >= 4f) //원래 위치로 이동했다면
            {
                skill_2_move2 = false;  //움직임 멈추기
                skill_2_move1 = true;
                attack_chose = 0;
                cool_time1 = 0;
                boss_attack = true; //보스 주사위 굴리기
            }
        }


        

        IEnumerator CircleFire()
        {
            int skill_2_SP_bullet = 3;
            bool a = true;

            if (Boos_Blood == true)
        {
            skill_2_SP_bullet  = 5;
        }

            //공격주기
            float attackRate = 0.01f;
            //발사체 생성갯수
            int count = skill_2_SP_bullet;
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
                    GameObject clone = Instantiate(Bullet_2, transform.position, Quaternion.identity);

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

                if (cool_time1 >= 10)        //10초가 지나면 발사를 멈추고 제자리에 돌아가서 공격 뭐할지 정하게 하기
                {
                    a = false;  //총알 발사 멈추기
                    skill_2_move2 = true;   //제자리로 이동시키기
                }
            }



        }




    }





    float Skill_3_SP_Speed = 1f; //스킬 발사 속도
    public void BossAttack3()
    {
        //활성화가 되어있다면 계속 랜덤 돌림
        int RandomY = Random.Range(-5, 6); //y
        int RandomX = Random.Range(-3, 4); //x


        Vector3 a = new Vector3(3, RandomY, 0); // 오른쪽
        Vector3 b = new Vector3(-3, RandomY, 0);   //왼쪽
        Vector3 c = new Vector3(RandomX, 5, 0);   //위쪽
        Vector3 d = new Vector3(RandomX, -5, 0);   //아래쪽

        if (Boos_Blood == true)
        {
            Skill_3_SP_Speed = 0.5f; //스킬 발사 속도
        }

        if (cool_time1 >= Skill_3_SP_Speed)   //발사 속도
        {
            cool_time1 = 0;
            GameObject bulletA = Instantiate(Bullet_3, a, Quaternion.identity);
            GameObject bulletB = Instantiate(Bullet_3, b, Quaternion.identity);
            GameObject bulletC = Instantiate(Bullet_3, c, Quaternion.identity);
            GameObject bulletD = Instantiate(Bullet_3, d, Quaternion.identity);
            bulletB.transform.localScale = new Vector3(-1, 1, 1);
            bulletC.transform.localScale = new Vector3(-1, 1, 1);
            PK_SoundManager.instance.B_Bullet3(); // 총알 발사 소리 재생
        }

        if (cool_time2 >= 10)   //10초 지나면 공격 멈추고 보스 주사위 굴리기
        {
            cool_time1 = 0;
            cool_time2 = 0;
            attack_chose = 0;   //보스 주사위 값 초기화
            boss_attack = true; //보스 주사위 굴리기
        }
    }











    float skill_4_SP_Speed = 2.5f; //스킬 발사 속도

    public void BossAttack4() //이걸로 폭죽처럼 만들수 있게 가능함
    {

        if (Boos_Blood == true)
        {
            skill_4_SP_Speed = 2f; //스킬 발사 속도
        }

        if (cool_time1 >= skill_4_SP_Speed)
        {
        PK_SoundManager.instance.B_Bullet4();
        Instantiate(Bullet_4, transform.position, Quaternion.identity);
        cool_time1 = 0;
        }


        if (cool_time2 >= 30)   //10초 지나면 보스 멈추고 주사위 굴리기
        {
            cool_time1 = 0;
            cool_time2 = 0;
            attack_chose = 0;   //보스 주사위 값 초기화
            boss_attack = true; //보스 주사위 굴리기
        }
    }



    float skill_5_SP_Speed = 0.3f; //스킬 발사 속도

    public void BossAttack5() //이걸로 폭죽처럼 만들수 있게 가능함
    {

        if (Boos_Blood == true)
        {
            skill_5_SP_Speed = 0.2f; //스킬 발사 속도
        }

        if (cool_time1 >= skill_5_SP_Speed) //발사 속도
        {
            Instantiate(Bullet_5, transform.position, Quaternion.identity);
            PK_SoundManager.instance.B_Bullet5();
            cool_time1 = 0;
        }


        if (cool_time2 >= 20)   //10초 지나면 보스 멈추고 주사위 굴리기
        {
            cool_time1 = 0;
            cool_time2 = 0;
            attack_chose = 0;   //보스 주사위 값 초기화
            boss_attack = true; //보스 주사위 굴리기
        }
    }







    public void Damage(int attack)  //플레이어에게 데미지를 입는 함수
    {
        B_HP -= attack;
        Debug.Log("보스 피 : " + B_HP);
        if (B_HP <= 0)
        {
            StopMainCameraAudio(); // 메인 카메라의 오디오 정지
            
            End.SetActive(true); //게임 오버 화면 활성화
            PK_SoundManager.instance.Endgame();
            cool_time1 = -99999;
            
            Boss_Spawn.gameObject.SetActive(false);
            EndTimeline.Play();
        }
    }

    void StopMainCameraAudio()
    {
        // 메인 카메라의 AudioSource 가져오기
        AudioSource cameraAudio = Camera.main.GetComponent<AudioSource>();

        if (cameraAudio != null)
        {
            // 오디오 정지
            cameraAudio.Stop();
            Debug.Log("메인 카메라의 오디오가 멈췄습니다.");
        }
        else
        {
            Debug.LogError("메인 카메라에 AudioSource가 없습니다.");
        }
    }
}
