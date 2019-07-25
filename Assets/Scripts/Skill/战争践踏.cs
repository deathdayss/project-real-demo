using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 战争践踏 : GeneralSkills
{
    public float damage;
    public float radius;
    public bool isMg;



    public override void launch()
    {        
        owner.GetComponent<BasicUnits>().state = 0;
        owner.GetComponent<BasicUnits>().setPosition = null;
        Collider2D[] effect = Physics2D.OverlapCircleAll(owner.transform.position, radius);
        foreach (Collider2D thing in effect)
        {
            if (thing.isTrigger)
            {
                BasicUnits it = thing.GetComponent<BasicUnits>();
                if (it.team != 1)
                {
                    it.HP -= damage;
                }
            }

        }
        currentCD = 0;
    }
}
