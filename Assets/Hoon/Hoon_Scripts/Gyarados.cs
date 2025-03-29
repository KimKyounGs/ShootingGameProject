using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;


public class Gyarados : Hoon_Player
{   
    public float skillCooldown = 10f;
    public bool CanSkill = true;
    public GameObject skill;
    public Image skillUI;
    public Image skillBackUI;

    public TMP_Text skillNameUI;
    public TMP_Text skillButtonUI;
    private bool isBreathSoundPlaying = false;

    void OnEnable()
    {
        // UI 요소들 활성화
        if (skillUI != null) skillUI.gameObject.SetActive(true);
        if (skillBackUI != null) skillBackUI.gameObject.SetActive(true);
        if (skillNameUI != null) skillNameUI.gameObject.SetActive(true);
        if (skillButtonUI != null) skillButtonUI.gameObject.SetActive(true);
    }

    void OnDisable()
    {
        // UI 요소들 비활성화
        if (skillUI != null) skillUI.gameObject.SetActive(false);
        if (skillBackUI != null) skillBackUI.gameObject.SetActive(false);
        if (skillNameUI != null) skillNameUI.gameObject.SetActive(false);
        if (skillButtonUI != null) skillButtonUI.gameObject.SetActive(false);
        if (isBreathSoundPlaying)
        {
            isBreathSoundPlaying = false;
            Hoon_AudioManager.instance.PlayLoopingDragonBreath(false);
        }
    }

    IEnumerator SkillCooldown()
    {
        skillUI.fillAmount = 0;
        skillNameUI.color = new Color(1, 1, 1, 0.5f);

        float elapsedTime = 0f;
        
        while (elapsedTime < skillCooldown)
        {
            elapsedTime += Time.deltaTime;
            float fillAmount = elapsedTime / skillCooldown;
            skillUI.fillAmount = fillAmount;
            yield return null;
        }
         
        skillUI.fillAmount = 1f;
        skillNameUI.color = new Color(1, 1, 1, 1f);
        
        Hoon_AudioManager.instance.CryGyarados();
        StartCoroutine(UIJumpAnimation(skillUI.gameObject));

        CanSkill = true;
    }

    public GameObject waterfall;
    protected override void Shoot()
    {
        base.Shoot();
        // 발사 시작할 때만 효과음 재생 시작
        if (!isBreathSoundPlaying)
        {
            isBreathSoundPlaying = true;
            Hoon_AudioManager.instance.PlayLoopingDragonBreath(true);
        }
    }

    void Skill()
    {
        Hoon_AudioManager.instance.SFXDragonRage1();

        if(Bullet[1] != null)
        {
            StartCoroutine(SkillCooldown());
            CanSkill = false;
            Instantiate(Bullet[1], pos.transform.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("스킬이 없습니다.");
        }
    }

    protected override void Dash()
    {
        base.Dash();
        Hoon_AudioManager.instance.SFXWaterfall();
        StartCoroutine(Waterfall());
    }

    IEnumerator Waterfall()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        
        if (moveX != 0 || moveY != 0)
        {
            lastMoveDirection = new Vector2(moveX, moveY).normalized;
        }

        DisableHitbox();
        
        float dashSpeed = speed * 2f;
        float dashTime = 0.2f;
        float startTime = Time.time;
        float lastEffectTime = startTime;

        while (Time.time - startTime < dashTime)
        {
            // 현재 위치에 이펙트 생성
            if (Time.time - lastEffectTime >= effectDashInterval)
            {
                GameObject effect = Instantiate(waterfall, transform.position + new Vector3(Random.Range(-0.25f, 0.25f), Random.Range(-0.25f, 0.25f), 0), Quaternion.identity);
                Destroy(effect, 0.5f); // 0.5초 후 이펙트 제거
                lastEffectTime = Time.time;
            }

            transform.Translate(lastMoveDirection * dashSpeed * Time.deltaTime);
            yield return null;
        }
        
        yield return new WaitForSeconds(0.3f);
        EnableHitbox();
    }

    protected override void Update()
    {
        base.Update();
        
        if (Input.GetKeyUp(KeyCode.Space) && isBreathSoundPlaying)
        {
            isBreathSoundPlaying = false;
            Hoon_AudioManager.instance.PlayLoopingDragonBreath(false);
        }

        if(Input.GetKeyDown(KeyCode.E) && CanSkill == true) 
        {
            CanSkill = false;
            StartCoroutine(SkillCooldown());
            Skill();
        }

    }

}

