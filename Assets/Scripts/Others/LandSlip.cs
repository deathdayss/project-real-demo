using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandSlip : MonoBehaviour
{
    public Plot myPlot;
    public Console player;
    public GameObject stones = new GameObject();

    // Update is called once per frame
    void Update()
    {
        Collider2D[] myArmy = Physics2D.OverlapCircleAll(new Vector2(118, 58), 16.6f);
        int countArmy = 0;
        foreach(Collider2D unit in myArmy)
        {
            if(unit.GetComponent<BasicUnits>() != null && unit.GetComponent<BasicUnits>().team == 1)
            {
                countArmy++;
            }
        }
        if(countArmy >= player.myUnits.Count - 2)
        {
            myPlot.getText();
            stones.SetActive(true);
            Destroy(gameObject);
        }
    }
}
