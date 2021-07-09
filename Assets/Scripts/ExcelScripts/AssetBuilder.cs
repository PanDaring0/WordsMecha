using UnityEditor;
using UnityEngine;
using System.IO;
using OfficeOpenXml;
using System.Collections.Generic;
using System.Reflection;

public class AssetBuilder : MonoBehaviour
    {
        FieldInfo[] itemFieldInfoArray;//表数据对应类的反射（注意：FieldInfo和PropertyInfo）
        
        public static void CreateSkillAsset(SkillSet set)
        {
            set.skills = set.SkillList(SkillSet.excelsFolderPath);
            
            if(!Directory.Exists(SkillSet.assetPath))
            {
                Directory.CreateDirectory(SkillSet.assetPath);
            }

            string assetPath = string.Format("{0}{1}.asset",SkillSet.assetPath,set.excelName);
            AssetDatabase.CreateAsset(set,assetPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();         
        }

        //写入xlsx文件，注意excel文件的后缀必须为.xlsx，若为.xls则无法读取到Workbook.Worksheets
        public void WriteToExcel(SkillSet set)
        {
            SaveToAsset(set);
    
            string path = SkillSet.excelsFolderPath + set.excelName + ".xlsm";
            FileInfo xlsxFile = new FileInfo(path);
    
            if (xlsxFile.Exists)
            {
                //通过ExcelPackage打开文件
                using (ExcelPackage package = new ExcelPackage(xlsxFile))
                {
                    //修改excel的第一个sheet，下标从1开始
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
    
                    for (int i = 1; i < set.totalNum; i++)
                    {
                        {//下标从1开始，第一行因为是描述信息所以是selectedIndex + 2
                            worksheet.Cells[i + 1, 0].Value = set.skills[i];
                            
                        
                        }
                    }
                    package.Save();
                    Debug.Log("WriteToExcel Success");
                }
            }
        }


        //修改的数据保存到Asset文件中
        public static void SaveToAsset(SkillSet set)
        {
            for(int i = 0; i < set.totalNum;i++)
            {
                SkillManager.Instance.skillArray[i] = GetNewSkill();
            }
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log("SaveToAsset Success");
        }

        public static Skill GetNewSkill()
        {
            Skill skill = new Skill();
            /*for (int i = 0; i < itemFieldInfoArray.Length; i++)
            {
                var v = dataValues[i];
                itemFieldInfoArray[i].SetValue(skill, v);
            }*/
            return skill;
        }

    }