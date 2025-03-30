using System.Collections;
#if UNITY_EDITOR
using UnityEditor.Experimental.GraphView;
#endif
using UnityEngine;
using UnityEngine.UIElements;

public class Finale : MonoBehaviour
{
    public float speed = 3f;
    public GameObject finaleBulletPrefab;

    bool explosion = false;

    void Start()
    {

    }

    
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(0, 0, 0), speed * Time.deltaTime);

        if(transform.position.y <0.2f && !explosion)
        {
            explosion = true;
            StartCoroutine(TriggerFireworks());
        }
    }

    public void Fireworks()
    {
        if(explosion)
        {
            int bulletCount = 20;
            float angleStep = 360f / bulletCount;

            for (int i = 0; i < bulletCount; i++)
            {
                float angle = i * angleStep;
                float x = Mathf.Cos(angle * Mathf.Deg2Rad);
                float y = Mathf.Sin(angle * Mathf.Deg2Rad);
                Vector2 direction = new Vector2(x, y);

                GameObject bullet = Instantiate(finaleBulletPrefab, Vector2.zero, Quaternion.identity);
                bullet.GetComponent<Finale_Bullet>().SetDirection(direction);
            }
        }
    }
    IEnumerator TriggerFireworks()
    {
        for (int i = 0; i < 3; i++)
        {
            Fireworks();
            yield return new WaitForSeconds(0.2f);
        }
        Destroy(gameObject);
    }
}
