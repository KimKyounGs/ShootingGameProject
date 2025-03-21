using System.Collections;
using UnityEngine;

public class Hoon_Monster : MonoBehaviour
{
    public float moveSpeed;
    public float HP;
    public float exp;
    public Animator ani;
    public SpriteRenderer sr;

    protected virtual void Start()
    {
        ani = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }
    protected virtual void Update()
    {
        Move();
    }

    protected virtual void Move()
    {
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime); 
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
    protected virtual IEnumerator monsterDie()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }
    
    protected virtual IEnumerator Hit()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sr.color = Color.white;
    }

    protected virtual void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
