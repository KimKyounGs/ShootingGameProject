using System.Collections;
using UnityEngine;

public class carvanha : Hoon_Monster
{
    protected override void Start()
    {
        base.Start();
        moveSpeed = 1f;
        HP = 1;
        exp = 0.2f;
    }

}
