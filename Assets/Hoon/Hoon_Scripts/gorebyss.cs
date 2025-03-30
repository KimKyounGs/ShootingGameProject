using System;
using UnityEngine;

public class gorebyss : MonoBehaviour
{
    public float speed = 2.0f;        // Lerp 속도
    public float targetDistance = 3f;  // 플레이어와 유지할 거리
    public float followSpeed = 0.5f;   // 따라가는 속도

    private GameObject player;
    private Vector3 targetPosition;
    private SpriteRenderer playerSprite;
    private CapsuleCollider2D playerCollider;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            // 플레이어의 SpriteRenderer와 Collider2D 컴포넌트 가져오기
            playerSprite = player.GetComponent<SpriteRenderer>();
            playerCollider = player.GetComponent<CapsuleCollider2D>();

            Hoon_AudioManager.instance.SFXHeal();

            // 플레이어 스프라이트 색상을 핑크색으로 변경
            if (playerSprite != null)
            {
                playerSprite.color = new Color(1f, 0.5f, 0.8f, 1f); // 핑크색
            }
            else
            {
                player = GameObject.FindGameObjectWithTag("Player");
                playerSprite = player.GetComponent<SpriteRenderer>();
                playerSprite.color = new Color(1f, 0.5f, 0.8f, 1f); // 핑크색
            }

            if (playerCollider != null)
            {
                playerCollider.enabled = false;
            }
        }

        float playerHP = Hoon_Player.instance.HP;
        float playerMaxHP = Hoon_Player.instance.maxHP;

        if (playerMaxHP - playerHP >= 20)
        {
            Hoon_Player.instance.HP += 20;
            playerHP = Hoon_Player.instance.HP;
            Hoon_Player.instance.HPLeftUI.text = ($"{playerHP} / {playerMaxHP}");

        }
        else if(playerMaxHP - playerHP <= 0)
        {
            Hoon_Player.instance.HPLeftUI.text = ($"DEAD: {playerHP} / {playerMaxHP}");
        }
        else if(playerMaxHP - playerHP <= 20)
        {
            Hoon_Player.instance.HP = Hoon_Player.instance.maxHP;
            playerHP = Hoon_Player.instance.HP;
            Hoon_Player.instance.HPLeftUI.text = ($"{playerHP} / {playerMaxHP}");
        }
    }
    void OnDestroy()
    {
        // 오브젝트가 파괴될 때 플레이어 상태 복구
        if (player != null)
        {
            if (playerSprite != null)
            {
                playerSprite.color = Color.white; // 원래 색상으로 복구
            }
            
            if (playerCollider != null)
            {
                playerCollider.enabled = true; // 콜라이더 다시 활성화
            }
        }
    }
    void Update()
    {
        if (player != null)
        {
            // 플레이어 위치 기준으로 목표 위치 계산
            Vector3 directionToPlayer = (transform.position - player.transform.position).normalized;
            targetPosition = player.transform.position + (directionToPlayer * targetDistance);

            // 부드럽게 목표 위치로 이동
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
    }
}
