using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;

    Animator ani;

    public GameObject[] bullet;
    public Transform pos = null;


    void Start()
    {
        ani = GetComponent<Animator>();    
    }

    
    void Update()
    {
        float moveX = moveSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
        float moveY = moveSpeed * Time.deltaTime * Input.GetAxis("Vertical");

        transform.Translate(moveX, moveY, 0);
    }
}
