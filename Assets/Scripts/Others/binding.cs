using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class binding : MonoBehaviour
{
    public Plot myPlot;
    public List<GameObject> bindedUnits = new List<GameObject>();
    public List<GameObject> fakeUnits = new List<GameObject>();
    public List<GameObject> binds = new List<GameObject>();
    public List<GameObject> enemies = new List<GameObject>();
    public GameObject fakes;

    // Start is called before the first frame update
    void Start()
    {
        /*foreach(GameObject units in bindedUnits)
        {
            units.GetComponent<BasicUnits>().enabled = false;
            units.GetComponent<BasicUnits>().team = 2;
            units.GetComponent<SpriteRenderer>().enabled = false;
            units.GetComponent<Rigidbody2D>().freezeRotation = true;
            units.GetComponent<Rigidbody2D>().isKinematic = false;
            SpriteRenderer[] ms = units.GetComponentsInChildren<SpriteRenderer>();
            foreach(SpriteRenderer sr in ms)
            {
                sr.enabled = false;
            }
            units.AddComponent<Obstraction>();
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        bool allNull = true;
        foreach(GameObject k in enemies)
        {
            if(k != null)
            {
                allNull = false;
                break;
            }
        }
        if(allNull)
        {
            myPlot.getText();
            Destroy(fakes);
            for (int i = 0; i < binds.Count; i++)
            {
                //bindedUnits[i].GetComponent<BasicUnits>().team = 1;
                /*bindedUnits[i].GetComponent<BasicUnits>().enabled = true;
                bindedUnits[i].GetComponent<SpriteRenderer>().enabled = true;*/
                /*SpriteRenderer[] ms = bindedUnits[i].GetComponentsInChildren<SpriteRenderer>();
                Destroy(bindedUnits[i].GetComponent<Obstraction>());*/
                bindedUnits[i].SetActive(true);
                Destroy(binds[i]);
            }
            Destroy(gameObject);
        }   
    }
}
