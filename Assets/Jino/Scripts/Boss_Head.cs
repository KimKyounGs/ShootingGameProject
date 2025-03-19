using UnityEngine;

public class Boss_Head : MonoBehaviour
{
    [SerializeField]
    GameObject Head_Bullet;

    private void Start()
    {
        Invoke("RightDownLaunch", 10f);
        Invoke("DownLaunch", 10f);
        Invoke("LeftDownLaunch", 10.5f);
        Invoke("DownLaunch", 10.5f);
    }

    public void RightDownLaunch()
    {
        GameObject go = Instantiate(Head_Bullet, transform.position, Quaternion.identity);

        go.GetComponent<Enemy_Bullet>().Move(new Vector2(1, -1));
    }

    public void LeftDownLaunch()
    {
        GameObject go = Instantiate(Head_Bullet, transform.position, Quaternion.identity);

        go.GetComponent<Enemy_Bullet>().Move(new Vector2(-1, -1));
    }

    public void DownLaunch()
    {
        GameObject go = Instantiate(Head_Bullet, transform.position, Quaternion.identity);

        go.GetComponent<Enemy_Bullet>().Move(new Vector2(0, -1));
    }

}
