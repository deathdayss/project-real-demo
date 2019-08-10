using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 冲击剑 : AimSkills
{

    public new void Start()
    {
        name = "冲击剑";
        description = "冲击" + radius.ToString() + "码距离，对沿途敌人造成" + damage + "点物理伤害";
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
                owner.GetComponent<BasicUnits>().setPosition = null;
                isAiming = false;
                player.orignalMouse.GetComponent<SpriteRenderer>().enabled = true;
                player.targetMouse.GetComponent<SpriteRenderer>().enabled = false;
                player.isOrign = true;
                player.tarPoint.transform.position = tarPoint;
                if (player.tarPoint.GetComponent<ToMoveAnime>().isPlay)
                {
                    player.tarPoint.GetComponent<ToMoveAnime>().time = 0f;
                    player.tarPoint.GetComponent<Animator>().Play("ToMovePoint");
                }
                else
                {
                    player.tarPoint.GetComponent<ToMoveAnime>().isPlay = true;
                }
            }
        }
        if (owner.GetComponent<BasicUnits>().state == indicate)
        {
            
            owner.transform.position = Vector2.MoveTowards(owner.transform.position, tarPoint, owner.GetComponent<BasicUnits>().movSpd * Time.deltaTime);
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
                            it.HP -= damage - it.phDef;
                            if (it.HP <= 0)
                            {
                                it.killer = owner;
                            }
                        }
                    }
                }
                owner.GetComponent<BasicUnits>().state = 0;
                owner.transform.position = tarPoint;
                currentCD = maxCD;
            }
        }
    }
}
