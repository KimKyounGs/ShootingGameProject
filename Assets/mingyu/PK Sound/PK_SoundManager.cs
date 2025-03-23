using UnityEngine;

public class PK_SoundManager : MonoBehaviour
{
    //다른 스크립트에서도 쓸 수 있게 만들기
    public static PK_SoundManager instance;

    AudioSource myAudio;

    public AudioClip Player_Swould;
    public AudioClip S_Gage_C;
    public AudioClip S_Gage_F;



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
}
