using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Gyarados : Hoon_Player
{   
    public GameObject waterfall;
    protected override void Shoot()
    {
        base.Shoot();
        Hoon_AudioManager.instance.SFXDragonBreath();
    }

    protected override void Skill()
    {
        base.Skill();
        Hoon_AudioManager.instance.SFXDragonRage1();
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
                GameObject effect = Instantiate(waterfall, transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0), Quaternion.identity);
                Destroy(effect, 0.5f); // 0.5초 후 이펙트 제거
                lastEffectTime = Time.time;
            }

            transform.Translate(lastMoveDirection * dashSpeed * Time.deltaTime);
            yield return null;
        }
        
        EnableHitbox();
        yield return new WaitForSeconds(0.3f);
    }

    void Update()
    {
           

    }

}

