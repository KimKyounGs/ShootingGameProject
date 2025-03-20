using UnityEngine;

public class SideLazerR : MonoBehaviour
{
    public float moveSpeed = 7f;
    public GameObject Lazer2;
    public Transform pos2 = null;
    Vector2 vec;


    void Start()
    {
        Invoke("Fire_Lazer", 1.8f);
    }

    private void Update()
    {
        transform.position = Vector2.Lerp(transform.position, new Vector2(2.7f, transform.position.y), moveSpeed * Time.deltaTime);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void MoveDown()
    {

    }

    void Fire_Lazer()
    {
        GameObject go = Instantiate(Lazer2, pos2.transform.position, Quaternion.identity);
        Destroy(go, 1.2f);
    }
}
