using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;


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
        StartCoroutine(StartWinSequence());
    }
    private IEnumerator StartWinSequence()
    {
        WinTimeline.Play();
        Time.timeScale = 0;
        
        Camera mainCamera = Camera.main;
        Vector3 originalPosition = new Vector3(0, 0, -10f);
        mainCamera.transform.position = originalPosition;
        
        float elapsed = 0f;
        float duration = 7.133f;    
        float magnitude = 0.5f;    
        
        // 타임라인 시작 시간 저장
        double startTime = WinTimeline.time;

        while (WinTimeline.time - startTime < duration)
        {
            elapsed += Time.unscaledDeltaTime;

            float remainingTime = duration - (float)(WinTimeline.time - startTime);
            float currentMagnitude = magnitude * (remainingTime / duration);
            
            float x = Random.Range(-1f, 1f) * currentMagnitude;
            float y = Random.Range(-1f, 1f) * currentMagnitude;
                
            mainCamera.transform.position = new Vector3(
                originalPosition.x + x,
                originalPosition.y + y,
                originalPosition.z
            );
            
            yield return null;
        }
    
    // 카메라 원위치
    StopAllCoroutines();
    mainCamera.transform.position = originalPosition;

}
    public void StartHallOfFame()
    {   
        StopAllCoroutines(); // 이전 코루틴 완전히 정지    
        Camera.main.transform.position = new Vector3(0, 0, -10f); // 카메라 강제 초기화
        // HallOfFameTimeline.Play();
        ResetPlayerColor();
        
        SceneManager.LoadScene("HallOfFame");

        Time.timeScale = 0;
    }

    void ResetPlayerColor()
    {
        // 모든 플레이어 찾기
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            if (player != null)
            {
                // SpriteRenderer 컴포넌트 가져오기
                SpriteRenderer playerSprite = player.GetComponent<SpriteRenderer>();
                if (playerSprite != null)
                {
                    // 스프라이트 색상을 흰색(원래 색상)으로 복구
                    playerSprite.color = Color.white;
                }
            }
        }
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
        StartCoroutine(EndMove());
    }
    IEnumerator EndMove()
    {
        float moveDuration = 2f;
        float elapsed = 0f;
        Vector3 startPos = GameObject.FindWithTag("Player").GetComponent<Transform>().position;
        GameObject.FindWithTag("Player").GetComponent<Hoon_Player>().ani.SetBool("Down", false);
        GameObject.FindWithTag("Player").GetComponent<Hoon_Player>().ani.SetBool("Right", false);
        GameObject.FindWithTag("Player").GetComponent<Hoon_Player>().ani.SetBool("Left", false);
        Vector3 TopPos = new Vector3(0, 6.5f, 0);
        while (elapsed < moveDuration)
        {
            GameObject.FindWithTag("Player").GetComponent<Transform>().position = Vector3.Lerp(startPos, TopPos, elapsed / moveDuration);
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }
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
