using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotTrigger : MonoBehaviour
{
    public Console player;
    public Plot myPlot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject unit in player.myUnits)
        {
            if(Vector2.Distance(gameObject.transform.position, unit.transform.position) <= 8)
            {
                myPlot.getText();
                Destroy(gameObject);
                break;
            }
        }
    }
}
