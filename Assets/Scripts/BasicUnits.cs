using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BasicUnits : MonoBehaviour
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
    public int indexOfChos;
    public int team = 1;
    public int state = 0; // initial state is stop
    public bool atkMode = true;
    public bool isChosen = false;
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
    public Vector2 tarPoint;
    public Vector2 atkPoint;
    public Vector2? setPosition = null;
    Vector2 tarEnemyPos;
    Collider2D[] click;
    public List<GameObject> attacker = new List<GameObject>();
    public List<GameObject> seenThings = new List<GameObject>();
    public List<GameObject> dirctRes = new List<GameObject>();
    public List<Vector2> dirct = new List<Vector2>();
    public Collision2D temCollision;
    float colli = 0;
    bool collis = false;


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
        for (int i = 0; i < 8; i++)
        {
            dirctRes.Add(null);
        }
    }
    

    /*public GameObject ToAttack(GameObject tar)
    {
        state = 2; // attack tarEnemy
        areaEnemy = null;
        follow = null;
        isSeeking = false;
        setPosition = null;
        return tar.gameObject;
    }*/

    Vector2 Moveing(Vector2 place)
    {
        return Vector2.MoveTowards(transform.position, place, movSpd * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        temCollision = collision;
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
         
        /*if ((state == 1 || state == 4) && collision.collider.bounds.Contains(tarPoint))
        {
            Debug.Log(collision.collider.bounds);
            state = 0;
            gameObject.GetComponent<Collider2D>().bounds.Encapsulate(tarPoint);
        }*/
        /*if (collision.gameObject != tarEnemy && collision.gameObject != areaEnemy)
        {
            Debug.Log("f");
            Vector2 line = collision.transform.position - transform.position;
            Vector2 uLine = line / line.magnitude;
            Vector2 change = new Vector2(- uLine.y, uLine.x);
            Moveing((Vector2)transform.position + change);
        }*/
    }
    /*void OnCollisionStay2D(Collision2D collision)
    {
        if (collision == temCollision)
        {
            colli += Time.deltaTime;
            if (colli > 1)
            {
                collis = true;
                CheckCollision();
            }
        }
    }*/
    void CheckCollision()
    {
        if (temCollision != null && collis)
        {
            Debug.Log("arrive");
            if (temCollision.gameObject != tarEnemy && temCollision.gameObject != areaEnemy)
            {
                /*Debug.Log("f");
                Vector2 uLine = line / length;
                Vector2 change = new Vector2(-uLine.y, uLine.x);
                Moveing((Vector2)transform.position + change);*/
                Vector2 line = temCollision.transform.position - transform.position;
                float length = line.magnitude;
                if (length < 0.8f)
                {
                    
                    Vector2 uLine = line / length;
                    
                    Vector2 change = 1.4f * new Vector2(-uLine.y, uLine.x);
                    transform.position = Moveing((Vector2)transform.position + change);
                }
                else
                {
                    temCollision = null;
                    collis = false;
                    colli = 0;
                }
            }
            else
            {
                temCollision = null;
                colli = 0;
                collis = false;
            }
            
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
    private static int CompareDis(GameObject a, GameObject b)
    {
        float disA = 0;
        float disB = 0;
        if (a.GetComponent<BasicUnits>().tarEnemy != null)
        {
            disA = Vector2.Distance(a.transform.position, a.GetComponent<BasicUnits>().tarEnemy.transform.position);
        }
        else
        {
            disA = Vector2.Distance(a.transform.position, a.GetComponent<BasicUnits>().areaEnemy.transform.position);
        }
        if (b.GetComponent<BasicUnits>().tarEnemy != null)
        {
            disB = Vector2.Distance(b.transform.position, b.GetComponent<BasicUnits>().tarEnemy.transform.position);
        }
        else
        {
            disB = Vector2.Distance(b.transform.position, b.GetComponent<BasicUnits>().areaEnemy.transform.position);
        }
        if (disA > disB)
        {
            return 1;
        }
        if (disA < disB)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }
    void SeekDirct()
    {
        if (attacker.Count != 0)
        {
            dirct.Clear();
            float r = radius + 0.6f;
            float m = Mathf.Cos(Mathf.PI / 4) * r;
            dirct.Add((Vector2)transform.position + new Vector2(r, 0));
            dirct.Add((Vector2)transform.position + new Vector2(m, m));
            dirct.Add((Vector2)transform.position + new Vector2(0, r));
            dirct.Add((Vector2)transform.position + new Vector2(-m, m));
            dirct.Add((Vector2)transform.position + new Vector2(-r, 0));
            dirct.Add((Vector2)transform.position + new Vector2(-m, -m));
            dirct.Add((Vector2)transform.position + new Vector2(0, -r));
            dirct.Add((Vector2)transform.position + new Vector2(m, -m));
        }
    }

    Vector2 GetAttackPoint2(int i1, int i2, GameObject unit)
    {
        if (dirctRes[i1] == unit)
        {
            return dirct[i1];
        }
        else if (dirctRes[i2] == unit)
        {
            return dirct[i2];
        }
        else if (dirctRes[i1] == null)
        {
            int k = dirctRes.IndexOf(unit);
            if (k != -1)
            {
                dirctRes.Remove(unit);
                dirctRes.Insert(k, null);
            }
            dirctRes[i1] = unit;
            return dirct[i1];
        }
        else if (dirctRes[i2] == null)
        {
            int k = dirctRes.IndexOf(unit);
            if (k != -1)
            {
                dirctRes.Remove(unit);
                dirctRes.Insert(k, null);
            }
            dirctRes[i2] = unit;
            return dirct[i2];
        }
        else if (i1 == i2)
        {
            return transform.position;
        }
        else if (i1 == 0)
        {
            return GetAttackPoint2(7, i2 + 1, unit);
        }
        else if (i2 == 7)
        {
            return GetAttackPoint2(i1, 0, unit);
        }
        else
        {
            return GetAttackPoint2(i1 - 1, i2 + 1, unit);
        }
    }

    Vector2 GetAttackPoint(int index, GameObject unit)
    {
        if (dirctRes[index] == unit)
        {
            return dirct[index];
        }
        else if (dirctRes[index] == null)
        {
            int k = dirctRes.IndexOf(unit);
            if (k != -1)
            {
                dirctRes.Remove(unit);
                dirctRes.Insert(k, null);
            }
            dirctRes[index] = unit;
            return dirct[index];
        }
        else if (index == 0)
        {
            return GetAttackPoint2(1, 7, unit);
        }
        else if (index == 7)
        {
            return GetAttackPoint2(0, 6, unit);
        }
        else
        {
            return GetAttackPoint2(index - 1, index + 1, unit);
        }
    }

    void GiveAtkPoint()
    {
        if (attacker.Count != 0)
        {
            attacker.Sort(CompareDis);
            foreach(GameObject unit in attacker)
            {
                Vector2 t1 = unit.transform.position - transform.position;
                Vector2 uvect = t1 / t1.magnitude;
                float vx = uvect.x;
                float vy = uvect.y;
                float m = Mathf.Sin(Mathf.PI / 8);
                float n = Mathf.Cos(Mathf.PI / 8);
                if (vx >= 0)
                {
                    if (vy >= -m && vy <= m)
                    {
                        unit.GetComponent<BasicUnits>().atkPoint = GetAttackPoint(0, unit);
                    }
                    if (vy >= m && vy <= n)
                    {
                        unit.GetComponent<BasicUnits>().atkPoint = GetAttackPoint(1, unit);
                    }
                    if (vy >= n)
                    {
                        unit.GetComponent<BasicUnits>().atkPoint = GetAttackPoint(2, unit);
                    }
                    if (vy <= -m && vy >= -n)
                    {
                        unit.GetComponent<BasicUnits>().atkPoint = GetAttackPoint(7, unit);
                    }
                    if (vy <= -n)
                    {
                        unit.GetComponent<BasicUnits>().atkPoint = GetAttackPoint(6, unit);
                    }
                }
                if (vx < 0)
                {
                    if (vy >= -m && vy <= m)
                    {
                        unit.GetComponent<BasicUnits>().atkPoint = GetAttackPoint(4, unit);
                    }
                    if (vy >= m && vy <= n)
                    {
                        unit.GetComponent<BasicUnits>().atkPoint = GetAttackPoint(3, unit);
                    }
                    if (vy <= -m && vy >= -n)
                    {
                        unit.GetComponent<BasicUnits>().atkPoint = GetAttackPoint(5, unit);
                    }
                }
            }
        }
    }

    void killed()
    {
        if (HP <= 0)
        {
            CancelTar();
            CancelArea();
            Destroy(gameObject);
        }
    }
    public void AddTar(GameObject tar) // tool method
    {
        tarEnemy = tar;
        tarEnemy.GetComponent<BasicUnits>().attacker.Add(gameObject);
        state = 2; // attack tarEnemy
        CancelArea();
        follow = null;
        isSeeking = false;
        setPosition = null;
    }

    public void AddArea(GameObject area) // tool method
    {
        areaEnemy = area;
        areaEnemy.GetComponent<BasicUnits>().attacker.Add(gameObject);
    }

    public void CancelTar() // tool method
    {
        if (tarEnemy != null)
        {
            tarEnemy.GetComponent<BasicUnits>().attacker.Remove(gameObject);
            int k = tarEnemy.GetComponent<BasicUnits>().dirctRes.IndexOf(gameObject);
            if (k != -1)
            {
                tarEnemy.GetComponent<BasicUnits>().dirctRes.Remove(gameObject);
                tarEnemy.GetComponent<BasicUnits>().dirctRes.Insert(k, null);
            }
            tarEnemy = null;
        }
    }
    public void CancelArea() // tool method
    {
        if (areaEnemy != null)
        {
            areaEnemy.GetComponent<BasicUnits>().attacker.Remove(gameObject);
            int k = areaEnemy.GetComponent<BasicUnits>().dirctRes.IndexOf(gameObject);
            if (k != -1)
            {
                areaEnemy.GetComponent<BasicUnits>().dirctRes.Remove(gameObject);
                areaEnemy.GetComponent<BasicUnits>().dirctRes.Insert(k, null);
            }
            areaEnemy = null;
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
                if ((Vector2)transform.position == atkPoint)
                {
                    atkPoint = tarEnemy.transform.position;
                }
                transform.position = Moveing(atkPoint);
            }
            else
            {

                CancelTar();
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
            if ((Vector2)transform.position == atkPoint)
            {
                atkPoint = areaEnemy.transform.position;
            }
            /*transform.position = Moveing(areaEnemy.transform.position);*/ // Epic Trouble. Must be understood and solved
            transform.position = Moveing(atkPoint);
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
                AddArea((GameObject)enemieslist[index]);
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
                CancelArea();
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
            CancelTar();
            CancelArea();
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
    public void TarPoint(Vector2 click)
    {
        float k = 2.2f;
        Vector2 uUp = k * new Vector2(0, 0.5f);
        Vector2 uRight = k * new Vector2(0.5f, 0);
        Vector2 uDown = k * new Vector2(0, -0.5f);
        Vector2 uLeft = k * new Vector2(-0.5f, 0);
        if (indexOfChos == 0)
        {
            tarPoint = click;
        }
        else if (indexOfChos == 1)
        {
            tarPoint = click + uUp;
        }
        else if (indexOfChos == 2)
        {
            tarPoint = click + uRight;
        }
        else if (indexOfChos == 3)
        {
            tarPoint = click + uDown;
        }
        else if (indexOfChos == 4)
        {
            tarPoint = click + uLeft;
        }
        else if (indexOfChos == 5)
        {
            tarPoint = click + uUp + uLeft;
        }
        else if (indexOfChos == 6)
        {
            tarPoint = click + uRight + uDown;
        }
        else if (indexOfChos == 7)
        {
            tarPoint = click + uDown + uLeft;
        }
        else if (indexOfChos == 8)
        {
            tarPoint = click + uLeft + uUp;
        }
        else if (indexOfChos == 9)
        {
            tarPoint = click + 2 * uUp;
        }
        else if (indexOfChos == 10)
        {
            tarPoint = click + 2 * uRight;
        }
        else if (indexOfChos == 11)
        {
            tarPoint = click + 2 * uDown;
        }
        else if (indexOfChos == 12)
        {
            tarPoint = click + 2 * uLeft;
        }
        else if (indexOfChos == 13)
        {
            tarPoint = click + 2 * uUp + uRight;
        }
        else if (indexOfChos == 14)
        {
            tarPoint = click + 2 * uRight + uUp;
        }
        else if (indexOfChos == 15)
        {
            tarPoint = click + 2*uRight + uDown;
        }
        else if (indexOfChos == 16)
        {
            tarPoint = click + 2 * uDown + uRight;
        }
        else if (indexOfChos == 17)
        {
            tarPoint = click + 2 * uDown + uLeft;
        }
        else if (indexOfChos == 18)
        {
            tarPoint = click + 2 * uLeft + uDown;
        }
        else if (indexOfChos == 19)
        {
            tarPoint = click + 2 * uLeft + uUp;
        }
        else if (indexOfChos == 20)
        {
            tarPoint = click + 2 * uUp + uLeft;
        }
        else if (indexOfChos == 21)
        {
            tarPoint = click + 2 * uRight + 2 * uUp;
        }
        else if (indexOfChos == 22)
        {
            tarPoint = click + 2 * uRight + 2 * uDown;
        }
        else if (indexOfChos == 23)
        {
            tarPoint = click + 2 * uLeft + 2 * uDown;
        }
        else if (indexOfChos == 24)
        {
            tarPoint = click + 2 * uUp + 2 * uLeft;
        }
    }

    void RClick()
    {
        if (Input.GetMouseButtonDown(1) && player.isOrign)
        {
            Vector2 tarPointk = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            click = Physics2D.OverlapCircleAll(tarPointk, 0.1f);
            if (click.Length == 0 || click[0].GetComponent<BasicUnits>() == null)
            {
                TarPoint(tarPointk);
                state = 1; // receive ToMove order;
                CancelTar();
                CancelArea();
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
                    CancelTar();
                    CancelArea();
                    setPosition = null;
                }
                else if (tar.GetComponent<BasicUnits>().team != team)
                {
                    AddTar(tar.gameObject);
                }
                else
                {
                    state = 3; // follow my units
                    follow = tar.gameObject;
                    CancelTar();
                    CancelArea();
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
        CheckCollision();
        SeekDirct();
        GiveAtkPoint();
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
        if (temCollision == null)
        {
           State();
        }
        CanAttack();
        Attacking();
    }
}
