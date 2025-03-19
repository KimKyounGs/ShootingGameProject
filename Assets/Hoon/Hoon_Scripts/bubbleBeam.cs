using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class bubbleBeam : MonoBehaviour
{
    public float bulletSpeed = 0.5f;
    Animator ani;

    void Start()
    {
        ani = GetComponent<Animator>();
    }

    void Update()
    {
        transform.Translate(Vector2.up * bulletSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Hoon_Enemy"))
        {
            IDamageable enemy = collision.gameObject.GetComponent<IDamageable>();
            if (enemy != null)
            {
            enemy.Damage(1);
            }

            //SoundManager.instance.SoundDie();
            ani.SetBool("Destroy", true);
            StartCoroutine(BubblePop());
            //Destroy(collision.gameObject);
        }
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);       
    }

    IEnumerator BubblePop()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }
}


//     public float bulletSpeed = 0.5f;
//     public int damage = 10;
//     public GameObject Explosion;
//     public GameObject PowerItem;
//     public bool ExplodeFin = false;
//     

//         else if(collision.CompareTag("Boss"))
//         {
//             GameObject go = Instantiate(Explosion, transform.position, Quaternion.identity);
//             Destroy(go, 0.5f);
//             SoundManager.instance.SoundDie();
//         }
//     }




//     void Update()
//     {
//         transform.Translate(0, bulletSpeed * Time.deltaTime, 0);

        
//     }
// }
