using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class Hoon_Monster : MonoBehaviour
{
    public float moveSpeed;
    public float HP;
    public float exp;
    public float droprate = 0;
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
            Vector3 hitPos = transform.position;
            GameObject go = Instantiate(hit, hitPos, Quaternion.identity);
            
            if(droprate > 0)
            {
                ItemManager.instance.ItemDrop(hitPos);
            }
            
            Destroy(go, 0.2f);
            Destroy(gameObject);
            GameManager.instance.ExpGain(exp);
            

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
