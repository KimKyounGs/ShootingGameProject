using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.UIElements;

public class mantine : Hoon_Monster
{

    public GameObject bullet;
    public Transform pos1;
    public Transform pos2;
    public float delay = 2.5f;

    protected override void Start()
    {
        base.Start();
        moveSpeed = 0.8f;
        HP = 5;
        exp = 2f;
        Invoke("CreateBullet", delay);
    }
    void CreateBullet()
    {
        Instantiate(bullet, pos1.position, Quaternion.identity);
        Instantiate(bullet, pos2.position, Quaternion.identity);
        Invoke("CreateBullet", delay);
    }

}
