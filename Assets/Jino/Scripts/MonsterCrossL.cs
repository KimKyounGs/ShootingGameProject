using UnityEngine;

public class MonsterCrossL : MonoBehaviour
{
    public float moveSpeed = 7;
    public GameObject Gage1;
    public GameObject Lazer1;
    public Transform pos1 = null;
    bool isCrossComing = true;
    bool isLazerFired = false;


    void Start()
    {
        Invoke("Gaging", 1.5f);
    }

    
    void Update()
    {
        if(isCrossComing)
        {
            CrossComing();
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
        Vector2 end = new Vector2(3.4f, -7.4f);
    }

    void Gaging()
    {
        GameObject go = Instantiate(Gage1, pos1.position, Quaternion.identity);
        //Destroy(go, 0.5f);
    }
}
