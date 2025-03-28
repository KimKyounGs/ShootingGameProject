using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Magikarp : Hoon_Player
{
    protected override void Shoot()
    {
        base.Shoot();
        Hoon_AudioManager.instance.SFXBubbleShoot();
    }
    
}
