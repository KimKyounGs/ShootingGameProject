using UnityEngine;
using UnityEngine.UI;

public class PK_S_Gage2 : MonoBehaviour
{
    public Image myGage;
    public bool a = true;

    void Start()
    {
        
    }


    void Update()
    {

        if (myGage.fillAmount == 1)
        {
            if (a == true)
            {
                PK_SoundManager.instance.S_Gage_Full();
                a = false;
            }
        }

        if (myGage.fillAmount <= 0.3)
        {
            a = true;
        }

    }
}
