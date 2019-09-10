using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BindingBoss : MonoBehaviour
{
    public float damage;
    public GameObject bindingFlower;
    public List<GameObject> bindingFlowers = new List<GameObject>();
    public List<BasicUnits> bindingUnits = new List<BasicUnits>();
    public List<Vector2> originalPlace = new List<Vector2>();
    public float startTime = 0;
    public float damageHelper = 0;
    public float lastTime = 3;
    bool isBinding = false;
    float time = 0;
    float timeHelper = 0;
    public float gap = 3;
    Vector2 var;
    public bool isActive = false;
    // Start is called before the first frame update
    void Start()
    {
        Vector2 boundSize = gameObject.GetComponent<SpriteRenderer>().bounds.size;
        var = new Vector2(boundSize.x/2, boundSize.y / 2);
    }

    // binding each unit of the area
    void bindThem()
    {
        Collider2D[] units = Physics2D.OverlapAreaAll((Vector2)transform.position - var, (Vector2)transform.position + var);
        foreach(Collider2D unit in units)
        {
            if(unit.GetComponent<BasicUnits>() != null && unit.GetComponent<BasicUnits>().team == 1)
            {
                isBinding = true;
                GameObject flower = Instantiate(bindingFlower);
                bindingFlowers.Add(flower);
                flower.transform.position = (Vector2)unit.transform.position - new Vector2(0, 0.01f);
                bindingUnits.Add(unit.GetComponent<BasicUnits>());
                originalPlace.Add(unit.transform.position);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isBinding)
        {
            startTime += Time.deltaTime;
            if (startTime - damageHelper >= 1)
            {
                damageHelper = startTime;
                foreach (BasicUnits unit in bindingUnits)
                {
                    if (unit != null)
                    {
                        unit.HP -= damage - unit.mgDef;
                    }
                }
            }
            for (int i = 0; i < bindingUnits.Count; i++)
            {
                if (bindingUnits[i] != null)
                {
                    bindingUnits[i].transform.position = originalPlace[i];
                }
                else
                {
                    Destroy(bindingFlowers[i]);
                }
            }
            if (startTime >= lastTime)
            {
                foreach (GameObject theFlower in bindingFlowers)
                {
                    Destroy(theFlower);
                }
                isBinding = false;
                startTime = 0;
                damageHelper = 0;
                bindingFlowers.Clear();
                bindingUnits.Clear();
                originalPlace.Clear();
            }
        }

        if(time - timeHelper >= gap)
        {
            time = 0;
            timeHelper = 0;
            bindThem();
            isActive = false;
        }

        // decide if the area could be seen
        if(isActive)
        {
            time += Time.deltaTime;
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
        } else
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
