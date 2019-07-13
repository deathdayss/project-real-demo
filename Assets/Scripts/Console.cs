using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Console : MonoBehaviour
{
    public int gold;
    public int source;
    public GameObject lineLeft;
    public GameObject lineRight;
    public GameObject lineDown;
    public GameObject lineUp;
    public List<GameObject> myUnits;
    List<GameObject> chosen = new List<GameObject>();
    List<GameObject> team1 = new List<GameObject>();
    List<GameObject> team2 = new List<GameObject>();
    List<GameObject> team3 = new List<GameObject>();
    List<GameObject> team4 = new List<GameObject>();
    List<GameObject> team5 = new List<GameObject>();
    BasicUnits enemyChoice;
    Vector2 box1;
    Vector2 box1S;
    public bool isModeAtk = false;
    bool ishold;
    bool isOrign = true;

    private void Start()
    {
        lineLeft.GetComponent<SpriteRenderer>().enabled = false;
        lineRight.GetComponent<SpriteRenderer>().enabled = false;
        lineDown.GetComponent<SpriteRenderer>().enabled = false;
        lineUp.GetComponent<SpriteRenderer>().enabled = false;
    }

    void Choose()
    {
        if(isOrign)
        {
            if(Input.GetMouseButtonDown(0) && !ishold)
            {
                ishold = true;
                box1S = Input.mousePosition;
                box1 = Camera.main.ScreenToWorldPoint(box1S);
                
            }
            if (ishold)
            {
                lineLeft.GetComponent<SpriteRenderer>().enabled = true;
                lineRight.GetComponent<SpriteRenderer>().enabled = true;
                lineDown.GetComponent<SpriteRenderer>().enabled = true;
                lineUp.GetComponent<SpriteRenderer>().enabled = true;
                Vector2 boxedS = Input.mousePosition;
                Vector2 boxd = Camera.main.ScreenToWorldPoint(boxedS);
                float x1 = box1.x;
                float x2 = boxd.x;
                float y1 = box1.y;
                float y2 = boxd.y;
                Vector2 h1 = new Vector2(x1, (y1 + y2) / 2);
                Vector2 h2 = new Vector2(x2, (y1 + y2) / 2);
                Vector2 v1 = new Vector2((x1 + x2) / 2, y1);
                Vector2 v2 = new Vector2((x1 + x2) / 2, y2);
                lineLeft.transform.position = h1;
                lineRight.transform.position = h2;
                lineUp.transform.position = v1;
                lineDown.transform.position = v2;
                float scaleH = Mathf.Abs(box1S.x - boxedS.x)/2070f;
                float scaleV = Mathf.Abs(box1S.y - boxedS.y)/1170f;
                lineLeft.transform.localScale = new Vector2(1, scaleV);
                lineRight.transform.localScale = new Vector2(1, scaleV);
                lineUp.transform.localScale = new Vector2(scaleH, 1);
                lineDown.transform.localScale = new Vector2(scaleH, 1);
            }
            if (Input.GetMouseButtonUp(0))
            {
                lineLeft.GetComponent<SpriteRenderer>().enabled = false;
                lineRight.GetComponent<SpriteRenderer>().enabled = false;
                lineDown.GetComponent<SpriteRenderer>().enabled = false;
                lineUp.GetComponent<SpriteRenderer>().enabled = false;
                Vector2 box2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                ishold = false;
                Collider2D[] myunits = Physics2D.OverlapAreaAll(box1, box2);
                List<GameObject> boxChoice = new List<GameObject>();
                List<GameObject> enemyBox = new List<GameObject>();
                if (myunits != null)
                {
                    foreach (Collider2D unit in myunits)
                    {
                        BasicUnits mine1 = unit.GetComponent<BasicUnits>();
                        if (mine1 != null)
                        {
                            if (mine1.team == 1)
                            {
                                mine1.isChosen = true;
                                boxChoice.Add(unit.gameObject);
                            }
                            else
                            {
                                enemyChoice = mine1; 
                            }
                        }
                        if (boxChoice.Count > 0)
                        {
                            enemyChoice = null;
                            if (chosen.Count != 0)
                            {
                                foreach (GameObject a in chosen)
                                {
                                    if (!boxChoice.Contains(a))
                                    {
                                        a.GetComponent<BasicUnits>().isChosen = false;
                                    }
                                }
                            }
                            else
                            {
                                chosen = boxChoice;
                            }
                        }
                        else if (enemyChoice != null)
                        {
                            foreach (GameObject mine in chosen)
                            {
                                mine.GetComponent<BasicUnits>().isChosen = false;
                            }
                            chosen.Clear();
                        }
                    }
                }

            }
        }
    }
    void ModeAtk()
    {
        if (Input.GetKeyDown("a") && isOrign && chosen.Count != 0)
        {
            isModeAtk = true;
            isOrign = false;
        }
    }
    void ExeModeAtk()
    {
        if(isModeAtk)
        {
            if (Input.GetMouseButtonDown(1))
            {
                isModeAtk = false;
                isOrign = true;
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
    }
}
