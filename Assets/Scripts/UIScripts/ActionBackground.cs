using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionBackground : MonoBehaviour
{
    public GameObject pre_Action;//指令单元的预制体
    public List<GameObject> actionList = new List<GameObject>();
    public int actionNum = 0;
    
    
    void Start()
    {
    }

    public void ShowAction(Action action,SkillSet set)
    {
        GameObject pre = GameObject.Instantiate(pre_Action);
        pre.transform.SetParent(transform);
        
        pre.transform.position = new Vector3(-259f,120-40*actionNum,0) + transform.position;
        pre.GetComponent<ActionButton>().actionNum.text = (action.actionNum+1).ToString();

        if(action.actionType == 0)
            pre.GetComponent<ActionButton>().name.text = "移动";
        else if(action.actionType == 1)
            pre.GetComponent<ActionButton>().name.text = set.skills[action.skillNum].skillName;

        actionList.Add(pre);
        actionNum++;
    }

    //删去已完成的行动
    public void FinishAction()
    {
        Destroy(actionList[0]);
        actionList.RemoveAt(0);
    }



}
