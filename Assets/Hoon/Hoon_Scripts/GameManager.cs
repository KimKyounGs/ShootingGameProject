using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Playables;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PlayableDirector evolutionTimeline;
    public float exp = 0;
    public int level = 1;
    public float expLeft = 1f;
    public Vector3 centerPos = new Vector3(0, 0, 0);
    public int evolutionLevel = 5;

    public Image expUI;
    public TMP_Text levelUI;
    public TMP_Text nameUI;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else Destroy(gameObject);
    }
    public void Start()
    {
        expUI.fillAmount = 0;
        levelUI.text = "Lv" + level;
        nameUI.text = "잉어킹";
    }

    public void Update()
    {
        expUI.fillAmount = exp / expLeft;
    }
    public void ExpGain(float num)
    {
        exp += num;
        Debug.Log($"경험치 + {num*100}! 현재 경험치: {exp / expLeft * 100}%");

        while (exp >= expLeft)
        {
            LevelUp();
        }
        
    }

    private void LevelUp()
    {
        exp -= expLeft;
        expUI.fillAmount = exp;
        level ++;
        levelUI.text = "Lv" + level;
        expLeft += expLeft / 10f; //10% 더 채워야 레벨업.

        if (level >= evolutionLevel && !Hoon_Player.instance.isEvolved)
        {            
            Hoon_Player.instance.isEvolved = true;
            StartCoroutine(moveCenter());
            evolutionTimeline.Play();
            Time.timeScale = 0;
        }
        
    }
    public void EndTimeline()
    {
        Time.timeScale = 1;
        nameUI.text = "갸라도스";
    }

    IEnumerator moveCenter()
    {
        float moveDuration = 0.8f;
        float elapsed = 0f;
        Vector3 startPos = Hoon_Player.instance.GetComponent<Transform>().position;

        while (elapsed < moveDuration)
        {
            Hoon_Player.instance.GetComponent<Transform>().position = Vector3.Lerp(startPos, centerPos, elapsed / moveDuration);
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        Hoon_Player.instance.GetComponent<Transform>().position = centerPos;
    }

}
