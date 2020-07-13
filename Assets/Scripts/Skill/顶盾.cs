using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 顶盾 : GeneralSkills
{
    public bool isActive = false;
    public float FactorDef;
    public float FactorSpd;

    public new void Start()
    {
        //name = "顶盾（未激活）";
        //description = "激活顶盾，物防增加30%，速度降低30%";
        name = "Activate Defense";
        description = "Increase Melee DEF by 30% but decrease SPD by 30%.";
    }

    public override void launch()
    {
        BasicUnits it = owner.GetComponent<BasicUnits>();
        if (isActive)
        {
            //description = "激活顶盾，物防增加30%，速度降低30%";
            description = "Increase Melee DEF by 30% but decrease SPD by 30%.";
            isActive = false;
            it.phDef /= FactorDef;
            it.movSpd /= FactorSpd;
            //name = "顶盾（未激活）";
            name = "Activate Defense";
        }
        else
        {
            //description = "取消顶盾";
            description = "Normalise the Melee DEF and SPD.";
            isActive = true;
            it.phDef *= FactorDef;
            it.movSpd *= FactorSpd;
            //name = "顶盾";
            name = "Inactivate Defense";
        }
    }

    public override void Update()
    {

    }
}
