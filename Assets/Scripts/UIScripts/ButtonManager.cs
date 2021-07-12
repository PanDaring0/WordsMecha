using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    SkillButton[] skillButtons;
    public GameObject prefab_atk;
    public GameObject prefab_tec;
    public GameObject prefab_Joker;
    public GameObject prefab_Exit;
    public GameObject hero;
    void Start()
    {
        
        for(;;)
        {

            //SkillButton newPre = GameObject.Instantiate();

        }
    }

    //根据主角选定的技能索引生成
    private void CreatePreButton(Hero hero)
    {

    }
}
