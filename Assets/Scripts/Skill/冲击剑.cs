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
            if (Input.GetMouseButtonDown(1))
            {
                isAiming = false;
                player.orignalMouse.GetComponent<SpriteRenderer>().enabled = true;
                player.targetMouse.GetComponent<SpriteRenderer>().enabled = false;
                player.isOrign = true;
            }
            if (Input.GetMouseButtonDown(0))
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
                RaycastHit2D[] Object = Physics2D.CircleCastAll(owner.transform.position, radius2, tarPoint - (Vector2)owner.transform.position, Vector2.Distance(owner.transform.position, tarPoint));
                foreach (RaycastHit2D unit in Object)
                {
                    if (unit.collider.isTrigger && unit.collider.GetComponent<BasicUnits>() != null)
                    {
                        BasicUnits it = unit.collider.GetComponent<BasicUnits>();
                        if (it.team != owner.GetComponent<BasicUnits>().team)
                        {
                            it.HP -= damage;
                            if (it.HP <= 0)
                            {
                                it.killer = owner;
                            }
                        }
                    }
                }
                isMoving = false;
                owner.GetComponent<BasicUnits>().state = 0;
                owner.transform.position = tarPoint;
                currentCD = maxCD;
            }
        }
    }
}
