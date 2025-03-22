using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class TMPDialogPlayableAsset : PlayableAsset
{
    [TextArea(3, 5)]
    public string dialogText; // Inspector에서 입력할 대사 내용

    // Track Target 연결 (TMP_Text)
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<TMPDialogPlayable>.Create(graph);
        TMPDialogPlayable behaviour = playable.GetBehaviour();
        behaviour.dialogText = dialogText;
        return playable;
    }
}
