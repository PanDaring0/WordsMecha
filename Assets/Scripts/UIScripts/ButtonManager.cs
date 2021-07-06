using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    SkillButton[] skillButtons;
    void Start()
    {
        skillButtons = GetComponentsInChildren<SkillButton>();
    }
}
