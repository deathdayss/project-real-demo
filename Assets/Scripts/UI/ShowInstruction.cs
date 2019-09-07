using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowInstruction : MonoBehaviour
{
    public GameObject instruction;
    bool isDisplay = false;
    public void tryIt()
    {
        if(!isDisplay)
        {
            isDisplay = true;
            instruction.SetActive(true);
        } else
        {
            instruction.SetActive(false);
            isDisplay = false;
        }
    }
}
