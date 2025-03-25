using System.Collections;
using UnityEngine;
using UnityEngine.Playables;


public class SpawnManager : MonoBehaviour
{
    public GameObject luvdisc;
    public GameObject carvanha;
    public GameObject mantine;
    public GameObject sharpedo;
    public bool bossSpawn = false;

    public float gameDuration = 90;
    private float elapsedTime = 0f;

    void Start()
    {
        InvokeRepeating("SpawnLuvdisc", 10, 15f);
        InvokeRepeating("SpawnCarvanha", 1, 1f);
        InvokeRepeating("SpawnMantine", 10, 6f);
        InvokeRepeating("SpawnSharpedo", 25, 10f);
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
        yield return new WaitForSeconds(6.5f);
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
}

//     public float ss = -2;
//     public float es = 2;
//     public float StartTime = 1; // 처음 생성 시간
//     public float SpawnStop = 10; // 중지 시간
//     public GameObject Monster;
//     public GameObject Monster2;
//     public GameObject Boss;

//     bool swi = true;
//     bool swi2 = true;

//     [SerializeField]
//     GameObject textBossWarning;

//     private void Awake()
//     {
//         textBossWarning.SetActive(false);

//         // PoolManager.Instance.CreatePool(Monster, 10);
//     }
//     void Start()
//     {
//         // InvokeRepeating("Spawn", 3, 1);
//         StartCoroutine("RandomSpawn");
//         Invoke("Stop", SpawnStop);
//     }

//     IEnumerator RandomSpawn()
//     {
//         while(swi)
//         {
//             yield return new WaitForSeconds(StartTime);
//             float x = Random.Range(ss, es);
//             Vector2 r = new Vector2(x, transform.position.y);
//             Instantiate(Monster, r, Quaternion.identity);
//             // GameObject enemy = PoolManager.Instance.Get(Monster);
//             // enemy.transform.position = r;
//         }
//     }

//     IEnumerator RandomSpawn2()
//     {
//         while(swi2)
//         {
//             yield return new WaitForSeconds(StartTime+2);
//             float x = Random.Range(ss, es);
//             Vector2 r = new Vector2(x, transform.position.y);
//             Instantiate(Monster2, r, Quaternion.identity);
//         }
//     }

//     void Stop()
//     {
//         swi = false;
//         // 두번째 몬스터 코루틴 추가하면 된다~
//         StopCoroutine("RandomSpawn");
//         StartCoroutine("RandomSpawn2");
//         Invoke("Stop2", SpawnStop +20);
//     }

//     void Stop2()
//     {
//         swi2 = false;
//         StopCoroutine("RandomSpawn2");
//         textBossWarning.SetActive(true);

//         Vector3 pos = new Vector3(0, 3, 0);
//         Instantiate(Boss, pos, Quaternion.identity);
//         // StartCoroutine("RandomBossSpawn");
//         // Invoke("Stop2", SpawnStop +20);
//     }

    

//     void Update()
//     {
        
//     }
// }
