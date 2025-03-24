using System.Collections;
using UnityEngine;
using UnityEngine.Playables;


public class BossBGM : MonoBehaviour
{
    public static BossBGM instance;
    public PlayableDirector BossTimeline;
    public Vector3 centerPos = new Vector3(0, 0, 0);



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
        StartCoroutine(MoveBeyond());
        Time.timeScale = 0;
        BossTimeline.Play();        
    }

    public void BGMOff()
    {
        GameObject.Find("Main Camera").GetComponent<AudioSource>().volume = 0;
    }
    
    public void MoveToBoss()
    {
        StartCoroutine(MoveCenter());
    }

    IEnumerator MoveBeyond()
    {
        float moveDuration = 4f;
        float elapsed = 0f;
        Vector3 startPos = GameObject.FindWithTag("Player").GetComponent<Transform>().position;

        while (elapsed < moveDuration)
        {
            GameObject.FindWithTag("Player").GetComponent<Transform>().position = startPos + Vector3.up;
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }
        GameObject.FindWithTag("Player").GetComponent<Transform>().position = new Vector3(0, -6.5f, 0);
    }

    IEnumerator MoveCenter() // 필요할까?
    {
        float moveDuration = 1.2f;
        float elapsed = 0f;
        Vector3 startPos = GameObject.FindWithTag("Player").GetComponent<Transform>().position;
        
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
            if (obj.tag == "Hoon_Monster")
                DestroyImmediate(obj);
            else if (obj.tag == "Enemy_Bullet")
                DestroyImmediate(obj);
        }
    }
    public void EndTimeline()
    {
        Time.timeScale = 1;
    }
}
