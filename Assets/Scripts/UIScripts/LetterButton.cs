using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LetterButton : MonoBehaviour
{
    public string lackletter;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(SelectLetter);
    }

    public void SelectLetter()
    {
        
    }
}
