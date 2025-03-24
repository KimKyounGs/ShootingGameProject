
using JetBrains.Annotations;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;

public class Kyogre : MonoBehaviour
{
    public float HP = 1000000;
    //public Animator ani;
    //public SpriteRenderer sr;
    //public GameObject hit;
    public GameObject waterRing;
    //public GameObject pulse;
    public Transform pos1;
    Vector3 scale = new Vector3(0f,0f,0f);
    void Start()
    {
        InvokeRepeating("CastWaterRing", 3, 10);
    }
    void CastWaterRing()
    {
        Instantiate(waterRing, pos1.position, Quaternion.identity);
    }
    void Update()
    {   
    }
}
