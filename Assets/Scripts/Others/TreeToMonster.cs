using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeToMonster : MonoBehaviour
{
    public Console player;
    public Plot myPlot;
    public GameObject trees;
    public GameObject monsters;
    public bool canTrigger = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(canTrigger)
        {
            foreach (GameObject unit in player.myUnits)
            {
                if (Vector2.Distance(gameObject.transform.position, unit.transform.position) <= 8)
                {
                    myPlot.getText();
                    monsters.SetActive(true);
                    Destroy(trees);
                    Destroy(gameObject);
                }
            }
        }
    }
}
