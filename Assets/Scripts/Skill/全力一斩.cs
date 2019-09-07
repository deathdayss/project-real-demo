using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 全力一斩 : AimSkills
{
    public new void Start()
    {
        name = "全力一斩";
        description = "对目标敌人造成" + damage + "倍物理伤害。消耗3个技能点。";
    }
    public GameObject enemy;
    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (owner.GetComponent<BasicUnits>().state != indicate)
        {
            enemy = null;
        }
        if (isAiming)
        {
            if (owner.GetComponent<Hero>().skillPoint < 12)
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
                            enemy.GetComponent<BasicUnits>().circleAnime = true;
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
            if (owner.GetComponent<Hero>().skillPoint < 12)
            {
                owner.GetComponent<BasicUnits>().state = 0;
                enemy = null;
            }
            owner.transform.position = Vector2.MoveTowards(owner.transform.position, enemy.transform.position, owner.GetComponent<BasicUnits>().movSpd * Time.deltaTime);
            if (Vector2.Distance(owner.transform.position, enemy.transform.position) <= owner.GetComponent<BasicUnits>().radius + enemy.GetComponent<BasicUnits>().radius)
            {
                owner.GetComponent<Hero>().skillPoint -= 12;
                enemy.GetComponent<BasicUnits>().HP -= damage * owner.GetComponent<BasicUnits>().phAtk - enemy.GetComponent<BasicUnits>().phDef;
                if (enemy.GetComponent<BasicUnits>().HP <= 0)
                {
                    enemy.GetComponent<BasicUnits>().killer = owner;
                }
                owner.GetComponent<BasicUnits>().state = 0;
                currentCD = maxCD;
            }
        }
    }
}
