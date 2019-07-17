using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Console : MonoBehaviour
{
    public int gold;
    public int source;
    public bool isModeAtk = false;
    public bool isOrign = true;
    public bool ishold;
    public Transform cam;
    public GameObject lineLeft;
    public GameObject lineRight;
    public GameObject lineDown;
    public GameObject lineUp;
    public GameObject orignalMouse;
    public GameObject targetMouse;
    public List<GameObject> myUnits = new List<GameObject>();
    public List<GameObject> chosen = new List<GameObject>();
    List<GameObject> team1 = new List<GameObject>();
    List<GameObject> team2 = new List<GameObject>();
    List<GameObject> team3 = new List<GameObject>();
    List<GameObject> team4 = new List<GameObject>();
    List<GameObject> team5 = new List<GameObject>();
    BasicUnits enemyChoice;
    Vector2 box1;
    Vector2 box1S;
    bool camMove = false;
    
    

    private void Start()
    {
        lineLeft.GetComponent<SpriteRenderer>().enabled = false;
        lineRight.GetComponent<SpriteRenderer>().enabled = false;
        lineDown.GetComponent<SpriteRenderer>().enabled = false;
        lineUp.GetComponent<SpriteRenderer>().enabled = false;
        Cursor.visible = true;
        orignalMouse.GetComponent<SpriteRenderer>().enabled = true;
        targetMouse.GetComponent<SpriteRenderer>().enabled = false;
    }
    void CursorAdjust()
    {
        Vector2 place = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 place1 = new Vector2(place.x + 0.07f, place.y - 0.1f);
        orignalMouse.transform.position = place1;
        targetMouse.transform.position = place;
        if (Input.GetKeyDown("m"))
        {
            Cursor.visible = !Cursor.visible;
        }
    }
    void ChoseIndex()
    {
        int m = chosen.Count;
        if (m != 0)
        {
            for (int i = 0; i < chosen.Count; i++)
            {
                chosen[i].GetComponent<BasicUnits>().indexOfChos = i; 
            }
        }
    }
    void AdjustCam()
    {
        if (Input.GetKeyDown("c"))
            camMove = !camMove;
        if (camMove)
        {
            if (Input.mousePosition.x >= 1919)
            {
                cam.position += new Vector3(18 * Time.deltaTime, 0, 0);
            }
            if (Input.mousePosition.x <= 0)
            {
                cam.position -= new Vector3(18 * Time.deltaTime, 0, 0);
            }
            if (Input.mousePosition.y >= 1079)
            {
                cam.position += new Vector3(0, 18 * Time.deltaTime, 0);
            }
            if (Input.mousePosition.y <= 0)
            {
                cam.position -= new Vector3(0, 18 * Time.deltaTime, 0);
            }
        }
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
                lineLeft.transform.localScale = new Vector2(0.5f, scaleV);
                lineRight.transform.localScale = new Vector2(0.5f, scaleV);
                lineUp.transform.localScale = new Vector2(scaleH, 0.5f);
                lineDown.transform.localScale = new Vector2(scaleH, 0.5f);
            }
            if (Input.GetMouseButtonUp(0) && ishold)
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
                                    if (!boxChoice.Contains(a) && a != null)
                                    {
                                        a.GetComponent<BasicUnits>().isChosen = false;
                                    }
                                }
                            }
                            chosen = boxChoice;
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
        if (Input.GetKeyDown("a") && isOrign && chosen.Count != 0 && !ishold)
        {
            orignalMouse.GetComponent<SpriteRenderer>().enabled = false;
            targetMouse.GetComponent<SpriteRenderer>().enabled = true;
            isModeAtk = true;
            isOrign = false;
        }
    }
    public virtual void LClick()
    {
        if (isModeAtk && Input.GetMouseButtonDown(0) && chosen.Count != 0 && !ishold)
        {
            orignalMouse.GetComponent<SpriteRenderer>().enabled = true;
            targetMouse.GetComponent<SpriteRenderer>().enabled = false;
            isModeAtk = false;
            isOrign = true;
            Vector2 tarPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D[] click = Physics2D.OverlapCircleAll(tarPoint, 0.1f);
            foreach (GameObject unit in chosen)
            {
                unit.GetComponent<BasicUnits>().setPosition = null;
                if (click.Length != 0 && click[0].GetComponent<BasicUnits>() != null)
                {
                    if (click[0].gameObject == unit)
                    {
                        unit.GetComponent<BasicUnits>().state = 0;
                    }
                    else
                    {
                        unit.GetComponent<BasicUnits>().AddTar(click[0].gameObject);
                    }
                }
                else
                {
                    unit.GetComponent<BasicUnits>().TarPoint(tarPoint);
                    unit.GetComponent<BasicUnits>().state = 4; // attack at an point
                    unit.GetComponent<BasicUnits>().CancelTar();
                    unit.GetComponent<BasicUnits>().CancelArea();
                    unit.GetComponent<BasicUnits>().follow = null;
                    unit.GetComponent<BasicUnits>().isSeeking = false;
                }
            }
            
        }
    }
     void ExeModeAtk()
     {
         if(isModeAtk && !ishold)
         {
             if (Input.GetMouseButtonDown(1))
             {
                orignalMouse.GetComponent<SpriteRenderer>().enabled = true;
                targetMouse.GetComponent<SpriteRenderer>().enabled = false;
                isModeAtk = false;
                 isOrign = true;
             }
         }
     }
    void UIgame()
    {
        if (Input.GetKey("r"))
            SceneManager.LoadScene(0);
        if (Input.GetKey("q"))
            Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        CursorAdjust();
        ExeModeAtk();
        ChoseIndex();
        AdjustCam();
        ModeAtk();
        
        Choose();
        LClick();
        /*ExeModeAtk();*/
        UIgame();
    }
}
