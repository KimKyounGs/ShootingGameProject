using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class Hoon_Monster : MonoBehaviour
{
    public float moveSpeed;
    public float HP;
    public float exp;
    public Animator ani;
    public SpriteRenderer sr;
    public GameObject hit;
        
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
            GameObject go = Instantiate(hit, transform.position, Quaternion.identity);
            Destroy(go, 0.2f);
            Destroy(gameObject);
            GameManager.instance.ExpGain(exp);
            //Instantiate(PowerItem, transform.position, Quaternion.identity);
        }
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
