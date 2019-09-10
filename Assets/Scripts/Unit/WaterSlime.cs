using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSlime : BasicUnits
{
    public List<BasicUnits> poisoning = new List<BasicUnits>();
    public List<float> poisonTimer = new List<float>();
    public override void Attacking()
    {
        if (isClosed)
        {
            time += Time.deltaTime;
            if (isSeeking && areaEnemy != null && time - timeHelper >= atkSpd)
            {
                if (atkMode)
                {
                    areaEnemy.GetComponent<BasicUnits>().HP -= phAtk - areaEnemy.GetComponent<BasicUnits>().phDef;
                    if (poisoning.Contains(areaEnemy.GetComponent<BasicUnits>()))
                    {
                        poisonTimer[poisoning.IndexOf(areaEnemy.GetComponent<BasicUnits>())] = 4;
                    }
                    else
                    {
                        areaEnemy.GetComponent<BasicUnits>().phDef -= 1;
                        poisoning.Add(areaEnemy.GetComponent<BasicUnits>());
                        poisonTimer.Add(4);
                    }
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
                if (atkMode)
                {
                    tarEnemy.GetComponent<BasicUnits>().HP -= phAtk - tarEnemy.GetComponent<BasicUnits>().phDef;
                    if (poisoning.Contains(tarEnemy.GetComponent<BasicUnits>()))
                    {
                        poisonTimer[poisoning.IndexOf(tarEnemy.GetComponent<BasicUnits>())] = 4;
                    }
                    else
                    {
                        tarEnemy.GetComponent<BasicUnits>().phDef -= 1;
                        poisoning.Add(tarEnemy.GetComponent<BasicUnits>());
                        poisonTimer.Add(4);
                    }
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
    public override void killed()
    {
        if(HP <= 0)
        {
            if(poisoning.Count > 0)
            {
                foreach(BasicUnits pos in poisoning)
                {
                    pos.phDef += 1;
                }
            }
        }
        base.killed();
    }
    public override void Update()
    {
        base.Update();
        if (poisoning.Count > 0)
        {
            for(int i = 0; i < poisoning.Count; i++)
            {
                poisonTimer[i] -= Time.deltaTime;
                if (poisonTimer[i] < 0)
                {
                    poisonTimer.RemoveAt(i);
                    poisoning[i].phDef += 1;
                    poisoning.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}
