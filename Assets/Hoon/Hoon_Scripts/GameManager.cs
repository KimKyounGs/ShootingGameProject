using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int exp = 0;
    public int level = 1;
    public Image expUI;
    public TMP_Text levelUI;
    public void Start()
    {
        expUI.fillAmount = 0;
    }

    public void Update()
    {
        if(expUI.fillAmount >= 1)
        {
            expUI.fillAmount --;
            level ++;
            levelUI.text = "Lv" + level.ToString();
        }
        
    }
    public void ExpGain(float exp)
    {
        Debug.Log(exp);
        expUI.fillAmount += exp;
    }
}
