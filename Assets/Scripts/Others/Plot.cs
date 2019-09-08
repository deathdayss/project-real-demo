using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plot : MonoBehaviour
{
    public GameObject canvas;
    public GameObject content;
    public List<GameObject> textAll = new List<GameObject>();
    public List<GameObject> recordAll = new List<GameObject>();
    public float width = 507;
    public float height = 26.2f;
    public float widthR = 985.5f;
    public float hightR = 43.9f;
    List<string[]> dialogue = new List<string[]>();
    public float eachTime = 5;
    int current = 0;
    bool isTexting = false;
    float timeKeeper = 0;
    // Start is called before the first frame update
    public void Start()
    {
        string[] d1 = new string[1];
        d1[0] = "亚提斯：我是典型冒险者，被派来消灭这个地区的魔物。";
        string[] d2 = new string[3];
        d2[1] = "维京战士A：北方有一个洞窟，听说那里有强大的魔物，控制着这片区域。";
        d2[2] = "亚提斯：你说是那便是，是兄弟就去砍它们。";
        string[] d3 = new string[3];
        d3[1] = "弓箭手A：穿过这条湖边的走廊就抵达洞窟，我们部分人已经深入其中了。";
        d3[2] = "亚提斯：（兵力增加了，开心！）";
        string[] d4 = new string[4];
        d4[1] = "亚提斯：我日，什么鬼！";
        d4[2] = "弓箭手A：我们的退路被封住了。";
        d4[3] = "亚提斯：干！先往里走吧，看看会不会遇到什么特殊事件来解围。";
        string[] d5 = new string[5];
        d5[1] = "亚提斯：这里人怎么都被缠住了（是要打boss了吗？）。";
        d5[2] = "维京战士B：小心！";
        d5[3] = "藤妖：愚蠢的人类该死！";
        d5[4] = "亚提斯：nmsl！";
        string[] d6 = new string[9];
        d6[1] = "亚提斯：这藤蔓砍不断啊，进来的路口被封了，怎么出去？";
        d6[2] = "维京战士B：这个藤蔓用炸药都炸不破。我们试过了。";
        d6[3] = "亚提斯：？？";
        d6[4] = "维京战士B：我们之前在洞窟的西边埋了炸药，但没来得及引爆";
        d6[5] = "，我们就被藤妖抓住了。我们现在可以去引爆，出洞。我们被抓";
        d6[6] = "住期间，听藤妖说她们的老大在南边森林里。我想消灭他之后，";
        d6[7] = "这个藤蔓应该就会枯萎。";
        d6[8] = "亚提斯：你任务都布置的这么明确了，那就干咯。";
        string[] d7 = new string[2];
        d7[1] = "维京战士B：点火。";
        string[] d8 = new string[2];
        d8[1] = "维京战士B：啊，树变妖怪了！";
        string[] d9 = new string[2];
        d9[1] = "亚提斯：这就是boss吗？也太恶心了吧。";
        dialogue.Add(d1);
        dialogue.Add(d2);
        dialogue.Add(d3);
        dialogue.Add(d4);
        dialogue.Add(d5);
        dialogue.Add(d6);
        dialogue.Add(d7);
        dialogue.Add(d8);
        dialogue.Add(d9);
        for(int i = 0; i < dialogue.Count; i++)
        {
            if(i != 0)
            {
                string it = "";
                for(int m = 0; m < 56; m++)
                {
                    it += "-";
                }
                dialogue[i][0] = it;
            }
        }

    }

    // Add text to screen and record 
    public void getText()
    {
        isTexting = true;
        int m = 1;
        if(current == 0)
        {
            m = 0;
        }
        
        for(int i = 0; i < dialogue[current].Length; i++)
        {
            GameObject record = new GameObject();
            recordAll.Add(record);
            record.AddComponent<Text>();
            Text rText = record.GetComponent<Text>();
            rText.color = Color.black;
            rText.text = dialogue[current][i];
            rText.fontSize = 35;
            rText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            content.GetComponent<RectTransform>().sizeDelta = new Vector3(0, 0, (1.5f * recordAll.Count - 0.5f) * hightR);
            record.transform.parent = content.transform;
            record.transform.position = new Vector2(canvas.transform.position.x, canvas.transform.position.y);
            record.GetComponent<RectTransform>().sizeDelta = new Vector2(widthR, hightR);
        }

        float index = 0;
        for (int i = m; i < dialogue[current].Length; i++)
        {
            GameObject textHolder = new GameObject();
            textAll.Add(textHolder);
            textHolder.AddComponent<Text>();
            Text myText = textHolder.GetComponent<Text>();
            myText.color = Color.white;
            myText.text = dialogue[current][i];
            myText.fontSize = 20;
            myText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            textHolder.transform.parent = canvas.transform;
            textHolder.transform.position = new Vector2(canvas.transform.position.x  + 483, canvas.transform.position.y - 180 - 1.5f * height * index);
            textHolder.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
            index++;
        }
        current++;
    }

    // Update is called once per frame
    void Update()
    {
        if(isTexting)
        {
            if(timeKeeper >= textAll.Count * eachTime)
            {
                /*for (int i = 0; i < dialogue[current].Length; i++)
                {
                    GameObject record = new GameObject();
                    recordAll.Add(record);
                    record.AddComponent<Text>();
                    Text rText = record.GetComponent<Text>();
                    rText.text = dialogue[current][i];
                    rText.fontSize = 35;
                    rText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
                    content.GetComponent<RectTransform>().sizeDelta = new Vector3(0, 0, (1.5f * recordAll.Count - 0.5f) * hightR);
                    record.transform.parent = content.transform;
                    record.transform.position = new Vector2(canvas.transform.position.x, canvas.transform.position.y);
                    record.GetComponent<RectTransform>().sizeDelta = new Vector2(widthR, hightR);
                }*/
                foreach (GameObject text in textAll)
                {
                    Destroy(text);
                }
                textAll.Clear();
                isTexting = false;
                timeKeeper = 0;
            }
            timeKeeper += Time.deltaTime;
        }
    }
}
