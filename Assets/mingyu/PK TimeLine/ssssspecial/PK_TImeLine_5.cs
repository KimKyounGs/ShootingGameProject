using UnityEngine;

using System.Collections;
using System.Threading;
using UnityEngine.SceneManagement;

public class PK_TImeLine_5 : MonoBehaviour
{


    void Start()
    {
    }


    void Update()
    {
        StartCoroutine("StartGame");
    }


     IEnumerator StartGame()
    {
        int i = 8;

        Time.timeScale = 0;

        while (i > 0)
        {

            yield return new WaitForSecondsRealtime(1); //게임이 멈춰도 동작하는 대기
            i--;

            if (i == 4)
            {
                // 씬 전환
                SceneManager.LoadScene("KyoungIleScean");
            }
        }

    }
}
