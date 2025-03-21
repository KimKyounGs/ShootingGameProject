using System.Collections;
using UnityEngine;

public class luvdisc : Hoon_Monster
{
    protected override void Start()
    {
        base.Start();
        moveSpeed = 2f;
        HP = 1;
        exp = 0.2f;
    }

    protected override void Move()
    {
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime); 
    }

}