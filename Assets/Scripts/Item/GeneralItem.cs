using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralItem : MonoBehaviour
{
    public GameObject owner;
    public Console player;
    public float radius;
    public string name;
    public string description;
    public bool consumable;
    public int num = 1;
    public bool canUse;
    public bool haveCD;
    public bool canAbandon = true;
    public float maxCD;
    public float currentCD;
    public string place;

    public virtual void PassiveEffect()
    {
        
    }

    public virtual void abandon()
    {

    }

    public virtual void PositiveEffect()
    {
        if (consumable)
        {
            if (--num < 1)
            {
                owner.GetComponent<Hero>().Item.Remove(this);
                Destroy(gameObject);
            }
        }
    }

    public virtual void Update()
    {
        if (owner != null)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            PassiveEffect();
            if (canUse)
            {
                place = (owner.GetComponent<Hero>().Item.IndexOf(gameObject.GetComponent<GeneralItem>()) + 1).ToString();
                if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(place))
                {
                    PositiveEffect();
                }
            }
        }
    }
}
