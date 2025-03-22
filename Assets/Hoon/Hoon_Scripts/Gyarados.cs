using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Gyarados : MonoBehaviour
{
    public float Speed = 5f;
    private Vector2 minBounds;
    private Vector2 maxBounds;
    Animator ani; //애니메이터를 가져올 변수
    public GameObject[] Bullet;
    public Transform pos = null;
    public bool CanShoot = true;
    public int power = 0;
    public bool isEvolved = true;

    public void Shoot()
    {
        StartCoroutine(BulletCooldown());
        CanShoot = false;

        // if (power <= 3)
            Hoon_AudioManager.instance.SFXDragonBreath();
            Instantiate(Bullet[0], pos.transform.position, Quaternion.identity);

        // else if (power <= 6)
        //     Instantiate(Bullet[1], pos.transform.position, Quaternion.identity);
        // else if (power <= 9)
        //     Instantiate(Bullet[2], pos.transform.position, Quaternion.identity);
        // else if (power > 9)
        //     Instantiate(Bullet[3], pos.transform.position, Quaternion.identity);
    }


    IEnumerator BulletCooldown()
    {
        yield return new WaitForSeconds(0.5f);
        CanShoot = true;
    }
    void Start()
    {
        ani = GetComponent<Animator>();

    }
    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     if(collision.CompareTag("Item"))
    //     {
    //         Destroy(collision.gameObject);
    //         SoundManager.instance.SoundItem();
    //         power++;
    //         Debug.Log(power);
    //         if (power>11)
    //             power = 11;
    //         else if (power == 3 || power == 6 || power == 9)
    //         {
    //             GameObject go = Instantiate(PowEffect, transform.position, Quaternion.identity);
    //             Destroy(go, 1);
    //         }

    //     }
    // }
    void Update()
    {


        ani.SetBool("Down", false);    


        // 플레이어 이동
        float moveX = Input.GetAxis("Horizontal") * Speed * Time.deltaTime;
        float moveY = Input.GetAxis("Vertical") * Speed * Time.deltaTime;

        if(Input.GetAxis("Horizontal") <= -0.1)
            ani.SetBool("Left", true);
        else
            ani.SetBool("Left", false);

        if(Input.GetAxis("Horizontal") >= 0.1)
            ani.SetBool("Right", true);
        else
            ani.SetBool("Right", false);

        if(Input.GetAxis("Vertical") <= -0.1)
            ani.SetBool("Down", true);
        else
            ani.SetBool("Down", false);    

        transform.Translate(moveX, moveY, 0);

        // if(Input.GetKey(KeyCode.X))
        // {
        //     SoundManager.instance.SoundLazer();
        //     gValue += Time.deltaTime * 2f;
        //     Debug.Log(gValue);

        //     if(gValue >=1)
        //     {
        //         GameObject go = Instantiate(lazer, pos.position, Quaternion.identity);
        //         Destroy(go, 1);
        //         gValue = 0;
        //     }
        //     else
        //     {
        //         gValue -= Time.deltaTime;
        //         if(gValue <= 0)
        //         {
        //             gValue = 0;
        //         }
        //     }

        // }

        if(Input.GetKey(KeyCode.Space) && CanShoot == true && ani.GetBool("Down") == false)
        {
            // SoundManager.instance.SoundBullet();
            Shoot();
        }

                    
        //캐릭터의 월드 좌표를 뷰포트 좌표계로 변환해준다.
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
        viewPos.x = Mathf.Clamp01(viewPos.x); //x값을 0이상, 1이하로 제한한다.
        viewPos.y = Mathf.Clamp01(viewPos.y); //y값을 0이상, 1이하로 제한한다.
        Vector3 worldPos = Camera.main.ViewportToWorldPoint(viewPos);//다시월드좌표로 변환
        transform.position = worldPos; //좌표를 적용한다.

    }



}

