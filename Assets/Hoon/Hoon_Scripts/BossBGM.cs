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
        StartCoroutine(VictoryShake());
        WinTimeline.Play();
        Time.timeScale = 0;
    }
    private IEnumerator StartHallOfFameSequence()
    {
        // 먼저 카메라 흔들기 효과 실행 및 완료 대기
        yield return StartCoroutine(VictoryShake());
        
        // 카메라 흔들림이 완전히 끝난 후 타임라인 재생
        HallOfFameTimeline.Play();
        ResetPlayerColor();
        Time.timeScale = 0;
    }

    private IEnumerator VictoryShake()
    {
        // 메인 카메라와 원래 위치 가져오기
        Camera mainCamera = Camera.main;
        Vector3 originalPosition = new Vector3(0,0, -10f);
        mainCamera.transform.position = originalPosition;

        
        float elapsed = 0f;
        float duration = 1.5f;     // 카메라 흔들림 시간
        float magnitude = 0.5f;  // 카메라 흔들림 강도
        
        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime; // TimeScale이 0일 때도 작동하도록
            float percentComplete = elapsed / duration;
            
            // 시간이 지날수록 흔들림이 강해짐
            float intensifier = Mathf.Clamp01(percentComplete);
            
            // 랜덤한 흔들림 생성
            float x = Random.Range(-1f, 1f) * magnitude * intensifier;
            float y = Random.Range(-1f, 1f) * magnitude * intensifier;
            
            mainCamera.transform.position = new Vector3(
                originalPosition.x + x,
                originalPosition.y + y,
                originalPosition.z
            );
        }

        // 카메라 원위치
        mainCamera.transform.position = originalPosition;
        yield return null;

    }


    public void StartHallOfFame()
    {       
        StartCoroutine(StartHallOfFameSequence());
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
