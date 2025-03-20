using Unity.Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;

    private CinemachineImpulseSource impulseSource;

    void Start()
    {
        instance = this;
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    
    public void CameraShakeShow()
    {
        if(impulseSource != null)
        {
            impulseSource.GenerateImpulse();
        }
    }
}
