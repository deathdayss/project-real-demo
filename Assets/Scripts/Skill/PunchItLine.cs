using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchItLine : MonoBehaviour
{
    public float damage = 5;
    public float distance = 14;
    public float speed = 2;
    public float minTime = 2;
    float x;
    float y;
    Vector2 target;
    float time = 0;
    bool isLaunched = false;
    // Start is called before the first frame update
    void Start()
    {
        float rotate = transform.eulerAngles.z;
        float y = transform.position.y - Mathf.Cos(rotate * Mathf.PI/180) * distance;
        float x = transform.position.x + Mathf.Sin(rotate * Mathf.PI / 180) * distance;
        target = new Vector2(x, y);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if( collision.GetComponent<BasicUnits>() != null && collision.GetComponent<BasicUnits>().team == 1)
        {
            collision.GetComponent<BasicUnits>().HP -= damage - collision.GetComponent<BasicUnits>().phDef;
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time >= minTime)
        {
            isLaunched = true;
        }
        if(isLaunched)
        {
            
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
            if(transform.position.Equals(target))
            {
                Destroy(gameObject);
            }
        }
    }
}
