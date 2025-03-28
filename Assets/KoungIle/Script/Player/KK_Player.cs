using System.Linq;
using UnityEngine;
using System.Collections.Generic;

public class KK_Player : MonoBehaviour
{   public float speed = 5f;

    public GameObject[] bullet;  //총알배열
    public Transform bulletPos = null;
    public int power = 1;

    [SerializeField] private float attackCoolTime;
    [SerializeField] private float attackMaxCoolTime = 0.5f;
    private Dictionary<int, int[]> powerPatterns;

    private Vector2 minBounds;
    private Vector2 maxBounds;
    Animator ani; //애니메이터를 가져올 변수

    //레이져
    public GameObject lazer;
    public float gValue = 0;

    void Start()
    {
        ani = GetComponent<Animator>();
        // 화면의 경계를 설정
        Camera cam = Camera.main;
        Vector3 bottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, 0));

        minBounds = new Vector2(bottomLeft.x, bottomLeft.y);
        maxBounds = new Vector2(topRight.x, topRight.y);
        

        // 각 파워 레벨에 대한 총알 인덱스 패턴 설정
        powerPatterns = new Dictionary<int, int[]>
        {
            { 1, new int[] { 0 } },
            { 2, new int[] { 0, 0 } },
            { 3, new int[] { 0, 1, 0 } },
            { 4, new int[] { 1, 1, 1 } },
            { 5, new int[] { 1, 2, 1 } },
            { 6, new int[] { 2, 2, 2 } },
            { 7, new int[] { 2, 3, 2} },
            { 8, new int[] { 3, 3, 3} },
            { 9, new int[] { 3, 4, 3} },
            { 10, new int[] { 4, 4, 4} }
        };
    }

    void Update()
    {
        Move();
        Shoot();

    }

    void Move()
    {
        // 플레이어 이동
        float moveX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float moveY = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        // -1   0   1
        if (Input.GetAxis("Horizontal") <= -0.05f)
            ani.SetBool("left", true);
        else
            ani.SetBool("left", false);


        if (Input.GetAxis("Horizontal") >= 0.05f)
            ani.SetBool("right", true);
        else
            ani.SetBool("right", false);

        Vector3 newPosition = transform.position + new Vector3(moveX, moveY, 0);

        // 경계를 벗어나지 않도록 위치 제한
        newPosition.x = Mathf.Clamp(newPosition.x, minBounds.x, maxBounds.x);
        newPosition.y = Mathf.Clamp(newPosition.y, minBounds.y, maxBounds.y);

        transform.position = newPosition;
    }

    void Shoot()
    {
        if (power <= 0 || !powerPatterns.ContainsKey(power)) return;

        attackCoolTime += Time.deltaTime;

        if (Input.GetKey(KeyCode.Space) && attackCoolTime >= attackMaxCoolTime)
        {
            int[] pattern = powerPatterns[power];

            for (int i = 0; i < pattern.Length; i++)
            {
                float offset = (i - (pattern.Length - 1) / 2f) * 0.25f;
                Vector3 spawnPos = bulletPos.position + new Vector3(offset, 0f, 0f);

                int bulletIndex = Mathf.Clamp(pattern[i], 0, bullet.Length - 1); // 에러 방지 코드
                GameObject prefab = bullet[bulletIndex];
                Vector3 offsetFromPrefab = prefab.GetComponent<KK_PBulletData>()?.spawnOffset ?? Vector3.zero;
                spawnPos += offsetFromPrefab;
                Instantiate(prefab, spawnPos, Quaternion.identity);
            }

            attackCoolTime = 0f;
        }
    }


    public void IncreasePower()
    {
        power = Mathf.Min(power + 1, 10);
        // 파워업 이팩트!
    }
}
