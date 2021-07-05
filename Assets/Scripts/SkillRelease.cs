using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillRelease : MonoBehaviour
{
    public Character user;
    public MapScript map;

    public void Start()
    {
        map = GameObject.FindWithTag("Map").GetComponent<MapScript>();
        user = GetComponent<Character>();
    }

    //综合处理
    public void SkillHandle(Skill skill,Vector3Int target)
    {
        List<Vector3Int> damageRange = CheckRange(skill,target);
        List<GameObject> enemys = new List<GameObject>();
        //位移
        SkillMove(user.position,target);
        enemys = map.getGameObjectList(damageRange);
        foreach (GameObject enemy in enemys)
        {
            int damage = Damage(skill,user.atk,enemy.GetComponent<Enemy>().def);
            DamageEnemy(enemy,damage);

            if(skill.skillBuffType>=3)//有debuff效果
            {
                DeBuff(skill,enemy);
            }
        }

        Buff(skill,user);
    }
    
    //施放技能的最大范围
    public List<Vector3Int> ReleaseRange(Skill skill)
    {
        List<Vector3Int> rangelist = new List<Vector3Int>();
        int distance = skill.skillRange;
        Vector3Int zeroposition = new Vector3Int(0,0,0);
        Vector3Int target = new Vector3Int();
        for(int i = -distance;i<=distance;i++)
        {
            target.x = i;
            for(int j = -distance;j<=distance;j++)
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
        List<Vector3Int> damageList = new List<Vector3Int>();
        Vector3Int target = new Vector3Int();
        int distance = skill.skillDamageRange;
        for(int i = -distance;i<=distance;i++)
        {
            target.x = i;
            for(int j = -distance;j<=distance;j++)
            {
                target.y = j;
                if(MapScript.disBetweenPosition(target,user.position)<=distance)
                    damageList.Add(target);
            }
        }
        return damageList;
    }
    
    //位移
    public void Move(Vector3Int target)
    {
        user.position = target;
    }

    //技能位移
    public void SkillMove(Vector3Int position ,Vector3Int target)
    {
        Vector3 vector = new Vector3();
        vector.x = target.x - position.x;
        vector.y = target.y - position.y;
        vector.z = 0;
        vector = Vector3.Normalize(vector);
        position.x = (int)System.Math.Round(vector.x);
        position.y = (int)System.Math.Round(vector.y);
    }

    //造成伤害
    public void DamageEnemy(GameObject enemy,int damage)
    {
        enemy.GetComponent<Character>().health -= damage;
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
    public void DeBuff(Skill skill,GameObject enemy)
    {
        int debufftype = skill.skillBuffType;
        int debufftime = skill.skillBuffTime;
        int debuffimpact = skill.skillBuffImpact;
        if(debufftype == 3)
        {
            enemy.GetComponent<Character>().atk = enemy.GetComponent<Character>().atk - debuffimpact;
            enemy.GetComponent<Character>().bufflist.Add(new Buff(debufftype,debufftime,debuffimpact));
        }
        else if(debufftype == 4)
        {
            enemy.GetComponent<Character>().def = enemy.GetComponent<Character>().def - debuffimpact;
            enemy.GetComponent<Character>().bufflist.Add(new Buff(debufftype,debufftime,debuffimpact)); 
        }
        else if(debufftype == 5)
        {
            enemy.GetComponent<Character>().status = 1;
            enemy.GetComponent<Character>().bufflist.Add(new Buff(debufftype,debufftime,debuffimpact));   
        }
    }

    //buff效果
    public void Buff(Skill skill,Character user)
    {
        int bufftype = skill.skillBuffType;
        int bufftime = skill.skillBuffTime;
        int buffimpact = skill.skillBuffImpact;

        if(bufftype == 1)
        {
            user.atk = user.atk + buffimpact;
            user.bufflist.Add(new Buff(bufftype,bufftime,buffimpact));
        }
        else if(bufftype == 2)
        {
            user.def = user.def + buffimpact;
            user.bufflist.Add(new Buff(bufftype,bufftime,buffimpact));
        }
    }

}
