using UnityEngine;

public class KK_SoundManager : MonoBehaviour
{
    public static KK_SoundManager Instance;

    [Header("BGM")]
    public AudioSource bgmSource;
    public AudioClip[] bgmClips;

    [Header("효과음")]
    public AudioClip[] fxClips;
    public int fxPoolSize = 10;
    private AudioSource[] fxPool;

    private void Awake()
    {
        // 싱글톤
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // FX 풀 생성
        fxPool = new AudioSource[fxPoolSize];
        for (int i = 0; i < fxPoolSize; i++)
        {
            GameObject fxGO = new GameObject("FXAudio_" + i);
            fxGO.transform.SetParent(transform);
            AudioSource fxSource = fxGO.AddComponent<AudioSource>();
            fxPool[i] = fxSource;
        }
    }

    void Start()
    {
        bgmSource = gameObject.AddComponent<AudioSource>();
        PlayBGM(0); // 기본 BGM 재생
    }

    // BGM 재생
    public void PlayBGM(int index)
    {
        if (index < 0 || index >= bgmClips.Length) return;
        if (bgmSource.clip == bgmClips[index]) return;

        bgmSource.clip = bgmClips[index];
        bgmSource.loop = true;
        bgmSource.Play();
    }

    public void StopBGM() => bgmSource.Stop();
    public void PauseBGM() => bgmSource.Pause();
    public void ResumeBGM() => bgmSource.UnPause();

    // 효과음 재생 (중첩 가능)
    public void PlayFX(int index, float volume = 1f)
    {
        if (index < 0 || index >= fxClips.Length) return;

        foreach (AudioSource fxSource in fxPool)
        {
            if (!fxSource.isPlaying)
            {
                fxSource.clip = fxClips[index];
                fxSource.volume = volume;
                fxSource.Play();
                return;
            }
        }

        // 풀에 빈 자리가 없을 경우에도 하나 강제로 재생
        fxPool[0].clip = fxClips[index];
        fxPool[0].volume = volume;
        fxPool[0].Play();
    }
}
