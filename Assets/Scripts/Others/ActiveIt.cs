using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveIt : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<BasicUnits>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        bool t = false;
        Collider2D[] m = Physics2D.OverlapCircleAll(transform.position, 4);
        foreach (Collider2D g in m)
        {
            if (g.GetComponent<BasicUnits>() != null && g.GetComponent<BasicUnits>().name == "Altis (H)")
            {
                t = true;
            }
        }
        if (t)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            gameObject.GetComponent<BasicUnits>().enabled = true;
        }
    }
}
