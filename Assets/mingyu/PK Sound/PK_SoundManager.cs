using UnityEngine;

public class PK_SoundManager : MonoBehaviour
{
    //다른 스크립트에서도 쓸 수 있게 만들기
    public static PK_SoundManager instance;

    AudioSource myAudio;

    public AudioClip Player_Swould;
    public AudioClip S_Gage_C;
    public AudioClip S_Gage_F;

    public AudioClip M_Gage_F;

    public AudioClip Upgrade;

    public AudioClip P_bullet;

    public AudioClip P_missile;

    public AudioClip B_bullet3;

    public AudioClip M_bullet1;

    public AudioClip M_bullet2;
    public AudioClip MB_Shadow_On;
    public AudioClip MB_Shadow_Off;

    public AudioClip MB_bullet_L;
    public AudioClip MB_bullet_R;
    public AudioClip MB_bullet2;
    public AudioClip MB_bullet2_1;

    public void Awake()
    {
        if (PK_SoundManager.instance == null)
        {
            PK_SoundManager.instance = this;
        }
    }

    void Start()
    {
        myAudio = GetComponent<AudioSource>();
    }


    public void PlayerSwould()
    {
        myAudio.PlayOneShot(Player_Swould);
    }

    public void S_Gage_Cool()
    {
        myAudio.PlayOneShot(S_Gage_C);
    }

    public void S_Gage_Full()
    {
        myAudio.PlayOneShot(S_Gage_F);
    }

    public void M_Gage_Full()
    {
        myAudio.PlayOneShot(M_Gage_F);
    }

    public void UPGRADE()
    {
        myAudio.PlayOneShot(Upgrade);
    }

    public void P_Bullet()
    {
        float volumeScale = 0.1f; // 볼륨을 50%로 설정 (0.0 ~ 1.0)
        myAudio.PlayOneShot(P_bullet, volumeScale);
    }

    public void P_Missile()
    {
        float volumeScale = 0.5f;
        myAudio.PlayOneShot(P_missile, volumeScale);
    }

    public void B_Bullet3()
    {
        myAudio.PlayOneShot(B_bullet3);
    }

    public void M_Bullet1()
    {
        myAudio.PlayOneShot(M_bullet1);
    }

    public void M_Bullet2()
    {
        myAudio.PlayOneShot(M_bullet2);
    }

    public void MB_Bullet_L()
    {
        myAudio.PlayOneShot(MB_bullet_L);
    }
    public void MB_Bullet_R()
    {
        myAudio.PlayOneShot(MB_bullet_R);
    }

    public void MB_Bullet2()
    {
        myAudio.PlayOneShot(MB_bullet2);
    }   

    public void MB_Bullet2_1()
    {
        myAudio.PlayOneShot(MB_bullet2_1);
    }   

    public void MB_Shadow_on()
    {
        myAudio.PlayOneShot(MB_Shadow_On);
    }
    public void MB_Shadow_off()
    {
        myAudio.PlayOneShot(MB_Shadow_Off);
    }   
}
