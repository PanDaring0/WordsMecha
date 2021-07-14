using UnityEngine;
using Excel;
using System.IO;
using System.Data;

public class WordRead : ScriptableObject
{
    public int wordsNum = 6;
    public static string folderPath = Application.dataPath + "/Resources/Excels/Words/";

    public string excelName = "Words";//需要打开的文件名(不含后缀)

    public static string savingPath = "Assets/Resources/WordExcels/";
    public Word[] wordList;
    
    void Start()
    {
    }

    public void ReadExcelWords()
    {
        int columnNum = 0, rowNum = 0;
        DataRowCollection collect = ReadExcel(folderPath + excelName + ".xlsm", ref columnNum, ref rowNum);
        
        Word[] words = new Word[rowNum-1];
        int num = 0;
        string full,rem,lack0,lack1,lack2,lack3,mea;
        for(int i = 1;i<wordsNum;i++)
        {
            num = int.Parse(collect[i][0].ToString());
            full = collect[i][1].ToString();
            rem = collect[i][2].ToString();
            lack0 = collect[i][3].ToString();
            lack1 = collect[i][4].ToString();
            lack2 = collect[i][5].ToString();
            lack3 = collect[i][6].ToString();
            mea = collect[i][7].ToString();

            Word newWord = new Word(num,full,rem,lack0,lack1,lack2,lack3,mea);
            words[i-1] = newWord;
        }
        wordList = words;
    }
    
    static DataRowCollection ReadExcel(string filePath, ref int columnNum, ref int rowNum) 
    {
        FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
 
        DataSet result = excelReader.AsDataSet();
        //Tables[0] 下标0表示excel文件中第一张表的数据
        columnNum = result.Tables[0].Columns.Count;
        rowNum = result.Tables[0].Rows.Count;
        return result.Tables[0].Rows;
    }

}