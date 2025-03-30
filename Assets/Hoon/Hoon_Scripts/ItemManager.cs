using JetBrains.Annotations;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemManager : MonoBehaviour
{

    public static ItemManager instance;

    [Header("아이템 설정")]
    public GameObject huntail;
    public GameObject gorebyss;
    public GameObject deepseaTooth;
    public GameObject deepseaScale;
    public Image ToothUI;
    public Image ScaleUI;
    public TMP_Text countToothUI;
    public TMP_Text countScaleUI;


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
        countToothUI.text = deepseaToothBag.ToString();
        countScaleUI.text = deepseaScaleBag.ToString();

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
        ToothUI.fillAmount = 0;
        ScaleUI.fillAmount = 0;
        
        float elapsedTime = 0f;
        
        while (elapsedTime < itemCooldown)
        {
            elapsedTime += Time.deltaTime;
            float fillAmount = elapsedTime / itemCooldown;
            
            ToothUI.fillAmount = fillAmount;
            ScaleUI.fillAmount = fillAmount;
            
            yield return null;
        }
        
        // 마지막에 완전히 채우기
        ToothUI.fillAmount = 1f;
        ScaleUI.fillAmount = 1f;

        Hoon_AudioManager.instance.SFXcooldownRecover();

        StartCoroutine(UIJumpAnimation(ToothUI.gameObject));
        StartCoroutine(UIJumpAnimation(ScaleUI.gameObject));

        isItemCooldown = false;
    }

        public IEnumerator UIJumpAnimation(GameObject uiElement)
    {
        Vector3 originalScale = uiElement.transform.localScale;
        float jumpDuration = 0.2f;
        float jumpHeight = 1.5f; // 최대 크기 배수
        
        // 커지는 애니메이션
        float elapsedTime = 0f;
        while (elapsedTime < jumpDuration / 2)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / (jumpDuration / 2);
            float currentScale = Mathf.Lerp(1f, jumpHeight, progress);
            uiElement.transform.localScale = originalScale * currentScale;
            yield return null;
        }
        
        // 돌아오는 애니메이션
        elapsedTime = 0f;
        while (elapsedTime < jumpDuration / 2)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / (jumpDuration / 2);
            float currentScale = Mathf.Lerp(jumpHeight, 1f, progress);
            uiElement.transform.localScale = originalScale * currentScale;
            yield return null;
        }
        
        // 원래 크기로 정확히 복귀
        uiElement.transform.localScale = originalScale;
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
        Destroy(go, 8f);        

        yield return new WaitForSeconds(0.5f);
        
    }
}
