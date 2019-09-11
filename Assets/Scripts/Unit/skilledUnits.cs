using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skilledUnits : BasicUnits
{
    public List<GeneralSkills> skill = new List<GeneralSkills>();
    public GeneralSkills skill1;
    public GeneralSkills skill2;
    public GeneralSkills skill3;
    public GeneralSkills skill4;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        if (skill1 != null)
        {
            skill.Add(skill1);
        }
        if (skill2 != null)
        {
            skill.Add(skill2);
        }
        if (skill3 != null)
        {
            skill.Add(skill3);
        }
        if (skill4 != null)
        {
            skill.Add(skill4);
        }
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (isFirst && player.isOrign)
        {
            
            if (skill1 != null && skill1.currentCD <= 0 &&  Input.GetKeyDown("q") && skill1.isLearned)
            {
                foreach(GameObject unit in player.chosen)
                {
                    if (unit.GetComponent<skilledUnits>() != null && unit.GetComponent<skilledUnits>().name == name && unit != gameObject)
                    {
                        unit.GetComponent<skilledUnits>().skill1.launch();
                    }
                }
                skill1.launch();
            }
            else if (skill2 != null && skill2.currentCD <= 0 && Input.GetKeyDown("w") && skill2.isLearned)
            {
                foreach (GameObject unit in player.chosen)
                {
                    if (unit.GetComponent<skilledUnits>() != null && unit.GetComponent<skilledUnits>().name == name && unit != gameObject)
                    {
                        unit.GetComponent<skilledUnits>().skill2.launch();
                    }
                }
                skill2.launch();
            }
            else if (skill3 != null && skill3.currentCD <= 0 && Input.GetKeyDown("e") && skill3.isLearned)
            {
                foreach (GameObject unit in player.chosen)
                {
                    if (unit.GetComponent<skilledUnits>() != null && unit.GetComponent<skilledUnits>().name == name && unit != gameObject)
                    {
                        unit.GetComponent<skilledUnits>().skill3.launch();
                    }
                }
                skill3.launch();
            }
            else if (skill4 != null && skill4.currentCD <= 0 && Input.GetKeyDown("r") && skill4 .isLearned)
            {
                foreach (GameObject unit in player.chosen)
                {
                    if (unit.GetComponent<skilledUnits>() != null && unit.GetComponent<skilledUnits>().name == name && unit != gameObject)
                    {
                        unit.GetComponent<skilledUnits>().skill4.launch();
                    }
                }
                skill4.launch();
            }
        }
    }
}
