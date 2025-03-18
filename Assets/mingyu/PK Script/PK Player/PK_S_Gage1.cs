using UnityEngine;
using UnityEngine.UI;

public class PK_S_Gage1 : MonoBehaviour
{
    public int Gage = 0;
    public Image Swoard_Gage;

    void Update()
    {
        
    }


    public void S_Gage(int blood)
    {
        Gage += blood;
        Swoard_Gage.fillAmount += Gage;
    }
}
