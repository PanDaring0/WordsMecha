

[System.Serializable]
public class Word
{
    public int wordNum;//编号
    public string fullWord;
    public string[] lackletter = new string[4];//存储四个选项的字母，其中lackletter[0]为真

    public string remained;
    public string meaning;

    public Word()   
    {
        
    }
    public Word(int num,string full,string rem,string lack0,string lack1,string lack2,string lack3,string mea)
    {
        wordNum = num;
        fullWord = full;
        lackletter[0] = lack0;
        lackletter[1] = lack1;
        lackletter[2] = lack2;
        lackletter[3] = lack3;
        meaning = mea;
    }
}

