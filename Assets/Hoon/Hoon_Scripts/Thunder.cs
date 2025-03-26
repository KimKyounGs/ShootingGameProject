
using UnityEngine;

public class Thunder : MonoBehaviour
{
    void Update()
    {

    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
