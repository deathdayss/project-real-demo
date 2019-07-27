using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimSkills : GeneralSkills
{
    public Console player;
    public float damage;
    public float radius;
    public float radius2; 
    public bool isAiming;
    public int indicate;
    public Vector2 tarPoint;

    public override void launch()
    {
        isAiming = true;
        player.orignalMouse.GetComponent<SpriteRenderer>().enabled = false;
        player.targetMouse.GetComponent<SpriteRenderer>().enabled = true;
        player.isOrign = false;
    }

    public override void Update()
    {
        base.Update();
    }
}
