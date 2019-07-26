﻿using System.Collections;
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
    public bool armyMode = false;
    public Transform cam;
    public GameObject lineLeft;
    public GameObject lineRight;
    public GameObject lineDown;
    public GameObject lineUp;
    public GameObject orignalMouse;
    public GameObject targetMouse;
    public GameObject ico;
    public GameObject HPtext;
    public GameObject 物攻;
    public GameObject 魔攻;
    public GameObject 物防;
    public GameObject 魔防;
    public GameObject 名字;
    public GameObject 等级;
    public GameObject 经验;
    public GameObject 技能1;
    public GameObject 技能2;
    public GameObject 技能3;
    public GameObject 技能4;
    public GameObject 物品1;
    public GameObject 物品2;
    public GameObject 物品3;
    public GameObject 物品4;
    public GameObject 物品5;
    public List<GameObject> myUnits = new List<GameObject>();
    public List<GameObject> chosen = new List<GameObject>();
    public Vector2 mouse;
    public GameObject fallin;
    public GameObject atkPoint;
    public List<GameObject> team1 = new List<GameObject>();
    public List<GameObject> team2 = new List<GameObject>();
    public List<GameObject> team3 = new List<GameObject>();
    public List<GameObject> team4 = new List<GameObject>();
    public List<GameObject> team5 = new List<GameObject>();
    public GameObject enemyChoice;
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

    void ChosenOne()
    {
        if (chosen.Count != 0)
        {
            ico.GetComponent<Image>().enabled = true;
            HPtext.GetComponent<Text>().enabled = true;
            物攻.GetComponent<Text>().enabled = true;
            魔攻.GetComponent<Text>().enabled = true;
            物防.GetComponent<Text>().enabled = true;
            魔防.GetComponent<Text>().enabled = true;
            名字.GetComponent<Text>().enabled = true;
            ico.GetComponent<Image>().sprite = chosen[0].GetComponent<SpriteRenderer>().sprite;
            HPtext.GetComponent<Text>().text = chosen[0].GetComponent<BasicUnits>().HP.ToString("0") + "/" + chosen[0].GetComponent<BasicUnits>().maxHp.ToString("0");
            物攻.GetComponent<Text>().text = "物攻：" + chosen[0].GetComponent<BasicUnits>().phAtk.ToString("0");
            魔攻.GetComponent<Text>().text = "魔攻：" + chosen[0].GetComponent<BasicUnits>().mgAtk.ToString("0");
            物防.GetComponent<Text>().text = "物防：" + chosen[0].GetComponent<BasicUnits>().phDef.ToString("0");
            魔防.GetComponent<Text>().text = "魔防：" + chosen[0].GetComponent<BasicUnits>().mgDef.ToString("0");
            名字.GetComponent<Text>().text = chosen[0].GetComponent<BasicUnits>().name;
            if (chosen[0].GetComponent<skilledUnits>() != null)
            {

                skilledUnits it = chosen[0].GetComponent<skilledUnits>();
                if (it.skill1.isLearned)
                {
                    if (it.skill1.currentCD > 0)
                    {
                        技能1.GetComponent<Text>().text = it.skill1.name + " " + Mathf.Ceil(it.skill1.currentCD).ToString();
                    }
                    else
                    {
                        技能1.GetComponent<Text>().text = it.skill1.name;
                    }
                    技能1.GetComponent<Text>().enabled = true;
                }
                if (it.skill2 != null && it.skill2.isLearned)
                {
                    if (it.skill2.currentCD > 0)
                    {
                        技能2.GetComponent<Text>().text = it.skill2.name + " " + Mathf.Ceil(it.skill2.currentCD).ToString();
                    }
                    else
                    {
                        技能2.GetComponent<Text>().text = it.skill2.name;
                    }
                    技能2.GetComponent<Text>().enabled = true;
                }
                if (it.skill3 != null && it.skill3.isLearned)
                {
                    if (it.skill3.currentCD > 0)
                    {
                        技能3.GetComponent<Text>().text = it.skill3.name + " " + Mathf.Ceil(it.skill3.currentCD).ToString();
                    }
                    else
                    {
                        技能3.GetComponent<Text>().text = it.skill3.name;
                    }
                    技能3.GetComponent<Text>().enabled = true;
                }
                if (it.skill4 != null && it.skill4.isLearned)
                {
                    if (it.skill4.currentCD > 0)
                    {
                        技能3.GetComponent<Text>().text = it.skill1.name + " " + Mathf.Ceil(it.skill4.currentCD).ToString();
                    }
                    else
                    {
                        技能3.GetComponent<Text>().text = it.skill1.name;
                    }
                    技能4.GetComponent<Text>().enabled = true;
                }
            }
            if (chosen[0].GetComponent<Hero>() != null)
            {
                Hero it = chosen[0].GetComponent<Hero>();
                等级.GetComponent<Text>().text = "等级：" + it.level.ToString();
                等级.GetComponent<Text>().enabled = true;
                经验.GetComponent<Text>().text = "经验：" + it.exp.ToString() + "/" + it.maxExp.ToString();
                经验.GetComponent<Text>().enabled = true;
            }
        }
        else if (enemyChoice != null)
        {
            ico.GetComponent<Image>().enabled = true;
            HPtext.GetComponent<Text>().enabled = true;
            物攻.GetComponent<Text>().enabled = true;
            魔攻.GetComponent<Text>().enabled = true;
            物防.GetComponent<Text>().enabled = true;
            魔防.GetComponent<Text>().enabled = true;
            名字.GetComponent<Text>().enabled = true;
            ico.GetComponent<Image>().sprite = enemyChoice.GetComponent<SpriteRenderer>().sprite;
            HPtext.GetComponent<Text>().text = enemyChoice.GetComponent<BasicUnits>().HP.ToString() + "/" + enemyChoice.GetComponent<BasicUnits>().maxHp.ToString("0");
            物攻.GetComponent<Text>().text = "物攻：" + enemyChoice.GetComponent<BasicUnits>().phAtk.ToString("0");
            魔攻.GetComponent<Text>().text = "魔攻：" + enemyChoice.GetComponent<BasicUnits>().mgAtk.ToString("0");
            物防.GetComponent<Text>().text = "物防：" + enemyChoice.GetComponent<BasicUnits>().phDef.ToString("0");
            魔防.GetComponent<Text>().text = "魔防：" + enemyChoice.GetComponent<BasicUnits>().mgDef.ToString("0");
            名字.GetComponent<Text>().text = enemyChoice.GetComponent<BasicUnits>().name;
            if (enemyChoice.GetComponent<skilledUnits>() != null)
            {
                skilledUnits it = enemyChoice.GetComponent<skilledUnits>();
                if (it.skill1.isLearned)
                {
                    if (it.skill1.currentCD > 0)
                    {
                        技能1.GetComponent<Text>().text = it.skill1.name + " " + Mathf.Ceil(it.skill1.currentCD).ToString();
                    }
                    else
                    {
                        技能1.GetComponent<Text>().text = it.skill1.name;
                    }
                    技能1.GetComponent<Text>().enabled = true;
                }
                if (it.skill2 != null && it.skill2.isLearned)
                {
                    if (it.skill2.currentCD > 0)
                    {
                        技能2.GetComponent<Text>().text = it.skill2.name + " " + Mathf.Ceil(it.skill2.currentCD).ToString();
                    }
                    else
                    {
                        技能2.GetComponent<Text>().text = it.skill2.name;
                    }
                    技能2.GetComponent<Text>().enabled = true;
                }
                if (it.skill3 != null && it.skill3.isLearned)
                {
                    if (it.skill3.currentCD > 0)
                    {
                        技能3.GetComponent<Text>().text = it.skill3.name + " " + Mathf.Ceil(it.skill3.currentCD).ToString();
                    }
                    else
                    {
                        技能3.GetComponent<Text>().text = it.skill3.name;
                    }
                    技能3.GetComponent<Text>().enabled = true;
                }
                if (it.skill4 != null && it.skill4.isLearned)
                {
                    if (it.skill4.currentCD > 0)
                    {
                        技能3.GetComponent<Text>().text = it.skill1.name + " " + Mathf.Ceil(it.skill4.currentCD).ToString();
                    }
                    else
                    {
                        技能3.GetComponent<Text>().text = it.skill1.name;
                    }
                    技能4.GetComponent<Text>().enabled = true;
                }
            }
            if (enemyChoice.GetComponent<Hero>() != null)
            {
                Hero it = enemyChoice.GetComponent<Hero>();
                等级.GetComponent<Text>().text = "等级：" + it.level.ToString();
                等级.GetComponent<Text>().enabled = true;
            }
        }
        else
        {
            ico.GetComponent<Image>().enabled = false;
            HPtext.GetComponent<Text>().enabled = false;
            物攻.GetComponent<Text>().enabled = false;
            魔攻.GetComponent<Text>().enabled = false;
            物防.GetComponent<Text>().enabled = false;
            魔防.GetComponent<Text>().enabled = false;
            等级.GetComponent<Text>().enabled = false;
            经验.GetComponent<Text>().enabled = false;
            技能1.GetComponent<Text>().enabled = false;
            技能2.GetComponent<Text>().enabled = false;
            技能3.GetComponent<Text>().enabled = false;
            技能4.GetComponent<Text>().enabled = false;
            物品1.GetComponent<Text>().enabled = false;
            物品2.GetComponent<Text>().enabled = false;
            物品3.GetComponent<Text>().enabled = false;
            物品4.GetComponent<Text>().enabled = false;
            物品5.GetComponent<Text>().enabled = false;
            名字.GetComponent<Text>().enabled = false;
        }
    }
    
    void ChangeChosen(List<GameObject> team)
    {
        foreach (GameObject unit in chosen)
        {
            unit.GetComponent<BasicUnits>().isChosen = false;
        }
        foreach (GameObject unit in team)
        {
            unit.GetComponent<BasicUnits>().isChosen = true;
        }
    }

    void GetArmy()
    {
        if (Input.GetKey(KeyCode.LeftControl) && chosen.Count != 0)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                team1 = chosen;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                team2 = chosen;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                team3 = chosen;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                team4 = chosen;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                team5 = chosen;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1) && team1.Count != 0)
        {
            ChangeChosen(team1);
            chosen = team1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && team2.Count != 0)
        {
            ChangeChosen(team2);
            chosen = team2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && team3.Count != 0)
        {
            ChangeChosen(team3);
            chosen = team3;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && team4.Count != 0)
        {
            ChangeChosen(team4);
            chosen = team4;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5) && team5.Count != 0)
        {
            ChangeChosen(team5);
            chosen = team5;
        }
    }

    void ColorOfMouse()
    {
        Collider2D[] units = Physics2D.OverlapCircleAll(mouse, 0.1f);
        if (units.Length != 0)
        {
            if (units[0].GetComponent<BasicUnits>().team == 1)
            {
                orignalMouse.GetComponent<Renderer>().material.color = new Color(0, 1, 0, 1);
                targetMouse.GetComponent<Renderer>().material.color = new Color(0, 1, 0, 1);
            }
            else
            {
                if (units[0].GetComponent<SpriteRenderer>().enabled)
                {
                    orignalMouse.GetComponent<Renderer>().material.color = new Color(1, 0, 0, 1);
                    targetMouse.GetComponent<Renderer>().material.color = new Color(1, 0, 0, 1);
                }

            }
        }
        else
        {
            orignalMouse.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 1);
            targetMouse.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 1);
        }
    }



    void CursorAdjust()
    {
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 place1 = new Vector2(mouse.x + 0.07f, mouse.y - 0.1f);
        orignalMouse.transform.position = place1;
        targetMouse.transform.position = mouse;
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
                float x1 = box1.x;
                float x2 = mouse.x;
                float y1 = box1.y;
                float y2 = mouse.y;
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
                ishold = false;
                Collider2D[] myunitss = Physics2D.OverlapAreaAll(box1, mouse);
                List<Collider2D> myunits = new List<Collider2D>();
                if (myunitss != null)
                {
                    foreach (Collider2D unit in myunitss)
                    {
                        if (unit.isTrigger)
                        {
                            myunits.Add(unit);
                        }
                    }
                }
                List<GameObject> boxChoice = new List<GameObject>();
                List<GameObject> enemyBox = new List<GameObject>();
                if (myunits != null)
                {
                    if (enemyChoice != null)
                    {
                        enemyChoice.GetComponent<BasicUnits>().enemyChosen = false;
                        enemyChoice = null;
                    }
                    foreach (Collider2D unit in myunits)
                    {
                        BasicUnits mine1 = unit.GetComponent<BasicUnits>();
                        if (mine1 != null)
                        {
                            if (mine1.team == 1)
                            {
                                if (mine1.GetComponent<Hero>() != null)
                                {
                                    boxChoice.Insert(0, unit.gameObject);
                                }
                                else
                                {
                                    boxChoice.Add(unit.gameObject);
                                }
                            }
                            else
                            {
                                enemyChoice = unit.gameObject;
                            }
                        }
                    }
                    if (boxChoice.Count > 0)
                    {
                        if (chosen.Count != 0)
                        {
                            chosen[0].GetComponent<BasicUnits>().isFirst = false;
                            foreach (GameObject a in chosen)
                            {
                                a.GetComponent<BasicUnits>().isChosen = false;
                            }
                        }
                        boxChoice[0].GetComponent<BasicUnits>().isFirst = true;
                        foreach (GameObject unit in boxChoice)
                        {
                            unit.GetComponent<BasicUnits>().isChosen = true;
                        }
                        chosen = boxChoice;
                        enemyChoice = null;
                    }
                    else 
                    {
                        if (enemyChoice != null && enemyChoice.GetComponent<SpriteRenderer>().enabled)
                        {
                            foreach (GameObject mine in chosen)
                            {
                                mine.GetComponent<BasicUnits>().isChosen = false;
                            }
                            chosen.Clear();
                            enemyChoice.GetComponent<BasicUnits>().enemyChosen = true;
                        }
                        else
                        {
                            enemyChoice = null;
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
       /* if (Input.GetMouseButtonDown(1) && isModeAtk)
        {
            isModeAtk = false;
            isOrign = true;
            orignalMouse.GetComponent<SpriteRenderer>().enabled = true;
            targetMouse.GetComponent<SpriteRenderer>().enabled = false;
        }*/
    }
    public virtual void LClick()
    {
        if (isModeAtk && Input.GetMouseButtonDown(0) && chosen.Count != 0 && !ishold)
        {
            orignalMouse.GetComponent<SpriteRenderer>().enabled = true;
            targetMouse.GetComponent<SpriteRenderer>().enabled = false;
            isModeAtk = false;
            isOrign = true;
            Collider2D[] click = Physics2D.OverlapCircleAll(mouse, 0.1f);
            foreach (GameObject unit in chosen)
            {
                unit.GetComponent<BasicUnits>().setPosition = null;
                if (click.Length != 0 && click[0].GetComponent<BasicUnits>() != null)
                {
                    if (click[0].gameObject == unit)
                    {
                        unit.GetComponent<BasicUnits>().CancelArea();
                        unit.GetComponent<BasicUnits>().CancelTar();
                        unit.GetComponent<BasicUnits>().state = 0;
                    }
                    else if (click[0].GetComponent<SpriteRenderer>().enabled)
                    {
                        unit.GetComponent<BasicUnits>().CancelArea();
                        click[0].GetComponent<BasicUnits>().circleAnime = true;
                        if (unit.GetComponent<BasicUnits>().tarEnemy != click[0].gameObject)
                        {
                            unit.GetComponent<BasicUnits>().AddTar(click[0].gameObject);
                        }
                    }
                }
                else
                {
                    atkPoint.transform.position = mouse;
                    if (atkPoint.GetComponent<ToMoveAnime>().isPlay)
                    {
                        atkPoint.GetComponent<ToMoveAnime>().time = 0f;
                        atkPoint.GetComponent<Animator>().Play("ToMovePoint");
                    }
                    else
                    {
                        atkPoint.GetComponent<ToMoveAnime>().isPlay = true;
                    }
                    unit.GetComponent<BasicUnits>().TarPoint(mouse);
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
        ChosenOne();
        GetArmy();
        ColorOfMouse();
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
