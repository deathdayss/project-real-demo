using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 地斩 : GeneralSkills
{
    public float damage;
    public float radius;



    public override void launch()
    {        
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
                    it.HP -= damage - it.phDef;
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
