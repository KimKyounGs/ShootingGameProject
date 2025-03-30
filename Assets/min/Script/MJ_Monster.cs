using UnityEngine;
using UnityEngine.UI;

public class MJ_Monster : MonoBehaviour
{
    public float Speed = 3;
    public float Delay = 1f;
    public int x = 1;
    public int y = 1;
    
    public Transform ms1;
    public Transform ms2;
    public GameObject bullet;
    

    void Start()
    {
        Invoke("CreateBullet", Delay);
    }
    void CreateBullet()
    {
        Instantiate(bullet, ms1.position, Quaternion.identity);
        if (ms2 != null)
        {
            Instantiate(bullet, ms2.position, Quaternion.identity);
        }

        Invoke("CreateBullet", Delay);
    }

    void Update()
    {
        Vector3 vector = new Vector3(x, y, 0);

        transform.Translate(vector * Speed * Time.deltaTime);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
