using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionButton : MonoBehaviour
{
    public Selection selection;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(Select);

        selection = new Selection();
        selection.content = "l";
        selection.correctness = true;
        gameObject.GetComponentInChildren<Text>().text = selection.content;
    }

    public void Select()
    {
        if(selection.correctness)
        {
            if(RetellWords.continuous > 3)
                SkillRelease.damageAffect = 1.5f;
            else
                SkillRelease.damageAffect = 1f;

            
            RetellWords.continuous++;    
        }
        else
        {
            SkillRelease.damageAffect = 0.5f;
            RetellWords.continuous = 0;
        }
    }
}
