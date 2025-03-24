using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class sharpedo : Hoon_Monster
{
    public bool isSpawn;
    public GameObject bite;
    protected override void Start()
    {
        base.Start();
        moveSpeed = 0.5f;
        HP = 3;
        exp = 2f;
        isSpawn = true;
    }

    protected override void Move()
    {
        Charge();
    }

    void Charge()
    {
        if(isSpawn == true)
        {
            moveSpeed = 0.5f;
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
            StartCoroutine(Wait(2f));
        }
        else
        {
            moveSpeed = 6f;
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
        }

    }

    IEnumerator Wait(float sec)
    {
        yield return new WaitForSeconds(sec);
        isSpawn = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            moveSpeed = 0;
            GameObject go = Instantiate(bite, collision.transform.position, Quaternion.identity);
            Destroy(go, 0.3f);
            Hoon_AudioManager.instance.SFXCrunch();
            ani.SetBool("Attack", true);
            Destroy(gameObject, 0.4f);
        }

    }
}
    
 