using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.UIElements;

public class mantine : MonoBehaviour, IDamageable
{
    public float moveSpeed = 0.5f;
    public float HP = 5;
    public float exp = 0.5f;
    public float delay = 1.5f;
    Animator ani;

    public SpriteRenderer sr;

    public GameObject bullet;
    public Transform pos1;
    public Transform pos2;

    void Start()
    {
        ani = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        Invoke("CreateBullet", delay);

    }
    void CreateBullet()
    {
        Instantiate(bullet, pos1.position, Quaternion.identity);
        Instantiate(bullet, pos2.position, Quaternion.identity);
        Invoke("CreateBullet", delay);
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
