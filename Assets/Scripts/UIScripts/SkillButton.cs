using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    public int skillNum;
    public void Start()
    {
        GetComponent<Button>().onClick.AddListener(ReturnSkill);
    }

    //返回选中的技能
    public void ReturnSkill()
    {   
        InputController.s_skill = skillNum;
        //切换图片
    }

}
