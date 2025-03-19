using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Jino_SpawnManager : MonoBehaviour
{
    public float ss = -2f;
    public float es = 2f;

    public GameObject missile1;

    void Start()
    {
        Invoke("MissileLine", 0.5f);
    }

    void MissileLine()
    {
        for (float i = ss; i <= es; i += 0.4f)
        {
            Vector2 r = new Vector2(i, transform.position.y);
            Instantiate(missile1, r, Quaternion.identity);
        }
        
    }

    IEnumerator FirstLine()
    {
        for (int i = 1; i <= 10; i++)
        {
            Debug.Log($"ss: {ss}, es: {es}");
            Vector2 r = new Vector2(i, transform.position.y);
            Instantiate(missile1, r, Quaternion.identity);
            yield return null;
        }
    }
}
