using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Jino_SpawnManager : MonoBehaviour
{
    public float ss = -2f;
    public float es = 2f;

    public GameObject missile1;
    public GameObject SideLazer_L;
    public GameObject SideLazer_R;

    void Start()
    {
        Invoke("MissileLine", 0.5f);
        Invoke("Lining_Enemy_L", 13f);
        Invoke("Lining_Enemy_R", 13f);
        Invoke("StartShake", 12.5f);
    }

    void MissileLine()
    {
        for (float i = ss; i <= es; i += 0.4f)
        {
            Vector2 r = new Vector2(i, transform.position.y);
            Instantiate(missile1, r, Quaternion.identity);
        }
        
    }

    void Lining_Enemy_L()
    {
        Vector2 start = new Vector2(-4f, -4.5f);
        Instantiate(SideLazer_L, start, Quaternion.identity);
        Instantiate(SideLazer_L, start + new Vector2(0, 1.5f), Quaternion.identity);
        Instantiate(SideLazer_L, start + new Vector2(0, 3f), Quaternion.identity);
    }

    void Lining_Enemy_R()
    {
        Vector2 start = new Vector2(4f, -3.8f);
        Instantiate(SideLazer_R, start, Quaternion.identity);
        Instantiate(SideLazer_R, start + new Vector2(0, 1.5f), Quaternion.identity);
        Instantiate(SideLazer_R, start + new Vector2(0, 3f), Quaternion.identity);
    }

    void StartShake()
    {
        StartCoroutine("Shake");
    }

    IEnumerator Shake()
    {
        yield return new WaitForSeconds(0.2f);
        CameraShake.instance.CameraShakeShow();
        yield return new WaitForSeconds(0.2f);
        CameraShake.instance.CameraShakeShow();
        yield return new WaitForSeconds(0.2f);
        CameraShake.instance.CameraShakeShow();
        yield return new WaitForSeconds(0.2f);
        CameraShake.instance.CameraShakeShow();
        yield return new WaitForSeconds(0.2f);
        CameraShake.instance.CameraShakeShow();
        yield return new WaitForSeconds(0.2f);
        CameraShake.instance.CameraShakeShow();
        yield return new WaitForSeconds(0.2f);
        CameraShake.instance.CameraShakeShow();
        yield return new WaitForSeconds(0.2f);
        CameraShake.instance.CameraShakeShow();
    }
}
