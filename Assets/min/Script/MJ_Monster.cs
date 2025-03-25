using UnityEngine;

public class MJ_Monster : MonoBehaviour
{
    public int HP = 100;
    public float Speed = 3;
    //public float Delay = 1f;
    public int x = 1;
    public int y = 1;
    
    void Start()
    {
       
    }

    void Update()
    {
        Vector3 vector = new Vector3(x, y, 0);
        //아래 방향으로 움직여라
        transform.Translate(vector * Speed * Time.deltaTime);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    //미사일에 따른 데미지 입는 함수
    public void Damage(int attack)
    {
        HP -= attack;

        if (HP <= 0)
        {
            //ItemDrop();
            Destroy(gameObject);
        }
    }



}
