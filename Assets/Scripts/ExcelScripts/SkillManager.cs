using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class SkillManager : ScriptableObject
{
    static SkillManager mInstance;
    public static SkillManager Instance
    {
        get
        {
            if (mInstance == null)
            {
                mInstance = Resources.Load<SkillManager>("DataAssets/Skill");
            }
            return mInstance;
        }
    }
    public Skill[] skillArray;
}