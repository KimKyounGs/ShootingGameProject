using UnityEngine;
using UnityEngine.UI;

public class KK_UIManager : MonoBehaviour
{
 public static KK_UIManager Instance;

    [Header("🔫 파워 UI")]
    public Image powerIcon;
    public Text powerText;

    [Header("❤️ 목숨 UI")]
    public Image lifeIcon;
    public Text lifeText;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void UpdatePowerUI(int power)
    {
        powerText.text = $"x {power}";
        // 파워 아이콘 교체하려면 여기에 추가
    }

    public void UpdateLifeUI(int life)
    {
        lifeText.text = $"x {life}";
        // 라이프 아이콘 교체하려면 여기에 추가
    }
}
