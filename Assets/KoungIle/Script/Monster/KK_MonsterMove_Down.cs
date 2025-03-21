using UnityEngine;

public class KK_MonsterMove_Down : MonoBehaviour, IMonsterMove
{
    public float speed = 3f;

    public void Move()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime); 
    }
}
