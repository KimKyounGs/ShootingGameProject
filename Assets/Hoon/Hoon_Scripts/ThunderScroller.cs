using UnityEngine;

public class ThunderScroller : MonoBehaviour
{
    public float scrollSpeed = 1.5f;
    public bool moveRight = false;
    void Start()
    {
        
    }

    void Update()
    {
        if (transform.position.x > -6.0 && !moveRight)
            Move(Vector3.left);
            if (transform.position.x <= -6.0)    
            moveRight = true;

        if (moveRight == true)
            Move(Vector3.right);
            if (transform.position.x >= 0)
                moveRight = false;
    }

    void Move(Vector3 dir)
    {
        transform.Translate(dir * scrollSpeed * Time.deltaTime);
    }

}
