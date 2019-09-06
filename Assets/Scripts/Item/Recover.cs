using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recover : GeneralItem
{
    public int category;
    public float var;
    // Start is called before the first frame update
    void Start()
    {
        consumable = true;
        canUse = true;

    }
    public override void PositiveEffect()
    {
        if (category == 0)
        {
            float tvar = owner.GetComponent<BasicUnits>().HP + var;
            if (tvar > owner.GetComponent<BasicUnits>().maxHp)
            {
                owner.GetComponent<BasicUnits>().HP = owner.GetComponent<BasicUnits>().maxHp;
            }
            else
            {
                owner.GetComponent<BasicUnits>().HP = tvar;
            }
        }
        else if (category == 1)
        {

            int tvar = Mathf.FloorToInt(var) + owner.GetComponent<Hero>().skillPoint;
            if (tvar >= owner.GetComponent<Hero>().maxSkillPoint)
            {
                owner.GetComponent<Hero>().skillPoint = owner.GetComponent<Hero>().maxSkillPoint;
            }
            else
            {
                owner.GetComponent<Hero>().skillPoint = tvar;
            }
        }
        base.PositiveEffect();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }
}
