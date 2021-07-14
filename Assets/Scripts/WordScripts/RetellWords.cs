using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RetellWords : MonoBehaviour
{
    public SelectionButton[] selections;
    public Text remainText;
    public static Word formalWord = new Word();//当前单词
    public static int continuous = 0;//正确连击数
    public static WordRead read;


    void Start()
    {
        read = new WordRead();
        read.ReadExcelWords();
        selections = GetComponentsInChildren<SelectionButton>();
        formalWord = read.wordList[0];
    }

    //更新单词
    public void UpdateWords()
    {
        //改变WordButton的图片、包含的数据
        bool correctness = true;
        Debug.Log(formalWord.fullWord);
        remainText.text = formalWord.remained;

        int m = 2;
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

    public void SuccessRetell()
    {
        remainText.text = formalWord.fullWord;
        Wait();
    }

    IEnumerable Wait()
    {
        yield return new WaitForSeconds(1f);
    }
    
}
