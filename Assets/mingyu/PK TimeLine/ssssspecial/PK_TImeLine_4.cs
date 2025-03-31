using UnityEngine;

using System.Collections;
using System.Threading;
using UnityEngine.SceneManagement;

public class PK_TImeLine_4 : MonoBehaviour
{
    public GameObject Gage;

    void Start()
    {
        Gage.SetActive(false); // Gage 오브젝트 비활성화
    }


    void Update()
    {
        StartCoroutine("StartGame");
    }


     IEnumerator StartGame()
    {
        int i = 6;

        Time.timeScale = 0;

        while (i > 0)
        {

            yield return new WaitForSecondsRealtime(1); //게임이 멈춰도 동작하는 대기
            i--;

            if(i == 3)
            {
            
                Time.timeScale = 1;
            }
            if (i == 0)
            {
                gameObject.SetActive(false);
                Gage.SetActive(true); // Gage 오브젝트 활성화
                Time.timeScale = 1;
            
            }
        }

    }
}
