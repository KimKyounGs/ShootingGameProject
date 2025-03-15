using UnityEngine;

public class PK_Item1 : MonoBehaviour
{
    //아이템 가속 속도
    public float ItemVelocity = 20f;
    Rigidbody2D rig = null;


    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        rig.AddForce(new Vector3(ItemVelocity, ItemVelocity, 0f));
    }


    void Update()
    {

    }
}
