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
    public GameObject CrossLazer_L;
    public GameObject CrossLazer_R;
    public GameObject Circling_Missile_L;
    public GameObject Circling_Missile_R;
    public GameObject Circling_Missile_LD;
    public GameObject Circling_Missile_RD;
    

    void Start()
    {
        //Invoke("Circling_Missile_r", 0.1f);
        //Invoke("Circling_Missile_l", 0.3f);
        //Invoke("Circling_Missile_ld", 0.5f);
        //Invoke("Circling_Missile_rd", 0.7f);
        //StartCoroutine("Triple_Circling_R", 2f);
        //StartCoroutine("Triple_Circling_L", 2.5f);
        //Invoke("MissileLine", 0.5f);
        Invoke("Lining_Enemy_L", 13f);
        Invoke("Lining_Enemy_R", 13f);
        Invoke("StartShake", 12.5f);
        Invoke("first_CrossLazer_L", 20f);
        Invoke("first_CrossLazer_R", 20f);
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

    void first_CrossLazer_L()
    {
        Vector2 start = new Vector2(-3.4f, 7.4f);
        Instantiate(CrossLazer_L, start, Quaternion.identity);
    }

    void first_CrossLazer_R()
    {
        Vector2 start = new Vector2(3.4f, 7.4f);
        Instantiate(CrossLazer_R, start, Quaternion.identity);
    }

    void Circling_Missile_l()
    {
        Circling_Missile_L missile = Instantiate(Circling_Missile_L).GetComponent<Circling_Missile_L>();
        missile.SetStartpos(new Vector3(-2.8f, 0, 0));
    }

    IEnumerator Triple_Circling_L(float delay)
    {
        yield return new WaitForSeconds(delay);
        Circling_Missile_L missile1 = Instantiate(Circling_Missile_L).GetComponent<Circling_Missile_L>();
        missile1.SetStartpos(new Vector3(-2.8f, 0, 0));
        yield return new WaitForSeconds(0.2f);
        Circling_Missile_L missile2 = Instantiate(Circling_Missile_L).GetComponent<Circling_Missile_L>();
        missile2.SetStartpos(new Vector3(-2.8f, -2f, 0));
        yield return new WaitForSeconds(0.2f);
        Circling_Missile_L missile3 = Instantiate(Circling_Missile_L).GetComponent<Circling_Missile_L>();
        missile3.SetStartpos(new Vector3(-2.8f, -4f, 0));
        yield return new WaitForSeconds(0.2f);
    }

    void Circling_Missile_ld()
    {
        Circling_Missile_L missile = Instantiate(Circling_Missile_LD).GetComponent<Circling_Missile_L>();
        missile.SetDown(true);
        missile.SetStartpos(new Vector3(-2.8f, 0, 0));
    }

    void Circling_Missile_r()
    {
        Circling_Missile_R missile = Instantiate(Circling_Missile_R).GetComponent<Circling_Missile_R>();
        missile.SetStartpos(new Vector3(2.8f, 0, 0));
    }

    IEnumerator Triple_Circling_R(float delay)
    {
        yield return new WaitForSeconds(delay);
        Circling_Missile_R missile1 = Instantiate(Circling_Missile_R).GetComponent<Circling_Missile_R>();
        missile1.SetStartpos(new Vector3(2.8f, 0, 0));
        yield return new WaitForSeconds(0.2f);
        Circling_Missile_R missile2 = Instantiate(Circling_Missile_R).GetComponent<Circling_Missile_R>();
        missile2.SetStartpos(new Vector3(2.8f, -2f, 0));
        yield return new WaitForSeconds(0.2f);
        Circling_Missile_R missile3 = Instantiate(Circling_Missile_R).GetComponent<Circling_Missile_R>();
        missile3.SetStartpos(new Vector3(2.8f, -4f, 0));
        yield return new WaitForSeconds(0.2f);
    }

    void Circling_Missile_rd()
    {
        Circling_Missile_R missile = Instantiate(Circling_Missile_RD).GetComponent<Circling_Missile_R>();
        missile.SetDown(true);
        missile.SetStartpos(new Vector3(2.8f, 0, 0));
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
