using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BindSpirit : skillPointUnits
{
    public float maxCD;
    public float currentCD;
    public float maxTime;
    public float lastTime;
    public float damage;
    public GameObject bindTarget;
    public GameObject bindPic;
    public BindSpirit retainBind;
    public Vector2 stayPoint;
    public float timeBind;
    public float timeBindHelper;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        bindPic.GetComponent<SpriteRenderer>().enabled = false;
        if(retainIt != null)
        {
            maxCD = retainBind.maxCD;
            maxTime = retainBind.maxTime;
            damage = retainBind.damage;
        }
    }

    void binding()
    {
        if (bindTarget != null)
        {
            timeBind += Time.deltaTime;
            if(timeBind - timeHelper >= 1)
            {
                timeHelper = timeBind;
                bindTarget.GetComponent<BasicUnits>().HP -= damage - bindTarget.GetComponent<BasicUnits>().mgDef;
            }
            bindTarget.transform.position = stayPoint;
            if(lastTime <= 0)
            {
                bindPic.GetComponent<SpriteRenderer>().enabled = false;
                bindTarget = null;
            }
        }
        else
        {
            lastTime = 0;
            bindPic.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    void bind(GameObject enemy)
    {
        if(currentCD <= 0 && skillPoint >= 4 && Vector2.Distance(transform.position, enemy.transform.position) <= 4)
        {
            timeBind = 0;
            timeBindHelper = 0;
            currentCD = maxCD;
            skillPoint -= 4;
            bindTarget = enemy;
            lastTime = maxTime;
            stayPoint = enemy.transform.position;
            bindPic.GetComponent<SpriteRenderer>().enabled = true;
            bindPic.transform.position = new Vector2(stayPoint.x, stayPoint.y - 0.01f);
        }
    }

    void canBind()
    {
        if(tarEnemy != null)
        {
            bind(tarEnemy);
        } else if(areaEnemy != null)
        {
            bind(areaEnemy);
        }
    }

    // Update is called once per frame
    public override void Update()
    {
        if (currentCD > 0)
        {
            currentCD -= Time.deltaTime;
        }
        if(lastTime > 0)
        {
            lastTime -= Time.deltaTime;
        }
        canBind();
        binding(); 
        base.Update();   
    }
}
