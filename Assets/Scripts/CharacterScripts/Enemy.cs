using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public SkillSet skillSet;
    public SkillRelease skillRelease;

    public void Escape()
    {
        skillRelease.SkillHandle(skillSet.skills[3], mapScript.heroPoint);
    }


    public void Chase()
    {
        
    }

    public void Start()
    {
        skillSet = new SkillSet("Enemy");
        skillRelease = GetComponent<SkillRelease>();
        skillRelease.ReleaseStart();
        skillSet.skills = skillSet.SkillList(SkillSet.excelsFolderPath);
    }

    public void selectActionThisTurn()
    {

    }
}
