using UnityEngine;

public class SideLazerR : MonoBehaviour
{
    public float moveSpeed = 7f;
    public GameObject Lazer2;
    public Transform pos2 = null;
    bool isSidecoming = true;
    bool isLazerFired = false;

    Animator Eani;

    void Start()
    {
        Eani = GetComponent<Animator>();
        Invoke("Fire_Lazer", 1.8f);
    }

    private void Update()
    {
        if (isSidecoming)
        {
            SideComing();
        }

        if (isLazerFired)
        {
            MoveDown();
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void SideComing()
    {
        transform.position = Vector2.Lerp(transform.position, new Vector2(2.7f, transform.position.y), moveSpeed * Time.deltaTime);
    }

    void MoveDown()
    {
        Vector2 end = new Vector2(-2, -5);
        transform.Translate(end * 0.5f * Time.deltaTime);
    }

    void Fire_Lazer()
    {
        GameObject go = Instantiate(Lazer2, pos2.transform.position, Quaternion.identity);
        Destroy(go, 1.2f);
        isSidecoming = false;
        Invoke("StartMovingDown", 1.2f);
    }

    void StartMovingDown()
    {
        Eani.SetTrigger("go");
        isLazerFired = true;
    }
}
