using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 顶盾 : GeneralSkills
{
    public bool isActive = false;
    public float FactorDef;
    public float FactorSpd;

    public override void launch()
    {
        BasicUnits it = owner.GetComponent<BasicUnits>();
        if (isActive)
        {
            isActive = false;
            it.phDef /= FactorDef;
            it.movSpd /= FactorSpd;
            name = "顶盾（未激活）";
        }
        else
        {
            isActive = true;
            it.phDef *= FactorDef;
            it.movSpd *= FactorSpd;
            name = "顶盾";
        }
    }

    public override void Update()
    {

    }
}
