using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetellWords : MonoBehaviour
{
    public LetterButton[] words;
    public static Word formalWord;//当前单词
    void Start()
    {
        words = GetComponentsInChildren<LetterButton>();
        //改变WordButton的图片、包含的数据
        foreach (LetterButton word in words)
        {
            
        }
    }

    
}
