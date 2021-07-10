using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public SkillSet skillSet;
    public SkillRelease skillRelease;
    public SortedList posiblePositionList;
    public Vector3Int heroPoint;

    public void addPositionToList()
    {

    }

    public void Escape()
    {
        posiblePositionList.Clear();
        heroPoint = mapScript.heroPoint;
        if(MapScript.disBetweenPosition(position,heroPoint) < 3)
        {
            for (int i = 0; i <= 3; i++)
            {
                Vector3Int v1 = new Vector3Int(heroPoint.x + i, heroPoint.y + 3 - i, 0);
                Vector3Int v2 = new Vector3Int(heroPoint.x + i, heroPoint.y - 3 + i, 0);
                Vector3Int v3 = new Vector3Int(heroPoint.x - i, heroPoint.y + 3 - i, 0);
                Vector3Int v4 = new Vector3Int(heroPoint.x - i, heroPoint.y - 3 + i, 0);
                if (mapScript.isPositionInMap(v1))
                {
                }

            }
        }
        skillRelease.SkillHandle(skillSet.skills[3], heroPoint);
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
