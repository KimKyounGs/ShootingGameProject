using UnityEngine;
using System.Collections;

public class PK_Warning : MonoBehaviour
{

    void Start()
    {
        PK_SoundManager.instance.Boss_WArning();
    }


    void Update()
    {
        StartCoroutine("StartGame");
    }


     IEnumerator StartGame()
    {
        int i = 2;

        Time.timeScale = 0;

        while (i > 0)
        {

            yield return new WaitForSecondsRealtime(1); //게임이 멈춰도 동작하는 대기
            i--;

            if (i == 0)
            {
                gameObject.SetActive(false);
                Time.timeScale = 1;
            }
        }

    }
}
