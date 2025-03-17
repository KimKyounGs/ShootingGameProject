using UnityEngine;

public class KK_IItem : MonoBehaviour
{
    public float itemVelocity = 20f;
    Rigidbody2D rigid;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        rigid.AddForce(new Vector3(itemVelocity, itemVelocity, 0f));
    }

    void Update()
    {
        
    }
    
}
