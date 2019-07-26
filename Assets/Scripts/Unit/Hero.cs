using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : skilledUnits
{
    public GameObject item1;
    public GameObject item2;
    public GameObject item3;
    public GameObject item4;
    public GameObject item5;
    public int level;
    public int exp;
    public int maxExp;
    void levelUp()
    {
        if (exp >= maxExp)
        {
            level++;
            maxExp += 10;
        }
    }
    public override void Update()
    {
        base.Update();
        levelUp();
    }
}
