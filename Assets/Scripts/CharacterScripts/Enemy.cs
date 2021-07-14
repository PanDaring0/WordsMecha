using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public SkillSet skillSet;
    public SkillRelease skillRelease;
    public SortedList posiblePositionList = new SortedList(new MyComparer());
    public Vector3Int heroPoint;

    public void TakeActions()
    {
        Escape();
        movable = false;
    }

    public void Escape()
    {
        posiblePositionList.Clear();
        heroPoint = mapScript.heroPoint;
        if (MapScript.disBetweenPosition(position, heroPoint) < 3)
        {
            for (int i = 0; i <= 3; i++)
            {
                Vector3Int[] v = new Vector3Int[4];
                v[0] = new Vector3Int(heroPoint.x + i, heroPoint.y + 3 - i, 0);
                v[1] = new Vector3Int(heroPoint.x + i, heroPoint.y - 3 + i, 0);
                v[2] = new Vector3Int(heroPoint.x - i, heroPoint.y + 3 - i, 0);
                v[3] = new Vector3Int(heroPoint.x - i, heroPoint.y - 3 + i, 0);
                for (int j = 0; j < 4; j++)
                {
                    if (mapScript.isPositionInMap(v[j]) && mapScript.gameObjectGroup[v[j].x, v[j].y] == null)
                    {
                        posiblePositionList.Add(MapScript.disBetweenPosition(v[j], position), v[j]);
                    }
                }
            }
        }
        if (posiblePositionList.Count != 0)
        {
            Move((Vector3Int)posiblePositionList.GetByIndex(0));
        }
        skillRelease.SkillHandle(skillSet.skills[2], heroPoint);
    }


    public void Chase()
    {
        
    }

    public void Start()
    {
        mapScript = map.GetComponent<MapScript>();
        position = mapScript.getCellPosition(transform.position);
        animator = GetComponent<Animator>();
        transShouldBe = transform.position;
        skillSet = new SkillSet("Enemy");
        skillRelease = GetComponent<SkillRelease>();
        skillRelease.ReleaseStart();
        skillSet.skills = skillSet.SkillList(SkillSet.excelsFolderPath);
    }

    public void selectActionThisTurn()
    {

    }
}
