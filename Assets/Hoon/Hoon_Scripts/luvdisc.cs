using System.Collections;
using UnityEngine;

public class luvdisc : Hoon_Monster
{
    protected override void Start()
    {
        base.Start();
        moveSpeed = 2.5f;
        HP = 1;
        exp = 1.5f;
    }

    protected override void Move()
    {
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime); 
    }

}