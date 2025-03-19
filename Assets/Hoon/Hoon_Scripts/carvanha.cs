using System.Collections;
using UnityEngine;

public class carvanha : MonoBehaviour, IDamageable
{
    public float moveSpeed = 2f;
    public float HP = 1;
    public float exp = 0.1f;
    public Animator ani;
    public SpriteRenderer sr;


    void Start()
    {
        ani = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);        
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
