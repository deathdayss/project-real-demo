using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstraction : MonoBehaviour
{
    float sight = 2;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D[] round1 = Physics2D.OverlapCircleAll(transform.position, sight);
        List<Collider2D> round = new List<Collider2D>();
        if (round1.Length != 0)
        {
            foreach (Collider2D unit in round1)
            {
                if (unit.isTrigger)
                {
                    round.Add(unit);
                }
                else if (unit.GetComponent<BasicUnits>() == null)
                {
                    round.Add(unit);
                }
            }
        }
        if (round.Count != 0)
        {
            foreach (Collider2D sth in round)
            {
                float itsY = sth.transform.position.y;
                float myY = gameObject.transform.position.y;
                int myLayer = gameObject.GetComponent<Renderer>().sortingOrder;
                int itsLayer = sth.GetComponent<Renderer>().sortingOrder;
                if (myY > itsY && myLayer >= itsLayer)
                {
                    gameObject.GetComponent<Renderer>().sortingOrder = itsLayer - 1;
                }
                if (myY < itsY && myLayer <= itsLayer)
                {
                    gameObject.GetComponent<Renderer>().sortingOrder = itsLayer + 1;
                }

            }
        }
    }
}
