using UnityEngine;

public class KK_Background : MonoBehaviour
{
    public float scrollSpeed = 0.01f;
    Material myMaterial;

    void Start()
    {
        myMaterial = GetComponent<Renderer>().material;
    }

    
    void Update()
    {
        float newOffsetY = myMaterial.mainTextureOffset.y + scrollSpeed * Time.deltaTime;

        Vector2 newOffset = new Vector2(0, newOffsetY);

        myMaterial.mainTextureOffset = newOffset;


    }
}