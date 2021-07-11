using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionBackground : MonoBehaviour
{
    public GameObject pre_Action;//指令单元的预制体
    public int actionNum = 0;
    
    
    void Start()
    {
        
    }

    public void ShowAction(Action action,SkillSet set)
    {
        GameObject pre = GameObject.Instantiate(pre_Action);
        pre.transform.parent = this.transform;
        
        foreach (Transform child in pre.transform)
        {
            Debug.Log(child.name);
            child.transform.position = new Vector3(-259.39f,120-50*actionNum,0);
            if(child.name == "actionNum")
                child.GetComponent<Text>().text = action.actionNum.ToString();
            else if(child.name == "Name")
            {
                if(action.actionType == 0)
                    child.GetComponent<Text>().text = "移动";
                else
                    child.GetComponent<Text>().text = set.skills[action.skillNum].skillName;
            }
        }

        actionNum++;
    }



}
