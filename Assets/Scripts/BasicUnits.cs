using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BasicUnits : MonoBehaviour
{
    public float HP;
    public float phAtk;
    public float mgAtk;
    public float phDef;
    public float mgDef;
    public float movSpd;
    public float atSpd;
    public float atDIs;
    public float Exp;
    public float pExp;
    public float arExp;
    public float sight = 8;
    public int team = 1;
    public bool atkMode;
    public bool isChosen;
    public bool isSeen = true;
    public Console player;
    public Rigidbody2D myBody;
    GameObject follow;
    GameObject tarEnemy;
    GameObject areaEnemy;
    GameObject atkMe;
    Vector2 tarPoint;
    int state = 0; // initial state is stop
    Collider2D[] click;
    List<GameObject> seenThings = new List<GameObject>();


    void Start()
    {
        if (team != 1)
        {
            isSeen = false;
        }
        myBody.isKinematic = true;
    }

    GameObject ToAttack(GameObject tar)
    {
        state = 2; // attack tarEnemy
        areaEnemy = null;
        follow = null;
        return tar.gameObject;
    }
    void Layer()
    {
        if (team != 1)
        {
            Collider2D[] round = Physics2D.OverlapCircleAll(transform.position, sight);
            if (round.Length != 0)
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

    void IsSeen()
    {
        if(isSeen)
        {
            gameObject.GetComponent <SpriteRenderer>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    void See()
    {
        if (team == 1)
        {
            Collider2D[] round = Physics2D.OverlapCircleAll(transform.position, sight); 
            if (round.Length != 0)
            {
                foreach (Collider2D sth in round)
                {
                    float itsY = sth.transform.position.y;
                    float myY = gameObject.transform.position.y;
                    int myLayer = gameObject.GetComponent<Renderer>().sortingOrder;
                    int itsLayer = sth.GetComponent<Renderer>().sortingOrder;
                    if (myY > itsY && myLayer >= itsLayer)
                    {
                        gameObject.GetComponent<Renderer>().sortingOrder--;
                    }
                    if (myY < itsY && myLayer <= itsLayer)
                    {
                        gameObject.GetComponent<Renderer>().sortingOrder++;
                    }
                    if (sth.GetComponent<BasicUnits>() != null)
                    {
                        if (sth.GetComponent<BasicUnits>().team != 1 && !seenThings.Contains(sth.gameObject))
                        {
                            sth.GetComponent<BasicUnits>().isSeen = true;
                            seenThings.Add(sth.gameObject);
                        }
                    }
                }
            }
        }
    }
    void DeleteSeen()
    {
        if (seenThings.Count != 0)
        {
            for (int i = 0; i < seenThings.Count; i++)
            {
                if (Vector3.Distance(transform.position, seenThings[i].transform.position) > sight)
                {
                    seenThings[i].GetComponent<BasicUnits>().isSeen = false;
                    seenThings.RemoveAt(i);
                    i--;
                }
            }
        }
    }

    void State()
    {
        if(state == 1) 
        {
            transform.position = Vector2.MoveTowards(transform.position, tarPoint, movSpd * Time.deltaTime);
        }
        else if (state == 2)
        {
           /* if ()*/
        }
    }
    
    void LClick()
    {
        if(player.isModeAtk && Input.GetMouseButtonDown(0))
        {
            player.isModeAtk = false;
            tarPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            click = Physics2D.OverlapCircleAll(tarPoint, 1);
            if (click.Length != 0 && click[0].GetComponent<BasicUnits>() != null)
            {

                tarEnemy = ToAttack(click[0].gameObject);
            }
            else
            {
                state = 4; // attack at an point
            }
        }
    }

    void RClick()
    {
        if (Input.GetMouseButtonDown(1) && !player.isModeAtk)
        {
            tarPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            click = Physics2D.OverlapCircleAll(tarPoint, 1);
            if (click.Length == 0 || click[0].GetComponent<BasicUnits>() == null)
            {
                state = 1; // receive ToMove order;
                tarEnemy = null;
                areaEnemy = null;
                follow = null;
            }
            else
            {
                Collider2D tar = click[0];
                if (tar.gameObject == gameObject)
                {
                    state = 0; // stop
                }
                else if (tar.GetComponent<BasicUnits>().team != team)
                {
                    tarEnemy = ToAttack(tar.gameObject);
                }
                else
                {
                    state = 3; // follow my units
                    follow = tar.gameObject;
                    tarEnemy = null;
                    areaEnemy = null;
                }
            }
        }
    }

    void Update()
    {
        Layer();
        IsSeen();
        See();
        DeleteSeen();
    }
}
