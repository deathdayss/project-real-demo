using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hero : skillPointUnits
{
    
    public List<GeneralItem> Item = new List<GeneralItem>();
    public GeneralItem tarItem;
    public GeneralItem abandonItem;
    public int level;
    public int exp;
    public int maxExp;
    
    void levelUp()
    {
        if (exp >= maxExp)
        {
            level++;
            maxExp += 10;
        }
    }
 
    
    public override void RClick()
    {
        Vector2 mousePoint = Input.mousePosition;
        Vector2 tarPointk = Camera.main.ScreenToWorldPoint(mousePoint);
        click = Physics2D.OverlapCircleAll(tarPointk, 0.1f);
        /*if (isFirst && Input.GetMouseButtonDown(1) && player.isOrign && !player.ishold)
        {
            for(int i = 0; i < player.物品.Count; i++)
            {
                if (player.物品[i].GetComponent<Text>().enabled && Mathf.Abs(tarPoint.x - player.物品[i].transform.position.x) < 0.27 && Mathf.Abs(tarPoint.y - player.物品[i].transform.position.y) < 0.08)
                {
                    player.isOrign = false;
                    player.isPullItem = true;
                    player.pullItem = Item[i];
                    break;
                }
            }
        }*/
        if (Input.GetMouseButtonDown(1) && player.isOrign && !player.ishold)
        {
            if (click.Length == 0)
            {
                tarPointAnime.transform.position = tarPointk;
                if (tarPointAnime.GetComponent<ToMoveAnime>().isPlay)
                {
                    tarPointAnime.GetComponent<Animator>().Play("ToMovePoint");
                    tarPointAnime.GetComponent<ToMoveAnime>().time = 0f;
                }
                else
                {
                    tarPointAnime.GetComponent<ToMoveAnime>().isPlay = true;
                }
                TarPoint(tarPointk);
                state = 1; // receive ToMove order;
                CancelTar();
                CancelArea();
                follow = null;
                isSeeking = false;
                setPosition = null;
            }
            else if (click[0].GetComponent<BasicUnits>() != null)
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
                    if (tar.GetComponent<SpriteRenderer>().enabled)
                    {
                        tar.GetComponent<BasicUnits>().circleAnime = true;
                        CancelArea();
                        if (tarEnemy != tar.gameObject)
                        {
                            AddTar(tar.gameObject);
                        }
                    }
                }
                else
                {
                    tar.GetComponent<BasicUnits>().circleAnime = true;
                    state = 3; // follow my units
                    follow = tar.gameObject;
                    CancelTar();
                    CancelArea();
                    isSeeking = false;
                    setPosition = null;
                }
            }
            else if (click[0].GetComponent<GeneralItem>() != null)
            {
                if (click[0].GetComponent<GeneralItem>().owner == null)
                {
                    tarItem = click[0].GetComponent<GeneralItem>();
                    tarPoint = tarItem.transform.position;
                    state = 5; // receive ToMoveToGetItem order;
                    CancelTar();
                    CancelArea();
                    follow = null;
                    isSeeking = false;
                    setPosition = null;
                }
            }
            else
            {
                tarPointAnime.transform.position = tarPointk;
                if (tarPointAnime.GetComponent<ToMoveAnime>().isPlay)
                {
                    tarPointAnime.GetComponent<Animator>().Play("ToMovePoint");
                    tarPointAnime.GetComponent<ToMoveAnime>().time = 0f;
                }
                else
                {
                    tarPointAnime.GetComponent<ToMoveAnime>().isPlay = true;
                }
                TarPoint(tarPointk);
                state = 1; // receive ToMove order;
                CancelTar();
                CancelArea();
                follow = null;
                isSeeking = false;
                setPosition = null;
            }
        }
    }
    public void GetItem()
    {
        if (state == 5 && tarItem != null)
        {
            if (tarItem.owner != null)
            {
                tarItem = null;
                state = 0;
            }
            else if (Vector2.Distance(transform.position, tarItem.transform.position) < radius + tarItem.radius)
            {
                state = 0;
                if (tarItem.consumable)
                {
                    bool have = false;
                    foreach(GeneralItem thing in Item)
                    {
                        if (thing.name == tarItem.name)
                        {
                            thing.num++;
                            have = true;
                            Destroy(tarItem.gameObject);
                            break;
                        }
                    }
                    if (!have && Item.Count < 5)
                    {
                        tarItem.owner = gameObject;
                        tarItem.PassiveEffect();
                        Item.Add(tarItem);
                        tarItem = null;
                    }
                }
                else if (Item.Count < 5)
                {
                    tarItem.owner = gameObject;
                    tarItem.PassiveEffect();
                    Item.Add(tarItem);
                }
                tarItem = null;
            }
            else
            {
                transform.position = Moveing(tarPoint);
            }
        }
    }
    public override void Update()
    {
        base.Update();
        levelUp();
        GetItem();
        SkillCircle();
    }
}
