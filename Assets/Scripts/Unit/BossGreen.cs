using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGreen : skillPointUnits
{
    public GameObject greenPunchLine;
    public GameObject greenPunchRound;
    public GameObject bindingMonster;
    public GameObject treeMan;
    public List<GameObject> trees;
    public List<GameObject> bindingArea;
    int summonTime = 0;
    int stage = 0;
    public float spdRecover = 20;
    float punchSpd = 6;
    float atkTime = 0;
    float atkTimeHelper = 0;
    float bindingTime = 0;
    public List<GameObject> linePoint = new List<GameObject>();
    public List<GameObject> roundClose = new List<GameObject>();
    public List<GameObject> roundFar= new List<GameObject>();
    public GameObject lastThings;
    public int bindingF = 5;
    int punchNum = 3;
    int pro = 2;
    // Start is called before the first frame update
    public override void Start()
    {
        //myBody.freezeRotation = true;
        //myBody.isKinematic = false;
        punchLine(4);
    }

    List<GameObject> shuffleList(List<GameObject> tarList)
    {
        List<GameObject> newList = new List<GameObject>();
        int length = tarList.Count;
        for(int i = 0; i < length; i++)
        {
            int indexOf = Random.Range(0, tarList.Count);
           // Debug.Log("random is " + indexOf);
           // Debug.Log("length is " + tarList.Count);
            newList.Add(tarList[indexOf]);
            tarList.RemoveAt(indexOf);
        }
        return newList;
    }

    void punchLine(int num)
    {

        linePoint = shuffleList(linePoint);
        for (int i = 0; i < num; i++)
        {
            GameObject it = Instantiate(greenPunchLine);
            it.transform.position = linePoint[i].transform.position;
            it.SetActive(true);
        }
    }
    void PunchRound(int num)
    {
        for(int i = 0; i < num; i++)
        {
            List<GameObject> tarList;
            if(Random.Range(0, 2) == 0)
            {
                tarList = roundClose;
            }
            else
            {
                tarList = roundFar;
            }
            if(Random.Range(0, 2) == 0) {
                tarList.Reverse();
            }
            GameObject it = Instantiate(greenPunchRound);
            it.GetComponent<PunchItRound>().targetPoint = tarList;
            it.transform.position = tarList[0].transform.position;
            it.SetActive(true);
        }
    }
    void summon(GameObject sm1, GameObject sm2)
    {
        GameObject t1 = Instantiate(sm1);
        GameObject t2 = Instantiate(sm2);
        t1.transform.position = trees[2 * summonTime].transform.position;
        t2.transform.position = trees[2 * summonTime + 1].transform.position;
        t1.transform.parent = gameObject.transform;
        t2.transform.parent = gameObject.transform;
        Destroy(trees[2 * summonTime]);
        Destroy(trees[2 * summonTime + 1]);
        summonTime++;
    }

    // Update is called once per frame
    public override void Update()
    {
        atkTime += Time.deltaTime;
        if(stage == 0 && HP <= maxHp * 3/2 && HP >= maxHp/3) {
            punchNum = 4;
            stage++;
        } else if(stage == 1 && HP < maxHp/3)
        {
            punchSpd = 5;
            stage++;
        } else if (stage == 2 && HP < maxHp/6)
        {
            punchNum = 5;
            pro = 3;
            stage++;
        }

        if(HP <= maxHp * 3/4 && summonTime == 0)
        {
            summon(treeMan, treeMan);
        } else if(HP <= maxHp/2 && summonTime == 1)
        {
            summon(treeMan, bindingMonster);
        } else if(HP <= maxHp/4 && summonTime == 2)
        {
            summon(bindingMonster, bindingMonster);
        }

        if(atkTime - atkTimeHelper >= punchSpd)
        {
            atkTimeHelper = atkTime;
            //punchLine(4);
            punchLine(punchNum);
            if(stage >= 2)
            {
                int probability = Random.Range(0, 5);
                if(probability < pro)
                {
                    PunchRound(1);
                }
            }
        }
        if (atkTime - bindingTime >= bindingF * punchSpd)
        {
            bindingTime = atkTime;
            foreach(GameObject area in bindingArea)
            {
                area.GetComponent<BindingBoss>().isActive = true;
            }
        }
        if(HP <= 0)
        {
            Destroy(lastThings);
        }
        Circle();
        CircleAnime();
        SkillCircle();
        BarChange();
        Layer();
    }
}
