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
    float width = 653;
    float height = 26.2f;
    float widthR = 985.5f;
    float hightR = 43.9f;
    List<string[]> dialogue = new List<string[]>();
    float eachTime = 3;
    int current = 0;
    bool isTexting = false;
    float timeKeeper = 0;

    // Set up dialogues
    public void Start()
    {
        string[] d1 = new string[1];
        //d1[0] = "亚提斯：我是典型冒险者，被派来消灭这个地区的魔物。";
        d1[0] = "Altis: I am a typical adventure. I am sent to eliminate monsters in this area.";
        string[] d2 = new string[3];
        //d2[1] = "维京战士A：北方有一个洞窟，听说那里有强大的魔物，控制着这片区域。";
        d2[1] = "Viking Warrior A: There is a cavern in the north. It said that there are powerful monsters in that place. They control this area.";
        //d2[2] = "亚提斯：你说是那便是，是兄弟就去砍它们。";
        d2[2] = "Altis: That's right. Let's kill them.";
        string[] d3 = new string[3];
        //d3[1] = "弓箭手A：穿过这条湖边的走廊就抵达洞窟，我们部分人已经深入其中了。";
        d3[1] = "Archer A: Go through this corridor along the lake and then arrive in the cavern. Some tropes of ours have gone deep into it.";
        //d3[2] = "亚提斯：（兵力增加了，开心！）";
        d3[2] = "Altis: (More tropes. Happy!)";
        string[] d4 = new string[4];
        //d4[1] = "亚提斯：我日，什么鬼！";
        d4[1] = "Altis: WTF";
        //d4[2] = "弓箭手A：我们的退路被封住了。";
        d4[2] = "Archer A: Our road is blocked";
        //d4[3] = "亚提斯：干！先往里走吧，看看会不会遇到什么特殊事件来解围。";
        d4[3] = "Altis: Damn it！Go head first. Let's see if there will be special event that helps us.";
        string[] d5 = new string[5];
        //d5[1] = "亚提斯：这里人怎么都被缠住了（是要打boss了吗？）。";
        d5[1] = "Altis: Why are people here entangled? (challenge boss?)";
        //d5[2] = "维京战士B：小心！";
        d5[2] = "Viking Warrior B: Be careful！";
        //d5[3] = "藤妖：愚蠢的人类该死！";
        d5[3] = "Vine Spirit: Stupid humans! Die！";
        //d5[4] = "亚提斯：nmsl！";
        d5[4] = "Altis: Fuck you！";
        string[] d6 = new string[9];
        //d6[1] = "亚提斯：这藤蔓砍不断啊，进来的路口被封了，怎么出去？";
        d6[1] = "Altis: The tree vine cannot be cut down. The entrance has been blocked. How to exit?";
        //d6[2] = "维京战士B：这个藤蔓用炸药都炸不破。我们试过了。";
        d6[2] = "Viking Warrior B: This vine cannot be destroyed by bomb. We have tried.";
        //d6[3] = "亚提斯：？？";
        d6[3] = "Altis: What?";
        //d6[4] = "维京战士B：我们之前在洞窟的西边埋了炸药，但没来得及引爆";
        d6[4] = "Viking Warrior C: We set bomb in the west of the cavern but it have not been fired.";
        //d6[5] = "，我们就被藤妖抓住了。我们现在可以去引爆，出洞。我们被抓";
        d6[5] = "Now we can fire the bomb and escape the cavern.";
        //d6[6] = "住期间，听藤妖说她们的老大在南边森林里。我想消灭他之后，";
        d6[6] = "When we were caught, we get the information that the boss of the monsters is in the southern forest from the vine spirit";
        //d6[7] = "这个藤蔓应该就会枯萎。";
        d6[7] = "I think if we kill the boss, the vine will droop.";
        //d6[8] = "亚提斯：你任务都布置的这么明确了，那就干咯。";
        d6[8] = "Altis: Your given task is so clear!";
        string[] d7 = new string[2];
        //d7[1] = "维京战士B：点火。引爆！";
        d7[1] = "Viking Warrior B: Fire the bomb to remove the stones!";
        string[] d8 = new string[2];
        //d8[1] = "维京战士B：啊，树变妖怪了！";
        d8[1] = "Viking Warrior: What? Trees become monsters!";
        string[] d9 = new string[2];
        //d9[1] = "亚提斯：这就是boss吗？也太恶心了吧。";
        d9[1] = "Altis: Is this boss? Too gross!";
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
        getText();
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
        Debug.Log("current is " + current);
        Debug.Log(dialogue.Count);
        Debug.Log("lines are " + dialogue[current].Length);
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
            //content.GetComponent<RectTransform>().sizeDelta = new Vector2(0, (1.5f * recordAll.Count - 0.5f) * hightR);
            //record.transform.parent = content.transform;
            //record.transform.position = new Vector2(content.transform.position.x, content.transform.position.y - 0.5f * hightR);
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
                content.GetComponent<RectTransform>().sizeDelta = new Vector2(0, (1.5f * recordAll.Count - 0.5f) * hightR + 20);
                for (int i = 0; i < recordAll.Count; i++)
                {
                    recordAll[i].transform.parent = content.transform;
                    recordAll[i].transform.position = new Vector2(content.transform.position.x + 0.5f * widthR + 40, content.transform.position.y - (0.5f + 1.5f * i) * hightR - 10);
                }
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
