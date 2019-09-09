using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGreen : skillPointUnits
{
    public GameObject greenPunch;
    public float spdRecover = 20;
    // Start is called before the first frame update
    public override void Start()
    {
        myBody.freezeRotation = true;
        myBody.isKinematic = false;
        punch();
    }

    void punch()
    {
        GameObject it = Instantiate(greenPunch);
        it.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 10);
    }

    // Update is called once per frame
    public override void Update()
    {
        Circle();
        CircleAnime();
        SkillCircle();
        BarChange();
        Layer();
    }
}
