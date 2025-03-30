using NUnit.Framework.Constraints;
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
    public GameObject Circling_Monster_L;
    public GameObject Circling_Monster_LD;
    public GameObject Circling_Monster_R;
    public GameObject Circling_Monster_RD;
    

    void Start()
    { 
        Invoke("MissileLine", 0.5f);
        Invoke("Lining_Enemy_L", 13f);
        Invoke("Lining_Enemy_R", 13f);
        Invoke("StartShake", 12.5f);
        Invoke("first_CrossLazer_L", 19.1f);
        Invoke("first_CrossLazer_R", 19.3f);
        Invoke("Circling_MonsterLD", 22f);
        Invoke("Circling_MonsterRD", 22f);
        Invoke("Circling_Missile_ld", 24f);
        Invoke("Circling_Missile_rd", 24.2f);
        Invoke("Lazer_L1", 27.3f);
        Invoke("Lazer_R1", 27.5f);
        StartCoroutine("Triple_Circling_R", 30f);
        Invoke("Circling_MonsterL", 32.1f);
        Invoke("Circling_MonsterR", 32.1f);
        Invoke("Circling_MonsterL", 32.3f);
        Invoke("Circling_MonsterR", 32.3f);
        Invoke("first_CrossLazer_L", 34.2f);
        Invoke("first_CrossLazer_R", 34.4f);
        Invoke("Lazer_R2", 38.8f);
        Invoke("Lazer_R3", 38.95f);
        Invoke("Lazer_R4", 39f);
        StartCoroutine("Triple_Circling_L", 44.3f);
        StartCoroutine("Triple_Circling_R", 44.3f);
        StartCoroutine("Triple_Circling_ld", 44.3f);
        StartCoroutine("Triple_Circling_rd", 44.3f);
        Invoke("MissileLine", 50.9f);
    }

    void MissileLine()
    {
        for (float i = ss; i <= es; i += 0.4f)
        {
            Vector2 r = new Vector2(i, transform.position.y);
            Instantiate(missile1, r, Quaternion.identity);
        }
        
    }

    void Lazer_L1()
    {
        Instantiate(SideLazer_L, new Vector3(-4f, -2.8f), Quaternion.identity);
    }

    void Lazer_R1()
    {
        Instantiate(SideLazer_R, new Vector3(4f, -4.5f), Quaternion.identity);
    }

    void Lazer_R2()
    {
        Instantiate(SideLazer_R, new Vector3(4f, -0.8f), Quaternion.identity);
    }
    void Lazer_R3()
    {
        Instantiate(SideLazer_R, new Vector3(4f, -2.3f), Quaternion.identity);
    }
    void Lazer_R4()
    {
        Instantiate(SideLazer_R, new Vector3(4f, -3.8f), Quaternion.identity);
    }

    void Lining_Enemy_L()
    {
        Vector2 start = new Vector2(-4f, -4.5f);
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
        Instantiate(Circling_Missile_L, new Vector3(-2.8f, 0, 0), Quaternion.identity);
    }

    IEnumerator Triple_Circling_L(float delay)
    {
        yield return new WaitForSeconds(delay);
        Instantiate(Circling_Missile_L, new Vector3(-2.8f, 0, 0), Quaternion.identity);
        yield return new WaitForSeconds(0.1f);
        Instantiate(Circling_Missile_L, new Vector3(-2.8f, -2f, 0), Quaternion.identity);
        yield return new WaitForSeconds(0.1f);
        Instantiate(Circling_Missile_L, new Vector3(-2.8f, -4f, 0), Quaternion.identity);
    }

    void Circling_Missile_ld()
    {
        Circling_Missile_L missile = Instantiate(Circling_Missile_LD, new Vector3(-2.8f, 0, 0), Quaternion.identity).GetComponent<Circling_Missile_L>();
        missile.SetDown(true);
    }

    IEnumerator Triple_Circling_ld(float delay)
    {
        yield return new WaitForSeconds(delay);
        Circling_Missile_L missile1 = Instantiate(Circling_Missile_LD, new Vector3(-2.8f, 0, 0), Quaternion.identity).GetComponent<Circling_Missile_L>();
        missile1.SetDown(true);
        yield return new WaitForSeconds(0.1f);
        Circling_Missile_L missile2 = Instantiate(Circling_Missile_LD, new Vector3(-2.8f, 2f, 0), Quaternion.identity).GetComponent<Circling_Missile_L>();
        missile2.SetDown(true);
        yield return new WaitForSeconds(0.1f);
        Circling_Missile_L missile3 = Instantiate(Circling_Missile_LD, new Vector3(-2.8f, 4f, 0), Quaternion.identity).GetComponent<Circling_Missile_L>();
        missile3.SetDown(true);
    }

    void Circling_Missile_r()
    {
        Instantiate(Circling_Missile_R, new Vector3(2.8f, 0, 0), Quaternion.identity);
    }

    IEnumerator Triple_Circling_R(float delay)
    {
        yield return new WaitForSeconds(delay);
        Instantiate(Circling_Missile_R, new Vector3(2.8f, 0, 0), Quaternion.identity);
        yield return new WaitForSeconds(0.1f);
        Instantiate(Circling_Missile_R, new Vector3(2.8f, -2f, 0), Quaternion.identity);
        yield return new WaitForSeconds(0.1f);
        Instantiate(Circling_Missile_R, new Vector3(2.8f, -4f, 0), Quaternion.identity);
    }

    void Circling_Missile_rd()
    {
        Circling_Missile_R missile = Instantiate(Circling_Missile_RD, new Vector3(2.8f, 0, 0), Quaternion.identity).GetComponent<Circling_Missile_R>();
        missile.SetDown(true);
    }

    IEnumerator Triple_Circling_rd(float delay)
    {
        yield return new WaitForSeconds(delay);
        Circling_Missile_R missile1 = Instantiate(Circling_Missile_RD, new Vector3(2.8f, 0, 0), Quaternion.identity).GetComponent<Circling_Missile_R>();
        missile1.SetDown(true);
        yield return new WaitForSeconds(0.1f);
        Circling_Missile_R missile2 = Instantiate(Circling_Missile_RD, new Vector3(2.8f, 2f, 0), Quaternion.identity).GetComponent<Circling_Missile_R>();
        missile2.SetDown(true);
        yield return new WaitForSeconds(0.1f);
        Circling_Missile_R missile3 = Instantiate(Circling_Missile_RD, new Vector3(2.8f, 4f, 0), Quaternion.identity).GetComponent<Circling_Missile_R>();
        missile3.SetDown(true);
    }

    void Circling_MonsterL()
    {
        Instantiate(Circling_Monster_L, new Vector3(-2.8f, 0, 0), Quaternion.identity);
    }

    void Circling_MonsterR()
    {
        Instantiate(Circling_Monster_R, new Vector3(2.8f, 0, 0), Quaternion.identity);
    }

    void Circling_MonsterLD()
    {
        Instantiate(Circling_Monster_LD, new Vector3(-2.8f, 0, 0), Quaternion.identity);
    }

    void Circling_MonsterRD()
    {
        Instantiate(Circling_Monster_RD, new Vector3(2.8f, 0, 0), Quaternion.identity);
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
