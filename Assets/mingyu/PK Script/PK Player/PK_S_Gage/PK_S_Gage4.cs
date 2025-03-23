using UnityEngine;
using UnityEngine.UI;

public class PK_S_Gage4 : MonoBehaviour
{
    public Image myGage;
    public bool a = true;

    void Start()
    {
        
    }

    void Update()
    {

        if (myGage.fillAmount == 0)
        {
            if (a == true)
            {
                PK_SoundManager.instance.S_Gage_Cool();
                a = false;
            }
        }

        if (myGage.fillAmount >= 0.9)
        {
            a = true;
        }
    }
}
