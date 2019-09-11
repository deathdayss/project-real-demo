using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowInstruction : MonoBehaviour
{
    public GameObject instruction;
    public GameObject others;
    public void tryIt()
    {
        instruction.SetActive(!instruction.activeSelf);
        if(instruction.activeSelf)
        {
            others.SetActive(false);
        }
    }
}
