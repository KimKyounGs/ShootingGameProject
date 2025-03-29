using System.Collections;
using UnityEngine;

public class carvanha : Hoon_Monster
{
    public GameObject bite;
    public GameObject attack;

    protected override void Start()
    {
        base.Start();
        moveSpeed = 2f;
        HP = 1;
        exp = 0.2f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Hoon_Player player = collision.gameObject.GetComponent<Hoon_Player>();
            if (player != null)
            {
                player.Damage(1);
            }
            moveSpeed = 0;
            Destroy(gameObject);
            Hoon_AudioManager.instance.SFXCrunch();
            GameObject Attack = Instantiate(attack, transform.position, Quaternion.identity);
            GameObject go = Instantiate(bite, transform.position, Quaternion.identity);
            Destroy(Attack, 0.3f);
            Destroy(go, 0.3f);
        }

    }

}
