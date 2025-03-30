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
        Invoke("DownLaunch", 16.4f);
        Invoke("DownLaunch", 16.6f);
        Invoke("DownLaunch", 18.4f);
        Invoke("DownLaunch", 18.6f);
        Invoke("DownLaunch", 24.1f);
        Invoke("DownLaunch", 24.3f);
        Invoke("DownLaunch", 26f);
        Invoke("DownLaunch", 26.2f);
        Invoke("DownLaunch", 31.6f);
        Invoke("DownLaunch", 31.8f);
        Invoke("DownLaunch", 39.3f);
        Invoke("DownLaunch", 39.5f);
        Invoke("DownLaunch", 39.3f);
        Invoke("DownLaunch", 39.5f);
        Invoke("DownLaunch", 41.1f);
        Invoke("DownLaunch", 41.3f);
        Invoke("DownLaunch", 52.8f);
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
