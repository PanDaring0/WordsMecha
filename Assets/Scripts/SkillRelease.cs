using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillRelease : MonoBehaviour
{
    public GameObject user;
    public Skill skill;
    
    //根据技能索引检测技能对应的区域
    public List<Vector3Int> CheckRange(int skillnum)
    {
        List<Vector3Int> vect3list = new List<Vector3Int>();
        if(skill.skillRemained==0)
        {
            Debug.Log("This skill is not enough!");//提示
        }
        else
        {
            switch(skill.skillDamageRange)
            {
                case 1:vect3list.Add(new Vector3Int(1,0,0));
                        vect3list.Add(new Vector3Int(-1,0,0));
                        vect3list.Add(new Vector3Int(0,1,0));
                        vect3list.Add(new Vector3Int(0,-1,0));
                        break;
                case 2:vect3list.Add(new Vector3Int(1,0,0));
                        vect3list.Add(new Vector3Int(-1,0,0));
                        vect3list.Add(new Vector3Int(0,1,0));
                        vect3list.Add(new Vector3Int(0,-1,0));
                        vect3list.Add(new Vector3Int(1,1,0));
                        vect3list.Add(new Vector3Int(-1,1,0));
                        vect3list.Add(new Vector3Int(1,-1,0));
                        vect3list.Add(new Vector3Int(-1,-1,0));
                        break;
                case 3:vect3list.Add(new Vector3Int(1,0,0));
                        vect3list.Add(new Vector3Int(-1,0,0));
                        vect3list.Add(new Vector3Int(0,1,0));
                        vect3list.Add(new Vector3Int(0,-1,0));
                        break;
                case 4:vect3list.Add(new Vector3Int(1,0,0));
                        vect3list.Add(new Vector3Int(-1,0,0));
                        vect3list.Add(new Vector3Int(0,1,0));
                        vect3list.Add(new Vector3Int(0,-1,0));
                        vect3list.Add(new Vector3Int(1,1,0));
                        vect3list.Add(new Vector3Int(-1,1,0));
                        vect3list.Add(new Vector3Int(1,-1,0));
                        vect3list.Add(new Vector3Int(-1,-1,0));
                        break;
                case 5:vect3list.Add(new Vector3Int(1,0,0));
                        vect3list.Add(new Vector3Int(-1,0,0));
                        vect3list.Add(new Vector3Int(0,1,0));
                        vect3list.Add(new Vector3Int(0,-1,0));
                        vect3list.Add(new Vector3Int(2,0,0));
                        vect3list.Add(new Vector3Int(-2,0,0));
                        vect3list.Add(new Vector3Int(0,2,0));
                        vect3list.Add(new Vector3Int(0,-2,0));
                        break;
            }

        }
        return vect3list;
    }


    public void Release(int skillnum,List<GameObject> enemyList)
    {
    }

}
