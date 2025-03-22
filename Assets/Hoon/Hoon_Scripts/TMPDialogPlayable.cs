using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class TMPDialogPlayable : PlayableBehaviour
{
    public string dialogText; // Timeline에서 설정할 대사

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        TMP_Text tmp = playerData as TMP_Text;
        if (tmp != null)
        {
            tmp.text = dialogText;
        }
    }
}
