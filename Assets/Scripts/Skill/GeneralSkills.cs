using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralSkills : MonoBehaviour
{
    public GameObject owner;
    public int skillPoint;
    public float maxCD;
    public float currentCD;
    public string name;
    public bool isLearned;
    
    // Start is called before the first frame update
    public void Start()
    {
        
    }
    public virtual void AddCD()
    {
        if (currentCD > 0)
        {
            currentCD -= Time.deltaTime;
        }
    }
    public virtual void launch()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        AddCD();
    }
}
