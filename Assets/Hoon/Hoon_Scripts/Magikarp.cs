using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Magikarp : Hoon_Player
{
    protected override void Shoot()
    {
        base.Shoot();
        Hoon_AudioManager.instance.SFXBubbleShoot();
    }
    
    protected override void Dash()
    {
        base.Dash();
        Hoon_AudioManager.instance.SFXSplash();
        StartCoroutine(Splash());
    }
    IEnumerator Splash()
    {
        if (!isInvincible)  // 무적이 아닐 때만 히트박스 조작
        {
            DisableHitbox();
        }
        
        int jumpCount = 3;  // 점프 횟수
        float jumpHeight = 0.5f;  // 점프 높이
        float jumpDuration = 0.5f;  // 한번 점프하는데 걸리는 시간
        Vector3 originalPos = transform.position;  // 초기 위치 저장

        for (int i = 0; i < jumpCount; i++)
        {
            float elapsedTime = 0f;
            Vector3 startPos = transform.position;
            
            // 점프 올라가는 부분
            while (elapsedTime < jumpDuration/2)
            {
                elapsedTime += Time.deltaTime;
                float progress = elapsedTime / (jumpDuration/2);
                
                // 포물선 움직임 계산
                float height = Mathf.Sin(progress * Mathf.PI/2) * jumpHeight;
                transform.position = startPos + new Vector3(0, height, 0);
                
                yield return null;
            }
            
            // 점프 내려오는 부분
            while (elapsedTime < jumpDuration)
            {
                elapsedTime += Time.deltaTime;
                float progress = elapsedTime / jumpDuration;
                
                float height = Mathf.Sin((1-progress) * Mathf.PI/2) * jumpHeight;
                transform.position = startPos + new Vector3(0, height, 0);
                
                yield return null;
            }
            
            // 각 점프 사이의 짧은 대기 시간
            yield return new WaitForSeconds(0.01f);
        }
        
        // 원래 y좌표로 부드럽게 돌아가기
        transform.position = new Vector3(transform.position.x, originalPos.y, transform.position.z);
        yield return new WaitForSeconds(0.3f);

        if (!isInvincible)
        {
            EnableHitbox();
        }
    }

}


