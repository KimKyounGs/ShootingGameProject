using System.Collections;
using UnityEngine;
using UnityEngine.Playables;


public class SpawnManager : MonoBehaviour
{
    public GameObject luvdisc;
    public GameObject carvanha;
    public GameObject mantine;
    public GameObject sharpedo;
    public GameObject clampearl;
    public bool bossSpawn = false;

    public float gameDuration = 90;
    private float elapsedTime = 0f;

    void Start()
    {
        InvokeRepeating("SpawnLuvdisc", 5, 5f);
        InvokeRepeating("SpawnCarvanha", 1, 0.5f);
        InvokeRepeating("SpawnMantine", 3, 6f);
        InvokeRepeating("SpawnSharpedo", 20, 2f);
        Invoke("SpawnClampearl", 5);
        Invoke("SpawnClampearl", 10);
        Invoke("SpawnClampearl", 20);
    }

    void Update()
    {
        if (bossSpawn) return;
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= gameDuration)
        {
            CancelInvoke();
            bossSpawn = true;
            StartCoroutine(BossSpawn());
        }
    }

    public IEnumerator BossSpawn()
    {
        yield return new WaitForSeconds(8f);
        BossBGM.instance.StartBossTimeline();
    }

    void SpawnLuvdisc()
    {
        float randomY = Random.Range(1f, 4f);
        Instantiate(luvdisc, new Vector3(transform.position.x,randomY,0f), Quaternion.identity);

    }
    void SpawnCarvanha()
    {
        float randomX = Random.Range(-2f, 2f);
        Instantiate(carvanha, new Vector3(randomX, transform.position.y, 0f), Quaternion.identity);
    }
    void SpawnMantine()
    {
        float randomX = Random.Range(-2f, 2f);
        Instantiate(mantine, new Vector3(randomX, transform.position.y, 0f), Quaternion.identity);
    }

    void SpawnSharpedo()
    {
        float randomX = Random.Range(-2f, 2f);
        Instantiate(sharpedo, new Vector3(randomX, transform.position.y, 0f), Quaternion.identity);
    }

    void SpawnClampearl()
    {
        float randomX = Random.Range(-2f, 2f);
        Instantiate(clampearl, new Vector3(randomX, transform.position.y, 0f), Quaternion.identity);
    }
}