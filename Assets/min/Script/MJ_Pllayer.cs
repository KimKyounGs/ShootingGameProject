using Unity.VisualScripting;
using UnityEngine;

public class MJ_Pllayer : MonoBehaviour
{

    public float moveSpeed = 5f;

    Animator animator; //애니메이터 생성

    void Start()
    {
        animator = GetComponent<Animator>(); //GetComponet로 애니메이터 가져오기
    }

    
    void Update()
    {
        //방향키로 움직임
        float moveX = moveSpeed * Time.deltaTime * Input.GetAxis("Horizontal"); //설정한 속도 x 보정 시간 x 수평 움직임
        float moveY = moveSpeed * Time.deltaTime * Input.GetAxis("Vertical"); //설정한 속도 x 보정 시간 x 수직 움직임

        //왼쪽
        if (Input.GetAxis("Horizontal") <= -0.5f)
        {
            animator.SetBool("left", true);
        }
        else
            animator.SetBool("left", false);
        //오른쪽
        if (Input.GetAxis("Horizontal") >= 0.5f)
        {
            animator.SetBool("right", true);
        }
        else
            animator.SetBool("right", false);
        //위쪽
        if (Input.GetAxis("Vertical") >= 0.5f)
        {
            animator.SetBool("up", true);
        }
        else
            animator.SetBool("up", false);

        transform.Translate(moveX, moveY, 0); //움직임

    }
}
