using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skillPointUnits : skilledUnits
{
    public int skillPoint;
    public int maxSkillPoint = 0;
    public List<GameObject> skillCircle = new List<GameObject>();
    // Start is called before the first frame update
    public void SkillCircle()
    {
        if (maxSkillPoint > 0)
        {
            for (int i = 0; i < skillCircle.Count; i++)
            {
                float m = (float)skillPoint - 4 * i;
                if (m <= 4)
                {
                    skillCircle[i].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, m / 4);
                }
            }
        }
    }
    public override void Attacking()
    {
        if (isClosed)
        {
            time += Time.deltaTime;
            if (isSeeking && areaEnemy != null && time - timeHelper >= atkSpd)
            {
                if (skillPoint < maxSkillPoint)
                    skillPoint++;
                if (atkMode)
                {
                    areaEnemy.GetComponent<BasicUnits>().HP -= phAtk - areaEnemy.GetComponent<BasicUnits>().phDef;
                }
                else
                {
                    areaEnemy.GetComponent<BasicUnits>().HP -= mgAtk - areaEnemy.GetComponent<BasicUnits>().phDef;
                }
                if (areaEnemy.GetComponent<BasicUnits>().HP <= 0)
                {
                    areaEnemy.GetComponent<BasicUnits>().killer = gameObject;
                }
                time = timeHelper;
            }
            if (state == 2 && tarEnemy != null && time - timeHelper >= atkSpd)
            {
                if (skillPoint < maxSkillPoint && tarEnemy.GetComponent<BasicUnits>().team != team)
                    skillPoint++;
                if (atkMode)
                {
                    tarEnemy.GetComponent<BasicUnits>().HP -= phAtk - tarEnemy.GetComponent<BasicUnits>().phDef;
                }
                else
                {
                    tarEnemy.GetComponent<BasicUnits>().HP -= mgAtk - tarEnemy.GetComponent<BasicUnits>().phDef;
                }
                if (tarEnemy.GetComponent<BasicUnits>().HP <= 0)
                {
                    tarEnemy.GetComponent<BasicUnits>().killer = gameObject;
                }
                time = timeHelper;
            }
        }
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        SkillCircle();
    }
}
