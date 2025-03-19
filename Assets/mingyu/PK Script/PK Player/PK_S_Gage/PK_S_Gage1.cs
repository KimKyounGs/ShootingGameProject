using UnityEngine;

using System.Collections;
using System.Threading;
using UnityEngine;

public class PK_SP_Gage1 : MonoBehaviour
{
    public GameObject Swould_Damage;
    bool swi = true;

    void Start()
    {
    }


    void Update()
    {
        StartCoroutine("RandomSpawn");
        StartCoroutine("StartGame");
    }

    IEnumerator StartGame()
    {
        int i = 3;

        Time.timeScale = 0;

        while (i > 0)
        {
            yield return new WaitForSecondsRealtime(1); //게임이 멈춰도 동작하는 대기
            i--;

            if (i == 0)
            {
                gameObject.SetActive(false);
                Swould_Damage.SetActive(true);
                swi = false;
                Time.timeScale = 1;
            }
        }

    }


    IEnumerator RandomSpawn()
    {
        while (swi)
        {
            //1초마다
            yield return new WaitForSecondsRealtime(1); //게임이 멈춰도 동작하는 대기
        }
    }
}

