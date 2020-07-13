using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 地斩 : GeneralSkills
{
    public float damage;
    public float radius;

    public new void Start()
    {
        //name = "地斩";
        name = "Attack the Ground";
        //description = "对周围" + radius.ToString() +"码内敌人造成" + damage.ToString() + "倍物理伤害。消耗1个技能点。";
        description = "对周围" + radius.ToString() + "码内敌人造成" + damage.ToString() + "倍物理伤害。消耗1个技能点。";
        description = "Deal " + damage.ToString() + " damage to enemies within " + radius.ToString() + " range. Consume 1 skill point.";
    }


    public override void launch()
    {
        if (owner.GetComponent<Hero>().skillPoint >= 4 && isLearned)
        {
            owner.GetComponent<Hero>().skillPoint -= 4;
            owner.GetComponent<BasicUnits>().state = 0;
            owner.GetComponent<BasicUnits>().setPosition = null;
            Collider2D[] effect = Physics2D.OverlapCircleAll(owner.transform.position, radius);
            foreach (Collider2D thing in effect)
            {
                if (thing.isTrigger && thing.GetComponent<BasicUnits>() != null)
                {
                    BasicUnits it = thing.GetComponent<BasicUnits>();
                    if (it.team != 1)
                    {
                        it.HP -= damage * owner.GetComponent<BasicUnits>().phAtk - it.phDef;
                        if (it.HP <= 0)
                        {
                            it.killer = owner;
                        }
                    }
                }

            }
            currentCD = maxCD;
        }
    }
}
