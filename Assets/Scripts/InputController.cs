using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public Camera camera;
    private SkillRelease release;
    private SkillSet set;
    private Skill skillSelected;//当前选中的技能 
    private Hero player;
    private MapScript mapScript;
    private Vector3 mousePositionOnScreen;
    private Vector3 mouseWorldPosition;
    private float x;
    private float y;
    private static bool selectable;//是否可以选择方块，即系统是否位于UI层
    private int mode = 0;//0-未选择技能，1-选格子，2-确认格子
    public int energyRemained = 0;//本回合剩余的能量
    




    public void Start()
    {
        player = new Hero(name);
        release = new SkillRelease(player);
        mapScript = new MapScript();
        set = new SkillSet(player.name);//读取人物的技能表
    }

    public void Update()
    {
        MouseFlow();
        MouseClick();
        RayCheck();
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
        if(!selectable)
            return;

        if(Input.GetMouseButtonDown(0))
        {
            if(mode == 0)
                Debug.Log("请选择技能");
            else if(mode == 1)
            {
                //Skill
            }
            else if(mode == 2)
            {
            }
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
            SelectGrid(rangeList);
        }

    }

    public Vector3Int SelectGrid(List<Vector3Int> rangeList)
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
        if(Input.GetMouseButton(0)){
        //从摄像机发出到点击坐标的射线
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if(Physics.Raycast(ray,out hitInfo)){
            //划出射线，只有在scene视图中才能看到
            Debug.DrawLine(ray.origin,hitInfo.point);
            GameObject gameObj = hitInfo.collider.gameObject;
            Debug.Log("click object name is " + gameObj.name);
            //当射线碰撞目标为boot类型的物品，执行拾取操作
            if(string.Equals(gameObj.tag,"UI"))
            {
                Debug.Log("pickup!");
            }
        }
    }
    }
}
