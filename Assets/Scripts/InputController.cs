using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public Camera camera;
    private SkillRelease release;
    private Skill skillSelected;//当前选中的技能 
    private Character player;
    private MapScript mapScript;
    private Vector3 mousePositionOnScreen;
    private Vector3 mouseWorldPosition;
    public int borderX = 0;
    public int energyRemained = 0;//本回合剩余的能量

    public void Start()
    {
        player = new Character();
        release = new SkillRelease(player);
        mapScript = new MapScript();
    }

    public void Update()
    {
        MouseFlow();
        MouseClick();
    }

    //鼠标坐标获取
    public void MouseFlow()
    {
        mousePositionOnScreen = Input.mousePosition;
        mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePositionOnScreen);
    }

    public void MouseClick()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(!((mouseWorldPosition.x>=-2.8&&mouseWorldPosition.x<=2.8&&mouseWorldPosition.x>=-5&&mouseWorldPosition.x<=-3.5)
            ||true))
                SelectGrid();
        }

    }


    public void SkillHandle()
    {
        List<Vector3Int> rangeList = release.ReleaseRange(skillSelected);
        Vector3Int selectedGrid = new Vector3Int();
        if(skillSelected.skillRange==0)
        {
            selectedGrid.x = 0;
            selectedGrid.y = 0;
        }
        else
        {
            SelectGrid();
        }

    }

    public Vector3Int SelectGrid()
    {
        while(Input.GetMouseButtonDown(0))
        {

        }
        return new Vector3Int(0,0,0);
    }
    public void SelectSkill()
    {
        if(skillSelected.skillRemained < 0)
        {
            Debug.Log("技能剩余量不足！");
            return;
        }
        if(skillSelected.skillCost > energyRemained)
        {
            Debug.Log("剩余能量不足！");
            return;
        }


        //选定的动画效果
    }

    public bool GridConfirm()
    {
        return true;
    }
}
