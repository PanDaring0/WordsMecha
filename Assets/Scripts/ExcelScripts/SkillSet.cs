using System;
using System.IO;
using System.Data;
using UnityEditor;
using UnityEngine;
using Excel;

[System.Serializable]
public class SkillSet : ScriptableObject
{
    public int totalNum = 12;
    public static string excelsFolderPath = Application.dataPath + "/Resources/Excels/";
    public string excelName = "";//需要打开的文件名(不含后缀)
    public static string savingPath = "Assets/Resources/CharactorExcels/";
    public static string assetPath = "Assets/Resources/DataAssets/";
    public Skill[] skills;
    
    public SkillSet(string name)
    {
        excelName = name;
    }

    public Skill[] SkillList(string filePath)
    {
        int columnNum = 0, rowNum = 0;
        DataRowCollection collect = ReadExcel(filePath + excelName + ".xlsm", ref columnNum, ref rowNum);

        Skill[] skilllist = new Skill[rowNum -1];
        int num,type,cost,range,maxnum,remained,damage,damageRange,movecount,bufftime,bufftype,buffimpact,ul;
        string name = "";
        for(int i = 1;i<totalNum;i++)
        {
            num = int.Parse(collect[i][0].ToString());
            name = collect[i][1].ToString();
            type = int.Parse(collect[i][2].ToString());
            cost = int.Parse(collect[i][3].ToString());
            range = int.Parse(collect[i][6].ToString());
            maxnum = int.Parse(collect[i][4].ToString());
            remained = int.Parse(collect[i][5].ToString());
            damage = int.Parse(collect[i][7].ToString());
            damageRange = int.Parse(collect[i][8].ToString());
            movecount = int.Parse(collect[i][9].ToString());
            bufftime = int.Parse(collect[i][11].ToString());
            bufftype = int.Parse(collect[i][10].ToString());
            buffimpact = int.Parse(collect[i][12].ToString());
            ul = int.Parse(collect[i][13].ToString());
            Skill skill = new Skill(num,type,name,cost,range,maxnum,remained,damage,damageRange,movecount,bufftype,bufftime,buffimpact,ul);
            skilllist[i-1] = skill;
        }
        return skilllist;
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

    public int MinCost(Hero player)
    {
        int min = 20;
        int[] activeSkills = player.GetComponent<Hero>().activeSkills;
        for(int i = 0;i<activeSkills.Length;i++)
        {
            if(skills[activeSkills[i]].skillCost<min)
            {
                min = skills[activeSkills[i]].skillCost;
            }
        }
        return min;
    }
}
