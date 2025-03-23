using UnityEngine;
using UnityEngine.Playables;


public class BossBGM : MonoBehaviour
{
    public static BossBGM instance;
    public PlayableDirector BossTimeline;


    AudioSource myAudio;
    public AudioClip VSKyogre;

    private void Awake()
    {
        if(BossBGM.instance == null)
        {
            BossBGM.instance = this;
        }
    }
    void Start()
    {
        myAudio = GetComponent<AudioSource>();        
    }

    public void PlayBGM()
    {
        myAudio.PlayOneShot(VSKyogre);
    }

    public void StartBossTimeline()
    {
        BossTimeline.Play();        
    }
}
