using System.Collections;
using UnityEngine;

public class luvdisc : MonoBehaviour, IDamageable
{
    public float moveSpeed = 2f;
    public float HP = 100;
    public float exp = 0.2f;
    Animator ani;
    public SpriteRenderer sr;


    void Start()
    {
        ani = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

    }

    void Update()
    {
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);        
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    public void Damage(float attack)
    {
        StartCoroutine(Hit());
        HP -= attack;

        if(HP <= 0)
        {
            ani.SetBool("Destroy", true);
            StartCoroutine(monsterDie());
            GameManager.instance.ExpGain(exp);
            //Instantiate(PowerItem, transform.position, Quaternion.identity);
        }
    }
    IEnumerator monsterDie()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }
    
    IEnumerator Hit()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sr.color = Color.white;
    }
}

// using System.IO.Compression;
// using UnityEngine;
// using UnityEngine.Timeline;

// public class Monster : MonoBehaviour
// {
//     public float moveSpeed = 2f;
//     public float Delay = 1f;
//     public float HP = 100;
//     public Transform ms1;
//     public Transform ms2;
//     public GameObject bullet;
//     public GameObject PowerItem;

//     void Start()
//     {
//         Invoke("CreateBullet", Delay); // Delay 한 번 호출
//     }

//     void CreateBullet()
//     {
//         Instantiate(bullet, ms1.position, Quaternion.identity);
//         Instantiate(bullet, ms2.position, Quaternion.identity);

//         //재귀 호출 (스타트에서 한 번만 실행하니까~)
//         Invoke("CreateBullet", Delay);

//     


// }
