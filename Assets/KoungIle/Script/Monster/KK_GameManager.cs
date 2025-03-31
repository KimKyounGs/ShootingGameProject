using UnityEngine;
using UnityEngine.Playables;

public class KK_GameManager : MonoBehaviour
{
    public PlayableDirector director;

    void Start()
    {
        if (director != null)
        {
            director.gameObject.SetActive(true);
            // 1. 게임을 일시정지
            Time.timeScale = 0f;

            // 2. 타임라인 자동 재생
            director.Play();

            // 3. 타임라인 끝나면 다시 시간 흐르게
            director.stopped += OnTimelineEnd;
        }
    }

    void OnTimelineEnd(PlayableDirector pd)
    {
        // 시간 다시 흐르게 설정
        Time.timeScale = 1f;

        // 더 이상 이벤트 필요 없으니 제거
        director.stopped -= OnTimelineEnd;

        Debug.Log("타임라인 끝! 게임 재개됨.");
    }
}
