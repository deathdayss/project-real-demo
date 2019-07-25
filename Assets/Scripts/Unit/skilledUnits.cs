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
            if (skill1 != null && skill1.currentCD >= skill1.maxCD &&  Input.GetKeyDown("q"))
            {
                skill1.GetComponent<GeneralSkills>().launch();
            }
            if (skill2 != null && skill2.currentCD >= skill2.maxCD && Input.GetKeyDown("w"))
            {
                skill2.GetComponent<GeneralSkills>().launch();
            }
            if (skill3 != null && skill3.currentCD >= skill3.maxCD && Input.GetKeyDown("e"))
            {
                skill3.GetComponent<GeneralSkills>().launch();
            }
            if (skill4 != null && skill4.currentCD >= skill4.maxCD && Input.GetKeyDown("r"))
            {
                skill4.GetComponent<GeneralSkills>().launch();
            }
        }
    }
}
