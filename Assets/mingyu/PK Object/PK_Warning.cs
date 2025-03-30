using UnityEngine;
using System.Collections;

public class PK_Warning : MonoBehaviour
{
    public GameObject Warning; //경고 오브젝트

    void Start()
    {
        
    }


    void Update()
    {

        StartCoroutine("StartGame");

    }

    IEnumerator StartGame()
    {
        int i = 10;

        Time.timeScale = 0;

        while (i > 0)
        {

            yield return new WaitForSecondsRealtime(1); //게임이 멈춰도 동작하는 대기
            i--;

            if (i == 0)
            {
                gameObject.SetActive(false);
                Warning.SetActive(true);
                Time.timeScale = 1;
            }
        }

    }
}
