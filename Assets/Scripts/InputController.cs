﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private SkillRelease release;
    private SkillSet set;
    public static int s_skill;//选中的技能
    public Skill skillSelected;//当前选中的技能 
    private Hero player;
    private MapScript mapScript;
    private Vector3 mousePositionOnScreen;
    private Vector3 mouseWorldPosition;
    private float x;//鼠标的世界坐标x
    private float y;
    private static bool UIselect;//是否可以选择方块，即系统是否位于UI层
    private int mode = 0;//0-未选择技能，1-选格子，2-确认格子
    private Vector3Int selectedGrid;
    private Vector3Int position;
    private List<Vector3Int> range;
    public int energyRemained = 0;//本回合剩余的能量
    public List<Action> actions;//指令序列
    public Action newAction;
    
    public void Start()
    {
        player = new Hero(name);
        release = GetComponent<SkillRelease>();
        mapScript = GameObject.FindWithTag("Map").GetComponent<MapScript>();
        set = new SkillSet(player.name);//读取人物的技能表
        selectedGrid = new Vector3Int();
        newAction = new Action();
        position = mapScript.birthPoint;
    }

    public void Update()
    {
        MouseFlow();
        RayCheck();
        MouseClick();
    }

    //鼠标坐标获取
    public void MouseFlow()
    {
        mousePositionOnScreen = Input.mousePosition;
        mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePositionOnScreen);
        x = mouseWorldPosition.x;
        y = mouseWorldPosition.y;
    }

    public void MouseClick()
    {
        if(UIselect)
            return;

        if(Input.GetMouseButtonUp(0))
        {
            if(mode == 0)//未选技能
            {
                SelectMoveGrid();
                newAction.actionNum++;
                newAction.actionType = 0;//移动
                mode = 2;
            }
            else if(mode == 1)//已选技能
            {
                SelectSkillGrid();   
                //输出范围，在范围内选择
            }
            else if(mode == 2)
            {
                if(GridConfirm())
                {
                    actions.Add(newAction);
                    //添加至命令单
                    if(newAction.actionType == 1)//如果是技能
                    {
                        position = position + (newAction.target-newAction.pos)*skillSelected.moveCount;
                        skillSelected = null;
                        s_skill = 0;
                    }
                    else
                    {
                        position = newAction.target;
                    }

                    mode = 0;

                }
                else
                {
                    if(newAction.actionType == 0)
                        mode = 0;
                    else
                        mode = 1;
                    Debug.Log("reselect");
                }
            }
        }

    }

    public void SelectMoveGrid()
    {
        selectedGrid = mapScript.getCellPosition(mouseWorldPosition);
    }

    //选定技能
    public void SelectSkill()
    {
        if(s_skill!=0)
        {
            //判断技能是否可用
            if(set.skills[s_skill].skillRemained < 0)
            {
                Debug.Log("技能剩余量不足！");
                return;
            }
            if(set.skills[s_skill].skillCost > energyRemained)
            {
                Debug.Log("剩余能量不足！");
                return;
            }
            if(set.skills[s_skill].unlocked == 0)
            {
                Debug.Log("未解锁此技能！");
                return;
            }
        
        //选定的动画效果

        SkillRangeHandle();
        newAction.actionType = 1;
        newAction.skillNum = s_skill;
        newAction.pos = position;
        mode = 1;

        }        
    }

    //输出技能的可选范围
    public void SkillRangeHandle()
    {
        if(skillSelected.skillRange==0)
        {
            range.Add(new Vector3Int(0,0,0));
        }
        else
        {
            range = release.ReleaseRange(skillSelected);
        }
    }

    public void SelectSkillGrid()
    {
        bool inRange = false;
        Vector3Int select = mapScript.getCellPosition(mouseWorldPosition);
        foreach (Vector3Int grid in range)
        {
            if(grid == select)
                inRange = true;
        }
        if(inRange)
        {
            selectedGrid = select;
            newAction.target = selectedGrid;

            mode = 2;
            return;
        }
        else
        {
            Debug.Log("");
        }
        selectedGrid = new Vector3Int(0,0,0);
    }

    //确认选中的格子
    public bool GridConfirm()
    {
        if(selectedGrid == mapScript.getCellPosition(mouseWorldPosition))
            return true;
        else
            return false;
    }

    public void RayCheck()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if(Physics.Raycast(ray,out hitInfo)){
            //划出射线，只有在scene视图中才能看到
            Debug.DrawLine(ray.origin,hitInfo.point);
            GameObject gameObj = hitInfo.collider.gameObject;
            //检测是否为UI
            if(string.Equals(gameObj.tag,"UI"))
            {
                UIselect = true;
            }
            else
                UIselect = false;
        }
    }
}
