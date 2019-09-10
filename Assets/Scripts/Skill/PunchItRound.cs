using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchItRound : MonoBehaviour
{
    public List<GameObject> targetPoint;
    public float damage = 5;
    public float distance = 14;
    public float speed = 2;
    public float minTime = 2;
    float x;
    float y;
    float time = 0;
    bool isLaunched = false;
    int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        rotateIt(targetPoint[0].transform.position, targetPoint[1].transform.position);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<BasicUnits>() != null && collision.GetComponent<BasicUnits>().team == 1)
        {
            collision.GetComponent<BasicUnits>().HP -= damage - collision.GetComponent<BasicUnits>().phDef;
            Destroy(gameObject);
        }
    }
    void rotateIt(Vector2 from, Vector2 to)
    {
        Vector2 dirction = to - from;
        float arcSin = 180 * Mathf.Asin(dirction.y/Mathf.Pow(Mathf.Pow(dirction.x, 2) + Mathf.Pow(dirction.y,2), 0.5f)) / Mathf.PI;
        float arCos = 180 * Mathf.Acos(dirction.x / Mathf.Pow(Mathf.Pow(dirction.x, 2) + Mathf.Pow(dirction.y, 2), 0.5f)) / Mathf.PI;
        if(dirction.y >= 0)
        {
            transform.eulerAngles = new Vector3(0,0, arCos + 90);
        } else if(dirction.x >= 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 90 - arCos);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, -arcSin - 90);
        }
    }
    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= minTime)
        {
            isLaunched = true;
        }
        if (isLaunched)
        {
            if(transform.position == targetPoint[index].transform.position)
            {
                index++;
                rotateIt(transform.position, targetPoint[index].transform.position);
            }
            transform.position = Vector2.MoveTowards(transform.position, targetPoint[index].transform.position, speed * Time.deltaTime);
            if (transform.position.Equals(targetPoint[targetPoint.Count - 1].transform.position))
            {
                Destroy(gameObject);
            }
        }
    }
}
