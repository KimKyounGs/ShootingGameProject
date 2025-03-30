using System.Collections;
#if UNITY_EDITOR
using UnityEditor.Timeline.Actions;
#endif
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
    bool Fallback = true;

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
        Invoke("SantanFireL", 17.1f);
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
        Invoke("SantanFireL", 56.2f);
        Invoke("SantanFireR", 56.5f);
        Invoke("CircleFire", 57.2f);
        Invoke("SantanFireL", 60.3f);
        Invoke("SantanFireR", 60.3f);
        Invoke("CircleFire", 61f);
        Invoke("SantanFireL", 63.8f);
        Invoke("SantanFireR", 64.1f);
        Invoke("CircleFire", 64.8f);
        Invoke("CircleFire", 68.6f);
        Invoke("CircleFire", 72.4f);
        Invoke("Finale", 73.5f);
    }

    
    void Update()
    {
        if(Fallback)
        {
            if (transform.position.x > 1)
            {
                flag *= -1;
            }
            if (transform.position.x < -1)
            {
                flag *= -1;
            }

            transform.Translate(flag * speed * Time.deltaTime, 0, 0);
        }
        else if(!Fallback)
        {
            transform.Translate(new Vector3(0, 10f, 0) * 0.2f * Time.deltaTime);
            if (transform.position.y > 8f)
            {
                Destroy(gameObject);
            }
        }

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
        int count = 20;

        float intervalAngle = 180 / count;
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

        float intervalAngle = 180 / count;
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
        Fallback = false;
    }
}
