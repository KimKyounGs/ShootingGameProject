using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[TrackColor(0.8f, 0.6f, 0.9f)]
[TrackClipType(typeof(TMPDialogPlayableAsset))]
[TrackBindingType(typeof(TMP_Text))]
public class TMPDialogTrack : TrackAsset
{
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        return ScriptPlayable<TMPDialogPlayableMixer>.Create(graph, inputCount);
    }
}

public class TMPDialogPlayableMixer : PlayableBehaviour
{
}
