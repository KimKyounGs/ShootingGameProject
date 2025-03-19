using System.Collections;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class Boss : MonoBehaviour
{
    int flag = 1;
    int speed = 2;
    bool onpos = false;

    public GameObject mb1;
    public GameObject mb2;
    public Transform pos1;
    public Transform pos2;

    void Start()
    {
        StartCoroutine("Move", 0);
        Invoke("CircleFire", 3f);
        Invoke("SantanFire1", 5.5f);
        Invoke("SantanFire2", 6.3f);
        Invoke("SantanFire1", 7.5f);
        Invoke("SantanFire2", 7.9f);
    }

    
    void Update()
    {
        if (transform.position.x >= 1)
        {
            flag *= -1;
        }
        if (transform.position.x <= -1)
        {
            flag *= -1;
        }

        transform.Translate(flag * speed * Time.deltaTime, 0, 0);
    }

    IEnumerator Move()
    {
        onpos = true; 
        while(onpos)
        {
            transform.position = Vector2.Lerp(transform.position, new Vector2(-1, 2.9f), speed * Time.deltaTime);
            if(transform.position.y <= 2.96f)
            {
                onpos = false;
                StopCoroutine(Move());
            }
            yield return null;
        }
    }

    public void CircleFire()
    {
        int count = 45;

        float intervalAngle = 360 / count;

        for(int i = 0; i< count; ++i)
        {
            GameObject clone = Instantiate(mb1, transform.position, Quaternion.identity);
            float angle = intervalAngle * i;
            float x = Mathf.Cos(angle * Mathf.Deg2Rad);
            float y = Mathf.Sin(angle * Mathf.Deg2Rad);
            clone.GetComponent<Boss_Bullet>().Move(new Vector2(x, y));
        }
    }

    void SantanFire1()
    {
        int count = 40;

        float intervalAngle = 160 / count;
        for(int i = 0; i < count; i++)
        {
            GameObject clone1 = Instantiate(mb2, pos1.transform.position, Quaternion.identity);
            float angle = intervalAngle * i;
            float x = Mathf.Cos(angle * Mathf.Deg2Rad);
            float y = Mathf.Sin(angle * Mathf.Deg2Rad);
            clone1.GetComponent<Santan_Bullet>().Move(new Vector2(-x, -y));

        }
    }
    void SantanFire2()
    {
        int count = 40;

        float intervalAngle = 160 / count;
        for (int i = 0; i < count; i++)
        {
            GameObject clone2 = Instantiate(mb2, pos2.transform.position, Quaternion.identity);
            float angle = intervalAngle * i;
            float x = Mathf.Cos(angle * Mathf.Deg2Rad);
            float y = Mathf.Sin(angle * Mathf.Deg2Rad);
            clone2.GetComponent<Santan_Bullet>().Move(new Vector2(-x, -y));

        }
    }
}
