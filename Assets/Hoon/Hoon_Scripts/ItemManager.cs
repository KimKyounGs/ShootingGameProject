using JetBrains.Annotations;
using System.Collections;
using UnityEngine;

public class ItemManager : MonoBehaviour
{

    public static ItemManager instance;

    [Header("아이템 설정")]
    public GameObject huntail;
    public GameObject gorebyss;
    public GameObject deepseaTooth;
    public GameObject deepseaScale;

    // public GameObject huntailCutScene;
    // public GameObject gorebyssCutScene;

    public Transform pos1;
    public bool CanHuntail = false;
    public bool CanGorebyss = false;
    public int deepseaToothBag = 0;
    public int deepseaScaleBag = 0;
    public bool isItemCooldown = false;
    public float itemCooldown = 8f;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else Destroy(gameObject);
    }

    public void ItemDrop(Vector3 dropPos)
    {
        int random = Random.Range(0, 100);
        if (random < 50)
        {
            Instantiate(deepseaTooth, dropPos, Quaternion.identity);
        }
        else if (random < 100)
        {
            Instantiate(deepseaScale, dropPos, Quaternion.identity);
        }
    }

    public void ObtainItem(string itemName)
    {
        if (itemName == "Deep Sea Tooth")
        {
            deepseaToothBag++;
            Debug.Log("Deep Sea Tooth 획득! 현재 개수: " + deepseaToothBag);
        }
        else if (itemName == "Deep Sea Scale")
        {
            deepseaScaleBag++;
            Debug.Log("Deep Sea Scale 획득! 현재 개수: " + deepseaScaleBag);
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (deepseaToothBag >= 1)
        {
            CanHuntail = true;
        }
        else
        {
            CanHuntail = false;
        }
        if (deepseaScaleBag >= 1)
        {
            CanGorebyss = true;
        }
        else
        {
            CanGorebyss = false;
        }

        if(Time.timeScale == 1)
        {
            if(Input.GetKeyDown(KeyCode.Alpha1) && CanHuntail == true && !isItemCooldown)
            {
                deepseaToothBag--;
                StartCoroutine(ItemCooldown());
                StartCoroutine(HuntailSkill());
            }
            if(Input.GetKeyDown(KeyCode.Alpha2) && CanGorebyss == true && !isItemCooldown) 
            {
                deepseaScaleBag--;
                StartCoroutine(ItemCooldown());
                StartCoroutine(GorebyssSkill());
            }
        }
    }

    IEnumerator ItemCooldown()
    {
        isItemCooldown = true;
        yield return new WaitForSeconds(itemCooldown);
        isItemCooldown = false;
    }

    IEnumerator HuntailSkill()
    {
        // GameObject go = Instantiate(huntailCutScene, pos1.transform.position, Quaternion.identity);
        // Destroy(go, 0.5f);
        Hoon_AudioManager.instance.CryHuntail();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Hoon_Player player1 = player.GetComponent<Hoon_Player>();
            if (player1 != null && player1.pos != null)
            {
                Instantiate(huntail, player1.pos.position, Quaternion.identity);
            }
        }
        yield return new WaitForSeconds(0.5f);
        
    }

    IEnumerator GorebyssSkill()
    {
        // GameObject go = Instantiate(gorebyssCutScene, pos1.transform.position, Quaternion.identity);
        // Destroy(go, 0.5f);
        Hoon_AudioManager.instance.CryGoreByss();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject go = Instantiate(gorebyss, player.transform.position + new Vector3(2f, 3f, 0), Quaternion.identity);
        Destroy(go, 10f);        

        yield return new WaitForSeconds(0.5f);
        
    }
}
