using UnityEngine;

public class MonsterCrossL : MonoBehaviour
{
    public float moveSpeed = 7;
    public GameObject Gage1;
    public GameObject Lazer1;
    public Transform pos1 = null;
    bool isCrossComing = true;
    bool isEffectEnd = false;
    bool isLazerFired = false;

    Animator Eani;

    void Start()
    {
        Eani = GetComponent<Animator>();
        Invoke("Gaging", 1.5f);
        Invoke("FireLazer", 2.5f);
    }

    
    void Update()
    {
        if(isCrossComing)
        {
            CrossComing();
        }

        if(isEffectEnd)
        {
            AfterEffect();
        }

        if (isLazerFired)
        {
            CrossDown();
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void CrossComing()
    {
        transform.position = Vector2.Lerp(transform.position, new Vector2(-1.7f, 3.7f), moveSpeed * Time.deltaTime);
    }
    
    void CrossDown()
    {
        Vector2 end = new Vector2(5.1f, -11.1f);
        transform.position = Vector2.Lerp(transform.position, end, 0.5f * Time.deltaTime);
    }

    void Gaging()
    {
        Eani.SetBool("engine", true);
        GameObject go = Instantiate(Gage1, pos1.position, Quaternion.identity);
        Destroy(go, 1f);
        isCrossComing = false;
    }

    void FireLazer()
    {
        Vector3 Adjustvec = new Vector2(2.83f, -6.17f);
        Quaternion rotation = Quaternion.Euler(0, 0, 25);
        GameObject fire = Instantiate(Lazer1, pos1.position + Adjustvec, rotation);
        Invoke("EndEffect", 0.1f);
        Destroy(fire, 1.2f);
        Invoke("EndLazer", 1.2f);
    }
    
    void AfterEffect()
    {
        transform.position = Vector2.Lerp(transform.position, new Vector2(-1.80625f, 3.93125f), 0.5f * Time.deltaTime);
    }

    void EndEffect()
    {
        isEffectEnd = true;
    }
    void EndLazer()
    {
        isEffectEnd = false;
        isLazerFired = true;
        Eani.SetBool("engine", false);
    }
}
