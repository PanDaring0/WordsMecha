using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillRelease : MonoBehaviour
{
    public Character user;

    public SkillRelease(Character character)
    {
        user = character;
    }
    
    //施放技能的最大范围
    public List<Vector3Int> ReleaseRange(Skill skill)
    {
        List<Vector3Int> rangelist = new List<Vector3Int>();
        int distance = skill.skillRange;
        Vector3Int zeroposition = new Vector3Int(0,0,0);
        Vector3Int target = new Vector3Int();
        for(int i = -distance;i<distance;i++)
        {
            target.x = i;
            for(int j = -distance;j<distance;j++)
            {
                target.y = j;
                if(MapScript.disBetweenPosition(target,zeroposition)<=distance)
                    rangelist.Add(target);
            }
        }
        return rangelist;
    }

    //检测伤害范围
    public List<Vector3Int> CheckRange(Skill skill,Vector3Int centerpoint)
    {
        List<Vector3Int> vect3list = new List<Vector3Int>();
        switch(skill.skillDamageRange)
        {
            case 1:vect3list.Add(new Vector3Int(0,0,0));
                    break;
            case 2:vect3list.Add(new Vector3Int(1,0,0));
                    vect3list.Add(new Vector3Int(-1,0,0));
                    vect3list.Add(new Vector3Int(0,1,0));
                    vect3list.Add(new Vector3Int(0,-1,0));
                    vect3list.Add(new Vector3Int(0,0,0));
                    break;
            case 3:vect3list.Add(new Vector3Int(1,0,0));
                    vect3list.Add(new Vector3Int(-1,0,0));
                    vect3list.Add(new Vector3Int(0,1,0));
                    vect3list.Add(new Vector3Int(0,-1,0));
                    vect3list.Add(new Vector3Int(1,-1,0));
                    vect3list.Add(new Vector3Int(-1,-1,0));
                    vect3list.Add(new Vector3Int(1,1,0));
                    vect3list.Add(new Vector3Int(-1,1,0));
                    vect3list.Add(new Vector3Int(0,0,0));
                    break;
        }
        return vect3list;
    }
    
    //位移
    public void Move(Vector3Int position)
    {
        user.position = position;
    }

    //伤害计算
    public int Damage(Skill skill,int atk,int def)
    {
        int damage = skill.skillDamage;
        if(atk<=def)
            return damage*atk/(atk+def);
        else
            return damage*(atk/def+1);        
    }

    //debuff效果
    public void DeBuff(Skill skill,List<GameObject> enemyList)
    {
        int debufftype = skill.skillBuffType;
        int debufftime = skill.skillBuffTime;
        int debuffimpact = skill.skillBuffImpact;
        if(debufftype == 3)
        {
            foreach (GameObject enemy in enemyList)
            {
                enemy.GetComponent<Character>().atk = enemy.GetComponent<Character>().atk - debuffimpact;
                enemy.GetComponent<Character>().bufflist.Add(new Buff(debufftype,debufftime,debuffimpact));
            }
        }
        else if(debufftype == 4)
        {
            foreach (GameObject enemy in enemyList)
            {
                enemy.GetComponent<Character>().def = enemy.GetComponent<Character>().def - debuffimpact;
                enemy.GetComponent<Character>().bufflist.Add(new Buff(debufftype,debufftime,debuffimpact));
            }    
        }
        else if(debufftype == 5)
        {
            foreach (GameObject enemy in enemyList)
            {
                enemy.GetComponent<Character>().status = 1;
                enemy.GetComponent<Character>().bufflist.Add(new Buff(debufftype,debufftime,debuffimpact));   
            }
        }
    }

    //buff效果
    public void Buff(Skill skill,GameObject user)
    {
        int bufftype = skill.skillBuffType;
        int bufftime = skill.skillBuffTime;
        int buffimpact = skill.skillBuffImpact;

        if(bufftype == 1)
        {
            user.GetComponent<Character>().atk = user.GetComponent<Character>().atk + buffimpact;
            user.GetComponent<Character>().bufflist.Add(new Buff(bufftype,bufftime,buffimpact));
        }
        else if(bufftype == 2)
        {
            user.GetComponent<Character>().def = user.GetComponent<Character>().def + buffimpact;
            user.GetComponent<Character>().bufflist.Add(new Buff(bufftype,bufftime,buffimpact));
        }
    }

}
