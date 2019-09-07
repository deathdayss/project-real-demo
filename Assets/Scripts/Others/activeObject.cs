using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activeObject : MonoBehaviour
{
    public List<GameObject> activeIt = new List<GameObject>();
    public Console player;
    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject target in activeIt)
        {
            target.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject units in player.myUnits)
        {
            if(Vector2.Distance(units.transform.position, transform.position) <= 8)
            {
                foreach (GameObject target in activeIt)
                {
                    target.SetActive(true);
                }
                Destroy(gameObject);
            }
        }
    }
}
