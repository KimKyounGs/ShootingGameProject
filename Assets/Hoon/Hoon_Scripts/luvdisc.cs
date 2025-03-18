using System.Collections;
using UnityEngine;

public class luvdisc : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float HP = 100;
    Animator ani;

    void Start()
    {
        ani = GetComponent<Animator>();
    }

    void Update()
    {
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);        
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    public void Damage(int damage)
    {
        HP -= damage;

        if(HP <= 0)
        {
            ani.SetBool("Destroy", true);
            StartCoroutine(monsterDie());
            //Instantiate(PowerItem, transform.position, Quaternion.identity);
        }
    }
    IEnumerator monsterDie()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
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
