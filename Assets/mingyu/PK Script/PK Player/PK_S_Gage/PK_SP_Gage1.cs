using UnityEngine;

using System.Collections;
using System.Threading;

public class PK_SP_Gage1 : MonoBehaviour
{
    public GameObject Swould_Damage;

    void Start()
    {
        PK_SoundManager.instance.PlayerSwould2();
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
                Swould_Damage.SetActive(true);
                Time.timeScale = 1;
            }
        }

    }
}

