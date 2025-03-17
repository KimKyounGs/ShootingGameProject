
using JetBrains.Annotations;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;

public class Kyogre : MonoBehaviour
{
    public GameObject waterRing;
    public GameObject pulse;
    public float ringScale = 0f;
    Vector3 scale = new Vector3(0f,0f,0f);
    void Start()
    {
        waterRing.GetComponent<Transform>().localScale = scale;
        pulse = Instantiate(waterRing, transform.position, Quaternion.identity);
    }

    void Update()
    {
        while(true)
        {
            float a = 0f;
            a = a + 0.1f;
            pulse.GetComponent<Transform>().localScale = new Vector3(a,a,a);
        }
        
    }
}
