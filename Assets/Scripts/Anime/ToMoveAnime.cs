using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToMoveAnime : MonoBehaviour
{
    public bool isPlay = false;
    public float time = 0;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<Animator>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlay)
        {
            gameObject.GetComponent<Animator>().Play("ToMovePoint 0");
            time += Time.deltaTime;
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            gameObject.GetComponent<Animator>().enabled = true;
            if (time >= 0.65)
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                gameObject.GetComponent<Animator>().enabled = false;
                gameObject.GetComponent<Animator>().Play("ToMovePoint");
                isPlay = false;
                time = 0;
            }
        }
    }
}
