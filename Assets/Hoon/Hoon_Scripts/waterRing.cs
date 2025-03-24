using JetBrains.Annotations;
using System.Collections;
using UnityEngine;

public class waterRing : MonoBehaviour
{
    public bool destroyWave = false;
    public float i = 0;
    void Start()
    {
        transform.localScale = new Vector3(0,0,0);
    }

    void Update()
    {
        transform.localScale = new Vector3(i,i,i);
        if(!destroyWave)
            StartCoroutine(EnlargeWave());
        if(i >= 4.5f)
            Destroy(gameObject);   
            
    }


    IEnumerator EnlargeWave()
    {
        i += 0.01f;
        yield return new WaitForSeconds(2);
        StartCoroutine(EnlargeWave());
             
    }    
            

}
