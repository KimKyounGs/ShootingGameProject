using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;

    Animator ani;

    public GameObject bullet;
    public Transform pos = null;

    //총알 준비 게이지
    public float rValue = 0f;
    public Image Gage;
    bool Ready = true;
    public bool startGame = false;
    

    void Start()
    {
        ani = GetComponent<Animator>();
    }


    void Update()
    {
        
        float moveX = moveSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
        float moveY = moveSpeed * Time.deltaTime * Input.GetAxis("Vertical");

        transform.Translate(moveX, moveY, 0);

        if (Input.GetAxis("Horizontal") <= -0.3f)
            ani.SetBool("left", true);
        else
            ani.SetBool("left", false);

        if (Input.GetAxis("Horizontal") >= 0.3f)
            ani.SetBool("right", true);
        else
            ani.SetBool("right", false);

        if (Input.GetAxis("Vertical") >= 0.3f)
            ani.SetBool("up", true);
        else
            ani.SetBool("up", false);

        if(Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = 7f;
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveSpeed = 5f;
        }

            rValue += Time.deltaTime;
        Gage.fillAmount = rValue;

        if(rValue >=1)
        {
            Ready = true;
        }

        if (Ready)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(bullet, pos.position, Quaternion.identity);
                rValue = 0f;
                Ready = false;
            }
        }

        StartCoroutine("MoveBeyond", 78f);

        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
        viewPos.x = Mathf.Clamp01(viewPos.x);
        viewPos.y = Mathf.Clamp01(viewPos.y);
        Vector3 worldPos = Camera.main.ViewportToWorldPoint(viewPos);
        transform.position = worldPos;
    }

    IEnumerator MoveBeyond(float delay)
    {
        yield return new WaitForSeconds(delay);
        float moveDuration = 2f;
        float elapsed = 0f;
        Vector3 startPos = transform.position;
        ani.SetBool("left", false);
        ani.SetBool("right", false);
        Vector3 TopPos = new Vector3(0, 10f, 0);
        Vector3 BottomPos = new Vector3(0, -3.89f, 0);

        while (elapsed < moveDuration)
        {
            transform.position = Vector3.Lerp(startPos, TopPos, elapsed / moveDuration);
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }
        SceneManager.LoadScene("MJ");
        Destroy(gameObject);
    }
}
