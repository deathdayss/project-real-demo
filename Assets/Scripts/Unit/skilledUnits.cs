using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skilledUnits : BasicUnits
{
    public GeneralSkills skill1;
    public GeneralSkills skill2;
    public GeneralSkills skill3;
    public GeneralSkills skill4;
    // Start is called before the first frame update

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (isFirst && player.isOrign)
        {
            
            if (skill1 != null && skill1.currentCD <= 0 &&  Input.GetKeyDown("q"))
            {
                skill1.launch();
            }
            else if (skill2 != null && skill2.currentCD <= 0 && Input.GetKeyDown("w"))
            {
                skill2.launch();
            }
            else if (skill3 != null && skill3.currentCD <= 0 && Input.GetKeyDown("e"))
            {
                skill3.launch();
            }
            else if (skill4 != null && skill4.currentCD <= 0 && Input.GetKeyDown("r"))
            {
                skill4.launch();
            }
        }
    }
}
