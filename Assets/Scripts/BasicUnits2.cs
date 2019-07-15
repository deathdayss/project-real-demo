using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BasicUnits2 : MonoBehaviour
{
    public int HP;
    public int phAtk;
    public int mgAtk;
    public float phDef;
    public float mgDef;
    public float movSpd;
    public float atkSpd = 1;
    public float atkDis = 0;
    public float thrSpd;
    public float radius;
    public float Exp;
    public float pExp;
    public float arExp;
    public float sight = 7;
    public float seek;
    public int team = 1;
    public int state = 0; // initial state is stop
    public bool atkMode = true;
    public bool isChosen = false;
    public bool isSeen = true;
    public Console player;
    public Rigidbody2D myBody;
    public GameObject circle;
    public GameObject tarEnemy = null;
    float time = 0;
    float timeHelper = 0;
    public bool isSeeking = false;
    bool isClosed = false;
    int atk;
    public GameObject follow;
    GameObject tarEnemyHelper = null;
    public GameObject areaEnemy = null;
    GameObject atkMe;
    public Vector2 tarPoint;
    public Vector2? setPosition = null;
    Vector2 tarEnemyPos;
    Collider2D[] click;
    public List<GameObject> seenThings = new List<GameObject>();


    void Start()
    {
        if (team != 1)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            player.myUnits.Add(gameObject);
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
        myBody.isKinematic = false;
    }

    public GameObject ToAttack(GameObject tar)
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
        /*if (collision.gameObject.GetComponent<BasicUnits>() != null)
        {
            if (((state == 1 && collision.gameObject.GetComponent<BasicUnits>().state == 1) || (state == 2 && collision.gameObject.GetComponent<BasicUnits>().state == 2)) && tarPoint == collision.gameObject.GetComponent<BasicUnits>().tarPoint)
            {
                if (Vector2.Distance(transform.position, tarPoint) < 0.5)
                {
                    state = 0;
                    collision.gameObject.GetComponent<BasicUnits>().state = 0;
                }
            }
            else*/
         
        if ((state == 1 || state == 4) && collision.collider.bounds.Contains(tarPoint))
        {
            Debug.Log(collision.collider.bounds);
            state = 0;
            gameObject.GetComponent<Collider2D>().bounds.Encapsulate(tarPoint);
        }
    }
      
    void Circle()
    {
        if (isChosen)
        {
            circle.GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            circle.GetComponent<SpriteRenderer>().enabled = false;
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

    /*void IsSeen()
    {
        if (isSeen)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }*/

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
                            if (!sth.GetComponent<SpriteRenderer>().enabled)
                            {
                                sth.GetComponent<SpriteRenderer>().enabled = true;
                            }
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
                    GameObject h = seenThings[i];
                    seenThings.RemoveAt(i);
                    i--;
                    bool k = false;
                    foreach (GameObject unit in player.myUnits)
                    {
                        if (unit.GetComponent<BasicUnits>().seenThings.Contains(h))
                        {
                            k = true;
                            break;
                        }
                    }
                    if (!k)
                    {
                        h.GetComponent<SpriteRenderer>().enabled = false;
                    }
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
            if ((Vector2)transform.position != (Vector2)setPosition)
            {
                
                transform.position = Moveing((Vector2)setPosition);
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
        else if (state == 2 && !isClosed) // attack tarenemy
        {
            if (tarEnemy == null)
            {
                state = 4;
                tarPoint = tarEnemyPos;
            }
            else if (tarEnemy.GetComponent<SpriteRenderer>().enabled)
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
        
        if (isSeeking && !isClosed) // seek to the areaEnemy
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
        if(isClosed)
        {
            time += Time.deltaTime;
            if (isSeeking && areaEnemy != null && time - timeHelper >= atkSpd)
            {
                areaEnemy.GetComponent<BasicUnits>().HP -= atk;
                time = timeHelper;
            }
            if (state == 2 && tarEnemy != null && time - timeHelper >= atkSpd)
            {
                tarEnemy.GetComponent<BasicUnits>().HP -= atk;
                time = timeHelper;
            }
        }
    }
    void CanAttack()
    {
        if (isSeeking && areaEnemy != null)
        {
            if (Vector2.Distance(transform.position, areaEnemy.transform.position) < radius + areaEnemy.GetComponent<BasicUnits>().radius + atkDis)
            {
                isClosed = true;
            }
            else
            {
                isClosed = false;
            }
        }
        if (state == 2)
        {
            if (tarEnemy != null && Vector2.Distance(transform.position, tarEnemy.transform.position) < radius + tarEnemy.GetComponent<BasicUnits>().radius + atkDis)
            {
                isClosed = true;
            }
            else
            {
                isClosed = false;
            }
        }
    }

    void checkenemies()
    {
        if (state != 1 && state != 2 && !isSeeking && !(state == 0 && setPosition != null))
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
            if (areaEnemy == null || Vector2.Distance((Vector2)setPosition, transform.position) > seek || !areaEnemy.GetComponent<SpriteRenderer>().enabled)
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
    /*void ModeAtk()
    {
        if (Input.GetKeyDown("a") && player.isOrign && player.chosen.Count != 0)
        {
            player.isModeAtk = true;
            player.isOrign = false;
        }
    }*/
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

    /*public virtual void LClick()
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
    }*/

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
        if (Input.GetMouseButtonDown(1) && player.isModeAtk)
        {
            player.isModeAtk = false;
            player.isOrign = true;
        }
    }

    void Update()
    {
        Circle();
        killed();
        /*IsSeen();*/
        Layer();
        See();
        DeleteSeen();
        if (isChosen)
        {
            /*LClick();*/
            RClick();
            PresS();
        }
        /*ModeAtk();*/
        tarPosition();
        checkenemies();
        SeekEnemy();
        State();
        CanAttack();
        Attacking();
    }
}
