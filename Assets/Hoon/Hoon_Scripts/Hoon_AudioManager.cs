using UnityEngine;

public class Hoon_AudioManager : MonoBehaviour
{
    public static Hoon_AudioManager instance;

    AudioSource myAudio;
    public AudioClip crunch;
    public AudioClip bubble_shoot;
    public AudioClip bubble_pop;
    public AudioClip cryRemoraid;
    public AudioClip dragonBreath;
    public AudioClip dragonRage1;
    public AudioClip dragonRage2;
    public AudioClip waterfall;
    public AudioClip waterfall2;
    public AudioClip splash;
    public AudioClip iceBeam;
    public AudioClip sheerCold;
    public AudioClip cryClampearl;
    public AudioClip cryHuntail;
    public AudioClip cryGoreByss;
    public AudioClip cryGyarados;
    public AudioClip protect;
    public AudioClip surf;
    public AudioClip thunder;
    public AudioClip hit1;
    public AudioClip hit2;

    public AudioClip whirlpool;
    public AudioClip cryKyogre;

    public AudioClip getItem;

    private void Awake()
    {
        if(Hoon_AudioManager.instance == null) //인스턴스 있는지 검사
        {
            Hoon_AudioManager.instance = this; //자기 자신을 담는다.
        }
    }
    void Start()
    {
        myAudio = GetComponent<AudioSource>();
    }

    public void SFXCrunch()
    {
        myAudio.PlayOneShot(crunch);
    }

    public void SFXBubblePop()
    {
        myAudio.PlayOneShot(bubble_pop);
    }
    public void SFXBubbleShoot()
    {
        myAudio.PlayOneShot(bubble_shoot);
    }
     public void SFXDragonBreath()
    {
        myAudio.PlayOneShot(dragonBreath);
    }

    public void SFXSurf()
    {
        myAudio.PlayOneShot(surf);
    }

    public void SFXThunder()
    {
        myAudio.PlayOneShot(thunder);
    }
    public void SFXWhirlpool()
    {
        myAudio.PlayOneShot(whirlpool);
    }
    public void SFXDragonRage1()
    {
        myAudio.PlayOneShot(dragonRage1);
    }
    public void SFXDragonRage2()
    {
        myAudio.PlayOneShot(dragonRage2);
    }

    public void SFXWaterfall()
    {
        myAudio.PlayOneShot(waterfall);
    }

    public void CryRemoraid()
    {
        myAudio.PlayOneShot(cryRemoraid);
    }

    public void CryKyogre()
    {
        myAudio.PlayOneShot(cryKyogre);
    }

    public void CryClampearl()
    {
        myAudio.PlayOneShot(cryClampearl);
    }

    public void CryHuntail()
    {
        myAudio.PlayOneShot(cryHuntail);
    }
    public void CryGoreByss()
    {
        myAudio.PlayOneShot(cryGoreByss);
    }
    public void CryGyarados()
    {
        myAudio.PlayOneShot(cryGyarados);
    }
    public void SFXProtect()
    {
        myAudio.PlayOneShot(protect);
    }

    public void SFXWaterfall2()
    {
        myAudio.PlayOneShot(waterfall2);
    }

    public void SFXGetItem()
    {
        myAudio.PlayOneShot(getItem);
    }

    public void SFXIceBeam()
    {
        myAudio.PlayOneShot(iceBeam);
    }
    public void SFXSheerCold()
    {
        myAudio.PlayOneShot(sheerCold);
    }
    public void SFXSplash()
    {
        myAudio.PlayOneShot(splash);
    }
    public void SFXHit1()
    {
        myAudio.PlayOneShot(hit1);
    }
    public void SFXHit2()
    {
        myAudio.PlayOneShot(hit2);
    }

}

