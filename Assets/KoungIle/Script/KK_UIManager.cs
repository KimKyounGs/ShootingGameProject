using UnityEngine;
using UnityEngine.UI;

public class KK_UIManager : MonoBehaviour
{
    public static KK_UIManager Instance;

    [Header("❤️ 목숨 UI")]
    public Text lifeText;
    [Header("🔫 파워 UI")]
    public Text powerText;


    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void UpdatePowerUI(int power)
    {
        powerText.text = $"{power} lv";
        // 파워 아이콘 교체하려면 여기에 추가
    }

    public void UpdateLifeUI(int life)
    {
        lifeText.text = $"x {life}";
        // 라이프 아이콘 교체하려면 여기에 추가
    }
}
