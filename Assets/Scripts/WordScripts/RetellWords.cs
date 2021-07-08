using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetellWords : MonoBehaviour
{
    public SelectionButton[] selections;
    public static Word formalWord;//当前单词
    public static int continuous = 0;

    public void GetButton()
    {
        selections = GetComponentsInChildren<SelectionButton>();
    }

    //更新单词
    [System.Obsolete]
    public void UpdateWords()
    {
        //改变WordButton的图片、包含的数据
        bool correctness = true;
        int m = Random.Range(0,3);
        for(int i = 0;i < 3;i++)
        {
            selections[m].selection.content = formalWord.lackletter[i];
            if(correctness)
            {
                selections[m].selection.correctness = true;
                correctness = false;
            }
            else
                selections[m].selection.correctness = false;

            m = ( m + 1 )% 4;
        }

    }

    
}
