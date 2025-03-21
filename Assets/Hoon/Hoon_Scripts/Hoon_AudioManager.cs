using UnityEngine;

public class Hoon_AudioManager : MonoBehaviour
{
    public static Hoon_AudioManager instance;

    AudioSource myAudio;
    public AudioClip crunch;
    public AudioClip bubble_shoot;
    public AudioClip bubble_pop;
    public AudioClip cryRemoraid;

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

    public void CryRemoraid()
    {
        myAudio.PlayOneShot(cryRemoraid);
    }
}

