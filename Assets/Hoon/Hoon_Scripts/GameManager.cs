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
    public int evolutionLevel = 20;

    public Image expUI;
    public TMP_Text levelUI;
    public TMP_Text nameUI;
    public TMP_Text dashNameUI;
    public GameObject gyarados;
    public GameObject magikarp;

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
        dashNameUI.text = "튀어오르기";

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
        dashNameUI.text = "폭포오르기";
        Hoon_Player.instance.dashUI.fillAmount = 1f;
        Hoon_Player.instance.dashNameUI.color = new Color(1, 1, 1, 1f);
        Hoon_Player.instance.CanDash = true;
        Hoon_Player.instance.HP = 90;
        Hoon_Player.instance.maxHP = 90;
        Hoon_Player.instance.HPLeftUI.text = ($"90 / 90");

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
