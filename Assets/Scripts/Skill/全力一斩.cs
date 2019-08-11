using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 全力一斩 : AimSkills
{

    public GameObject enemy;
    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (isAiming)
        {
            if (owner.GetComponent<Hero>().skillPoint < 4)
            {
                isAiming = false;
                player.orignalMouse.GetComponent<SpriteRenderer>().enabled = true;
                player.targetMouse.GetComponent<SpriteRenderer>().enabled = false;
                player.isOrign = true;
            }
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
                Collider2D[] tar = Physics2D.OverlapCircleAll(tarPoint, 0.1f);
                if(tar.Length > 0)
                {
                    if (tar[0].GetComponent<BasicUnits>() != null)
                    {
                        if (tar[0].GetComponent<BasicUnits>().team != owner.GetComponent<BasicUnits>().team)
                        {
                            owner.GetComponent<BasicUnits>().state = indicate;
                            owner.GetComponent<BasicUnits>().setPosition = null;
                            enemy = tar[0].gameObject;
                            isAiming = false;
                            player.orignalMouse.GetComponent<SpriteRenderer>().enabled = true;
                            player.targetMouse.GetComponent<SpriteRenderer>().enabled = false;
                            player.isOrign = true;
                        }
                    }
                }
            }
        }
        if (owner.GetComponent<BasicUnits>().state == indicate)
        {
            if (owner.GetComponent<Hero>().skillPoint < 4)
            {
                owner.GetComponent<BasicUnits>().state = 0;
            }
            owner.transform.position = Vector2.MoveTowards(owner.transform.position, tarPoint, owner.GetComponent<BasicUnits>().movSpd * Time.deltaTime);
            if (Vector2.Distance(owner.transform.position, tarPoint) <= radius)
            {
                owner.GetComponent<Hero>().skillPoint -= 4;
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
