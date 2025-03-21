using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float exp = 0;
    public int level = 1;
    public float expLeft = 1f;

    public Image expUI;
    public TMP_Text levelUI;

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
        
    }



}
