using UnityEngine;

public class BackGround : MonoBehaviour
{
    public float ScrollSpeed = 0.01f;
    Material myMaterial;

    void Start()
    {
        myMaterial = GetComponent<Renderer>().material;    
    }

    
    void Update()
    {
        float newOffsetY = myMaterial.mainTextureOffset.y + ScrollSpeed * Time.deltaTime;

        Vector2 newOffset = new Vector2(0, newOffsetY);

        myMaterial.mainTextureOffset = newOffset;
    }
}
