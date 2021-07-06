using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    SkillButton[] skillButtons;
    void Start()
    {
        skillButtons = GetComponentsInChildren<SkillButton>();
        foreach (SkillButton skillbutton in skillButtons)
        {
            string name = skillbutton.gameObject.name;
            Image img = skillbutton.gameObject.GetComponentInChildren<Image>();
        }
    }

    private Texture2D LoadImage(string imageName)
    {
        Texture2D texture2D = Resources.Load(imageName) as Texture2D;
        return texture2D;
    }
}
