using System.Collections;
using UnityEngine;
using UnityEngine.Playables;


public class BossBGM : MonoBehaviour
{
    public static BossBGM instance;
    public PlayableDirector BossTimeline;
    public PlayableDirector WinTimeline;
    public PlayableDirector HallOfFameTimeline;


    AudioSource myAudio;

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
        myAudio.Play();
    }
    public void StopBGM()
    {
        myAudio.Stop();
    }

    public void StartWinTimeline()
    {
        WinTimeline.Play();
        Time.timeScale = 0;

    }

    public void StartHallOfFame()
    {
        HallOfFameTimeline.Play();
    }

    public void StartBossTimeline()
    {
        StartCoroutine(MoveBeyond());
        BossTimeline.Play();
        Time.timeScale = 0;
    }

    public void BGMOff()
    {
        GameObject.Find("Main Camera").GetComponent<AudioSource>().volume = 0;
    }
    
    public void MoveToBoss()
    {
        StartCoroutine(MoveCenter());
    }

    public void MoveForward()
    {
        StartCoroutine(MoveBeyond());
    }
    IEnumerator MoveBeyond()
    {
        float moveDuration = 2f;
        float elapsed = 0f;
        Vector3 startPos = GameObject.FindWithTag("Player").GetComponent<Transform>().position;
        GameObject.FindWithTag("Player").GetComponent<Hoon_Player>().ani.SetBool("Down", false);
        GameObject.FindWithTag("Player").GetComponent<Hoon_Player>().ani.SetBool("Right", false);
        GameObject.FindWithTag("Player").GetComponent<Hoon_Player>().ani.SetBool("Left", false);
        Vector3 TopPos = new Vector3(0, 6.5f, 0);
        Vector3 BottomPos = new Vector3(0, -6.5f, 0);

        while (elapsed < moveDuration)
        {
            GameObject.FindWithTag("Player").GetComponent<Transform>().position = Vector3.Lerp(startPos, TopPos, elapsed / moveDuration);
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }
        GameObject.FindWithTag("Player").GetComponent<Transform>().position = BottomPos;
    }

    IEnumerator MoveCenter() // 필요할까?
    {
        float moveDuration = 2f;
        float elapsed = 0f;
        Vector3 startPos = GameObject.FindWithTag("Player").GetComponent<Transform>().position;
        Vector3 centerPos = new Vector3(0, -1.5f, 0);

        while (elapsed < moveDuration)
        {
            GameObject.FindWithTag("Player").GetComponent<Transform>().position = Vector3.Lerp(startPos, centerPos, elapsed / moveDuration);
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }
        GameObject.FindWithTag("Player").GetComponent<Transform>().position = centerPos;
    }
    public void DestroyForBoss()
    {
        GameObject[] allObjects = FindObjectsByType<GameObject>(FindObjectsSortMode.None);
        foreach (GameObject obj in allObjects)
        {
            if (obj.tag == "PlayerBullet")
                DestroyImmediate(obj);
        }
    }
    public void EndTimeline()
    {
        Time.timeScale = 1;
    }
}
