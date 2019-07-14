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
    public float atkSpd;
    public float atkDis = 0;
    public float radius;
    public float Exp;
    public float pExp;
    public float arExp;
    public float sight = 7;
    public float seek;
    public int team = 1;
    public bool atkMode = true;
    public bool isChosen = false;
    public bool isSeen = true;
    public Console player;
    public Rigidbody2D myBody;
    bool isSeeking = false;
    bool isAttakcing = false;
    float atk;
    int state = 0; // initial state is stop
    public GameObject circle;
    GameObject follow;
    GameObject tarEnemy = null;
    GameObject tarEnemyHelper = null;
    GameObject areaEnemy = null;
    GameObject atkMe;
    Vector2 tarPoint;
    Vector2? setPosition = null;
    Vector2 tarEnemyPos;
    Collider2D[] click;
    List<GameObject> seenThings = new List<GameObject>();


    void Start()
    {
        if (team != 1)
        {
            isSeen = false;
        }
        myBody.freezeRotation = true;
        if (atkMode)
        {
            atk = phAtk;
        }
        else
        {
            atk = mgAtk;
        }
    }

    GameObject ToAttack(GameObject tar)
    {
        state = 2; // attack tarEnemy
        areaEnemy = null;
        follow = null;
        isSeeking = false;
        setPosition = null;
        return tar.gameObject;
    }

    Vector2 Moveing(Vector2 place)
    {
        return Vector2.MoveTowards(transform.position, place, movSpd * Time.deltaTime);
    }
    /*void Circle()
    {
        if (isChosen)
        {
            circle.GetComponent<sprite>
        }
    }*/
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (state == 3 && collision.gameObject == follow)
        {
            state = 0;
        }
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
        if (isSeen)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
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
                if (seenThings[i] == null)
                {
                    seenThings.RemoveAt(i);
                    i--;
                }
                if (Vector3.Distance(transform.position, seenThings[i].transform.position) > sight)
                {
                    seenThings[i].GetComponent<BasicUnits>().isSeen = false;
                    seenThings.RemoveAt(i);
                    i--;
                }
            }
        }
    }
    void killed()
    {
        if (HP <= 0)
        {
            Destroy(gameObject);
        }
    }

    void State()
    {
        if (state == 0 && setPosition != null && !isSeeking) // stop
        {
            if (transform.position != (Vector3)setPosition)
            {
                Moveing((Vector2)setPosition);
            }
            else
            {
                setPosition = null;
            }
        }
        if (state == 1 || (state == 4 && !isSeeking)) // move/attack to the place
        {
            if ((Vector2)transform.position == tarPoint)
            {
                state = 0;
            }
            else
            {
                transform.position = Moveing(tarPoint);
            }
        }
        else if (state == 2 && !isAttakcing) // attack tarenemy
        {
            if (tarEnemy == null)
            {
                state = 4;
                tarPoint = tarEnemyPos;
            }
            else if (tarEnemy.GetComponent<BasicUnits>().isSeen)
            {
                transform.position = Moveing(tarEnemy.transform.position);
            }
            else
            {
                state = 4;
                tarPoint = tarEnemy.transform.position;
                tarEnemy = null;
            }
        }
        else if (state == 3 && !isSeeking) // follow my unit
        {
            if (follow == null)
            {
                state = 0;
            }
            else
            {
                transform.position = Moveing(follow.transform.position);
            }
        }
        
        if (isSeeking && !isAttakcing) // seek to the areaEnemy
        {
            transform.position = Moveing(areaEnemy.transform.position);
        }
    }
    void tarPosition()
    {
        if (state == 2 && tarEnemy != null)
        {
            tarEnemyPos = tarEnemy.transform.position;
        }
    }
    void Attacking()
    {
        if (isSeeking && areaEnemy != null)
        {
            if (Vector2.Distance(transform.position, areaEnemy.transform.position) < radius + areaEnemy.GetComponent<BasicUnits>().radius + atkDis)
            {
                isAttakcing = true;
                areaEnemy.GetComponent<BasicUnits>().HP -= atk * Time.deltaTime;
            }
            else
            {
                isAttakcing = false;
            }
        }
        if (state == 2)
        {
            if (tarEnemy != null && Vector2.Distance(transform.position, tarEnemy.transform.position) < radius + tarEnemy.GetComponent<BasicUnits>().radius + atkDis)
            {
                isAttakcing = true;
                tarEnemy.GetComponent<BasicUnits>().HP -= atk * Time.deltaTime;
            }
            else
            {
                isAttakcing = false;
            }
        }
    }

    void checkenemies()
    {
        if (state != 1 && state != 2 && !isSeeking)
        {
            ArrayList enemieslist = new ArrayList();
            ArrayList dis = new ArrayList();
            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, 4f);
            if (enemies.Length > 0)
            {
                foreach (Collider2D unit in enemies)
                {
                    GameObject enemys = unit.gameObject;
                    if (enemys.GetComponent<BasicUnits>() != null)
                    {
                        if (enemys.GetComponent<BasicUnits>().team != team)
                        {
                            enemieslist.Add(enemys);
                            dis.Add(Vector2.Distance(enemys.transform.position, transform.position));
                        }
                    }
                }
            }
            if (dis.Count > 0)
            {
                float min = (float)dis[0];
                int index = 0;
                int indexc = 0;
                foreach (float i in dis)
                {
                    if (i < min)
                    {
                        min = i;
                        index = indexc;
                    }
                    indexc++;
                }
                areaEnemy = (GameObject)enemieslist[index];
                setPosition = transform.position;
                isSeeking = true;
            }
        }
    }
     
    void SeekEnemy()
    {
        if(isSeeking)
        {
            if (areaEnemy == null || Vector2.Distance((Vector2)setPosition, areaEnemy.transform.position) > seek || !areaEnemy.GetComponent<BasicUnits>().isSeen)
            {
                isSeeking = false;
                areaEnemy = null;
                if (state != 0)
                {
                    setPosition = null;
                }
            }
        }
    }

    void PresS()
    {
        if (Input.GetKeyDown("s"))
        {
            state = 0;
            isSeeking = false;
            follow = null;
            tarEnemy = null;
            areaEnemy = null;
            setPosition = null;
        }
    }
    void ModeAtk()
    {
        if (Input.GetKeyDown("a") && player.isOrign && player.chosen.Count != 0)
        {
            player.isModeAtk = true;
            player.isOrign = false;
        }
    }
    /*void ExeModeAtk()
    {
        if (player.isModeAtk)
        {
            if (Input.GetMouseButtonDown(0))
            {
                player.isModeAtk = false;
                player.isOrign = true;
            }
        }
    } */

    public virtual void LClick()
    {
        if (player.isModeAtk && Input.GetMouseButtonDown(0))
        {
            player.isModeAtk = false;
            player.isOrign = true; 
            tarPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            click = Physics2D.OverlapCircleAll(tarPoint, 0.1f);
            if (click.Length != 0 && click[0].GetComponent<BasicUnits>() != null)
            {
                if (click[0] == gameObject)
                {
                    state = 0;
                }
                else
                {
                    tarEnemy = ToAttack(click[0].gameObject);
                }
            }
            else
            {
                state = 4; // attack at an point
                tarEnemy = null;
                areaEnemy = null;
                follow = null;
                isSeeking = false;
                setPosition = null;
            }
        }
    }

    void RClick()
    {
        if (Input.GetMouseButtonDown(1) && player.isOrign)
        {
            tarPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            click = Physics2D.OverlapCircleAll(tarPoint, 0.1f);
            if (click.Length == 0 || click[0].GetComponent<BasicUnits>() == null)
            {
                state = 1; // receive ToMove order;
                tarEnemy = null;
                areaEnemy = null;
                follow = null;
                isSeeking = false;
                setPosition = null;
            }
            else
            {
                Collider2D tar = click[0];
                if (tar.gameObject == gameObject)
                {
                    state = 0; // stop
                    isSeeking = false;
                    follow = null;
                    tarEnemy = null;
                    areaEnemy = null;
                    setPosition = null;
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
                    isSeeking = false;
                    setPosition = null;
                }
            }
        }
    }

    void Update()
    {
        killed();
        IsSeen();
        Layer();
        See();
        DeleteSeen();
        if (isChosen)
        {
            LClick();
            RClick();
            PresS();
        }
        ModeAtk();
        tarPosition();
        checkenemies();
        SeekEnemy();
        State();
        Attacking();
    }
}
