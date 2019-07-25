using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 冲击剑 : GeneralSkills
{
    public Console player;
    public float damage;
    public float radius;
    public float radius2;
    public bool isdisMg;
    public bool isAiming;
    public bool isMoving;
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
        if (isAiming)
        {
            if (Input.GetKeyDown("1"))
            {
                isAiming = false;
                player.orignalMouse.GetComponent<SpriteRenderer>().enabled = true;
                player.targetMouse.GetComponent<SpriteRenderer>().enabled = false;
                player.isOrign = true;
            }
            if (Input.GetKeyDown("0"))
            {
                tarPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                owner.GetComponent<BasicUnits>().state = indicate;
                isAiming = false;
                player.orignalMouse.GetComponent<SpriteRenderer>().enabled = true;
                player.targetMouse.GetComponent<SpriteRenderer>().enabled = false;
                player.isOrign = true;
                isMoving = true;
            }
        }
        if (isMoving)
        {
            if (owner.GetComponent<BasicUnits>().state == indicate)
            {
                owner.transform.position = Vector2.MoveTowards(owner.transform.position, tarPoint, owner.GetComponent<BasicUnits>().movSpd * Time.deltaTime);
            }
            else
            {
                isMoving = false;
            }
            if (Vector2.Distance(owner.transform.position, tarPoint) <= radius)
            {
                Collider2D[] Object = Physics2D.OverlapAreaAll(new Vector2(owner.transform.position.x, owner.transform.position.y + radius2)) // The Area connot be rotated
                owner.transform.position = tarPoint;
                isMoving = false;
                owner.GetComponent<BasicUnits>().state = 0;

            }
        }
    }
}
