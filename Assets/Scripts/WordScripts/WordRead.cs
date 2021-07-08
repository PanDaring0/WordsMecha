using UnityEngine;
using Excel;
using System.IO;
using System.Data;

public class WordRead : ScriptableObject
{
    public int wordsNum = 1000;
    public static string folderPath = Application.dataPath + "/Resources/Excels/Words/";

    public string excelName = "";//需要打开的文件名(不含后缀)

    public static string savingPath = "Assets/Resources/WordExcels/";
    public static string assetPath = "Assets/Resources/WordAssets/";

    


}