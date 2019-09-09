using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheBoss : MonoBehaviour
{
    public Console player;
    public Plot myPlot;
    public GameObject boss;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D[] myArmy = Physics2D.OverlapCircleAll(transform.position, 4);
        int countArmy = 0;
        foreach (Collider2D unit in myArmy)
        {
            if (unit.GetComponent<BasicUnits>() != null && unit.GetComponent<BasicUnits>().team == 1)
            {
                countArmy++;
            }
        }
        if (countArmy >= player.myUnits.Count - 2)
        {
            myPlot.getText();
            boss.SetActive(true);
            Destroy(gameObject);
        }
    }
}
