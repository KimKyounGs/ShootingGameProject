using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class bubbleBeam : MonoBehaviour
{
    public float bulletSpeed = 0.5f;
<<<<<<< Updated upstream
    public GameObject bubblePop;
=======
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
            Hoon_Monster enemy = collision.GetComponent<Hoon_Monster>();
            if (enemy != null)
            {
            enemy.Damage(1);
            }

            Hoon_AudioManager.instance.SFXBubblePop();
            Destroy(gameObject);
            GameObject go = Instantiate(bubblePop, collision.transform.position, Quaternion.identity);
            Destroy(go, 0.2f);
=======
            collision.gameObject.GetComponent<luvdisc>().Damage(1);
            //SoundManager.instance.SoundDie();
            ani.SetBool("Destroy", true);
            StartCoroutine(BubblePop());
            //Destroy(collision.gameObject);
>>>>>>> Stashed changes
        }
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);       
    }
<<<<<<< Updated upstream
}
=======

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
>>>>>>> Stashed changes
