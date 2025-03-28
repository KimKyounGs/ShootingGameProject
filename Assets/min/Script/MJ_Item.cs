using UnityEngine;

public class MJ_Item : MonoBehaviour
{

    public float ItemVelocity = 20f;
    Rigidbody2D rig = null;

    void Start()
    {
        //리지드 바디 불러와서 속도 조절
        rig = GetComponent<Rigidbody2D>();
        rig.AddForce(new Vector3(ItemVelocity, ItemVelocity, 0f));
    }

}
