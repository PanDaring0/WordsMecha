using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private SkillRelease release;
    private SkillSet set;
    private Skill skillSelected;//当前选中的技能 
    private Hero player;
    private MapScript mapScript;
    private Vector3 mousePositionOnScreen;
    private Vector3 mouseWorldPosition;
    private float x;//鼠标的世界坐标x
    private float y;
    private static bool UIselect;//是否可以选择方块，即系统是否位于UI层
    private int mode = 0;//0-未选择技能，1-选格子，2-确认格子
    private Vector3 selectedGrid;
    public int energyRemained = 0;//本回合剩余的能量
    
    public void Start()
    {
        player = new Hero(name);
        release = new SkillRelease(player);
        mapScript = new MapScript();
        set = new SkillSet(player.name);//读取人物的技能表
        selectedGrid = new Vector3();
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

        if(Input.GetMouseButtonDown(0))
        {
            if(mode == 0)
            {
                SelectMoveGrid();
                mode = 2;
            }
            else if(mode == 1)
            {
                //Skill
            }
            else if(mode == 2)
            {
                if(GridConfirm())
                {

                }
                else
                {
                    mode = 0;
                    Debug.Log("");
                }
            }
        }

    }

    public void SelectMoveGrid()
    {

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
            SelectSkillGrid(rangeList);
        }

    }

    public Vector3Int SelectSkillGrid(List<Vector3Int> rangeList)
    {
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


    private bool Filed()
    {
        return true;
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
