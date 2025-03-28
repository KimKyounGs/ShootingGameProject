using UnityEngine;

public class PK_UPGRADE3_Ani : MonoBehaviour
{
    public GameObject Missile; // 미사일 오브젝트
    public Transform Mis_pos1 = null; // 미사일 발사 위치
    public Transform Mis_pos2 = null; // 미사일 발사 위치

    private int missileCount = 0; // 발사된 미사일 횟수
    public float fireDelay = 0.5f; // 미사일 발사 간격
    private float fireTimer = 0f; // 발사 타이머



    public Animator ani; // 애니메이터 컴포넌트


    void Start()
    {
        ani = GetComponent<Animator>(); // 애니메이터 컴포넌트 할당
    }

    void Update()
    {
        // 발사 타이머 업데이트
        fireTimer += Time.deltaTime;

        // 미사일 발사 조건: 발사 간격이 지나고, 발사 횟수가 3번 미만일 때
        if (fireTimer >= fireDelay && missileCount < 3)
        {
            FireMissiles();
            missileCount++;
            fireTimer = 0f; // 타이머 초기화
        }

        // 미사일을 3번 발사한 후 오브젝트 비활성화
        if (missileCount >= 3)
        {
            ani.SetBool("Fire", false); // Fire 파라미터를 false로 변경
            if (fireTimer >= 0.5f)
            {
                missileCount = 0;
                fireTimer = 0f;
                ani.SetBool("Fire", true);
                gameObject.SetActive(false);
            }
        }
    }

    private void FireMissiles()
    {
        // 미사일 발사
        if (Mis_pos1 != null)
        {
            Instantiate(Missile, Mis_pos1.position, Quaternion.identity);
        }
        if (Mis_pos2 != null)
        {
            Instantiate(Missile, Mis_pos2.position, Quaternion.identity);
        }
    }
}
