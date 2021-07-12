using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButtonManager : MonoBehaviour
{
    List<GameObject> skillButtons;
    public GameObject prefab_atk;
    public GameObject prefab_tec;
    public GameObject prefab_Joker;
    //public GameObject prefab_Exit;
    public Hero hero;
    void Start()
    {
        hero = GameObject.FindWithTag("Hero").GetComponent<Hero>();
    }

    //根据主角选定的技能索引生成
    public void CreatePreButton(SkillSet set)
    {
        int activeNum = hero.activeSkills.Length;
        for(int i = 0;i < activeNum;i++)
        {
            GameObject newPre = new GameObject();
            Skill newSkill = set.skills[hero.activeSkills[i]];
            if(newSkill.skillType == 0)
                newPre = GameObject.Instantiate(prefab_atk);
            if(newSkill.skillType == 1)
                newPre = GameObject.Instantiate(prefab_tec);
            if(newSkill.skillType == 2)
                newPre = GameObject.Instantiate(prefab_Joker);
        
            newPre.transform.SetParent(transform);
            newPre.transform.position = new Vector3(-240 + 150*i,-145,0) + transform.position;
            newPre.GetComponentInChildren<Text>().text = set.skills[hero.activeSkills[i]].skillName;
        }
    }
}
