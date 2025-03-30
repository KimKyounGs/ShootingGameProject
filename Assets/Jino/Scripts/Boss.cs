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
    public GameObject finale;

    void Start()
    {
        StartCoroutine("Move", 0);
        Invoke("CircleFire", 3f);
        Invoke("SantanFireL", 5.5f);
        Invoke("SantanFireR", 6.3f);
        Invoke("SantanFireL", 7.5f);
        Invoke("SantanFireR", 7.9f);
        Invoke("SantanFireL", 15.5f);
        Invoke("SantanFireR", 16f);
        Invoke("SantanFireL", 17f);
        Invoke("SantanFireR", 17.4f);
        Invoke("SantanFireL", 19f);
        Invoke("CircleFire", 23f);
        Invoke("SantanFireL", 25f);
        Invoke("SantanFireR", 25f);
        Invoke("CircleFire", 26.8f);
        Invoke("SantanFireR", 29.1f);
        Invoke("SantanFireL", 29.4f);
        Invoke("CircleFire", 30.6f);
        Invoke("CircleFire", 34.4f);
        Invoke("CircleFire", 38.2f);
        Invoke("CircleFire", 42f);
        Invoke("CircleFire", 45.8f);
        Invoke("SantanFireL", 47f);
        Invoke("SantanFireR", 47.5f);
        Invoke("SantanFireL", 48.7f);
        Invoke("SantanFireR", 49f);
        Invoke("CircleFire", 49.6f);
        Invoke("CircleFire", 53.4f);
        Invoke("CircleFire", 57.2f);
        Invoke("CircleFire", 61f);
        Invoke("CircleFire", 64.8f);
        Invoke("CircleFire", 68.6f);
        Invoke("CircleFire", 72.4f);
        Invoke("Finale", 73.5f);
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
            transform.position = Vector2.Lerp(transform.position, new Vector2(-1, 3.2f), speed * Time.deltaTime);
            if(transform.position.y <= 3.26f)
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

    void SantanFireL()
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
    void SantanFireR()
    {
        int count = 20;

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

    void Finale()
    {
        Instantiate(finale, transform.position, Quaternion.identity);
    }
}
