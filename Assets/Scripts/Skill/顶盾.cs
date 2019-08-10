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
        name = "顶盾（未激活）";
        description = "激活顶盾，物防增加30%，速度降低30%";
    }

    public override void launch()
    {
        BasicUnits it = owner.GetComponent<BasicUnits>();
        if (isActive)
        {
            description = "激活顶盾，物防增加30%，速度降低30%";
            isActive = false;
            it.phDef /= FactorDef;
            it.movSpd /= FactorSpd;
            name = "顶盾（未激活）";
        }
        else
        {
            description = "取消顶盾";
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
