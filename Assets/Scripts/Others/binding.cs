﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class binding : MonoBehaviour
{
    public List<GameObject> bindedUnits = new List<GameObject>();
    public List<GameObject> binds = new List<GameObject>();
    public List<GameObject> enemies = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject units in bindedUnits)
        {
            units.GetComponent<BasicUnits>().enabled = false;
            units.GetComponent<BasicUnits>().team = 2;
            units.GetComponent<SpriteRenderer>().enabled = false;
            SpriteRenderer[] ms = units.GetComponentsInChildren<SpriteRenderer>();
            foreach(SpriteRenderer sr in ms)
            {
                sr.enabled = false;
            }
            units.AddComponent<Obstraction>();
        }
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
            for (int i = 0; i < binds.Count; i++)
            {
                bindedUnits[i].GetComponent<BasicUnits>().team = 1;
                bindedUnits[i].GetComponent<BasicUnits>().enabled = true;
                bindedUnits[i].GetComponent<SpriteRenderer>().enabled = true;
                SpriteRenderer[] ms = bindedUnits[i].GetComponentsInChildren<SpriteRenderer>();
                Destroy(bindedUnits[i].GetComponent<Obstraction>());
                Destroy(binds[i]);
            }
            Destroy(gameObject);
        }   
    }
}
