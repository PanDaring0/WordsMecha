﻿using System;
using System.IO;
using System.Data;
using UnityEditor;
using UnityEngine;
using Excel;

public class SkillSet
{
    public int totalNum = 0;
    public static string excelFolderPath = Application.dataPath + "/Excels/";
    public string excelName = "";//需要打开的文件名
    public static string savingPath = "Assets/Resources/CharactorExcels/";
    public static string assetPath = "Assets/Resources/DataAssets/";
    public Skill[] skills;

    public SkillSet(string excelName)
    {
        this.excelName = excelName; 
    }
    public void Start()
    {
        skills = SkillList(excelFolderPath);
    }
    public Skill[] SkillList(string filePath)
    {
        int columnNum = 0, rowNum = 0;
        DataRowCollection collect = ReadExcel(filePath, ref columnNum, ref rowNum);

        Skill[] skilllist = new Skill[rowNum -1];
        int num,type,cost,range,maxnum,remained,damage,damageRange,movecount,bufftime,bufftype,buffimpact,ul;
        string name = "";
        for(int i = 0;i<totalNum;i++)
        {
            num = int.Parse(collect[i][0].ToString());
            type = int.Parse(collect[i][2].ToString());
            cost = int.Parse(collect[i][3].ToString());
            name = collect[i][1].ToString();
            range = int.Parse(collect[i][4].ToString());
            maxnum = int.Parse(collect[i][5].ToString());
            remained = int.Parse(collect[i][6].ToString());
            damage = int.Parse(collect[i][7].ToString());
            damageRange = int.Parse(collect[i][8].ToString());
            movecount = int.Parse(collect[i][9].ToString());
            bufftime = int.Parse(collect[i][10].ToString());
            bufftype = int.Parse(collect[i][11].ToString());
            buffimpact = int.Parse(collect[i][12].ToString());
            ul = int.Parse(collect[i][13].ToString());
            Skill skill = new Skill(num,type,name,cost,range,maxnum,remained,damage,damageRange,movecount,bufftype,bufftime,buffimpact,ul);
            skilllist[i] = skill;
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

    public int MinCost()
    {
        int min = 20;
        foreach (Skill skill in skills)
        {
            if(skill.skillCost<min)
                min = skill.skillCost;
        }
        return min;
    }
}
