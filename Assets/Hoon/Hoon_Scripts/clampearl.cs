using System.Collections;
using UnityEngine;

public class clampearl : Hoon_Monster
{
    protected override void Start()
    {
        base.Start();
        moveSpeed = 0.01f;
        HP = 10;
        exp = 0.5f;
        droprate = 100;
    }



}
