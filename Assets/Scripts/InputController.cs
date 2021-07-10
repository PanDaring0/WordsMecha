using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private SkillRelease release;
    private SkillSet set;
    public static int s_skill;//选中的技能
    public Skill skillSelected;//当前选中的技能 
    private Hero player;
    private Vector3 mousePositionOnScreen;
    private Vector3 mouseWorldPosition;
    private float x;//鼠标的世界坐标x
    private float y;
    private static bool UIselect;//是否可以选择方块，即系统是否位于UI层
    public static int mode = 0;//0-未选择技能，1-选格子，2-确认格子，3-清单生成完毕
    public int minCost;//技能中最低消耗

    private Vector3Int selectedGrid;
    private Vector3Int position;
    private List<Vector3Int> range;
    public int energyRemained = 0;//本回合剩余的能量
    public int moveCost = 1;//移动所需能量
    public List<Action> actions;//指令序列
    public Action newAction;

    public ActionBackground background;
    
    void Start()
    {
        player = GetComponent<Hero>(); 
        release = GetComponent<SkillRelease>();
        actions = new List<Action>();

        selectedGrid = new Vector3Int();
        newAction = new Action();

        release.ReleaseStart();
        position = release.map.heroPoint;
        Debug.Log(position);

        set = new SkillSet(player.name);//读取人物的技能表
        AssetBuilder.CreateSkillAsset(set);
        minCost = set.MinCost(player);
        Test();
        Debug.Log(position);
    }

    //测试方法
    public void Test()
    {
        Action action = new Action();
        action.actionNum = 0;
        action.actionType = 0;
        action.skillNum = 0;
        action.pos = player.position;
        for(int i = 0;i<4;i++)
        {
            action.target = action.pos + new Vector3Int(1,1,1);
            actions.Add(action);
            action.pos = action.target;
        }
        //移动的处理
        ActionRelease();
    }

    void Update()
    {
        MouseFlow();
        RayCheck();
        if(player.movable)
        {
            MouseClick();
        }
        End();
    }

    //鼠标坐标获取
    public void MouseFlow()
    {
        mousePositionOnScreen = Input.mousePosition;
        mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePositionOnScreen);
        x = mouseWorldPosition.x;
        y = mouseWorldPosition.y;
    }

    //鼠标点击事件的综合处理
    public void MouseClick()
    {
        if(UIselect)
            return;

        if(Input.GetMouseButtonUp(0))
        {
            if(mode == 0)//未选技能
            {
                SelectMoveGrid();
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

                    if(newAction.actionType == 1)//如果是技能
                    {
                        //位移技能使下一个初始点移动
                        release.SkillMove(position,newAction.target);

                        skillSelected = null;
                        s_skill = 0;
                        energyRemained -= set.skills[newAction.skillNum].skillCost;
                        set.skills[newAction.skillNum].skillRemained--;
                    }
                    else if(newAction.actionType == 0)//如果是移动
                    {
                        //下一个初始点变为本次的目标点
                        position = newAction.target;
                        energyRemained -= MapScript.disBetweenPosition(newAction.pos,newAction.target)*moveCost;
                    }
                    else//结束回合
                    {
                        player.def += energyRemained;
                        energyRemained = 0;
                        mode = 3;
                    }
                    mode = 0;
                    newAction.actionNum++;//读取下一条指令

                }
                else
                {
                    if(newAction.actionType == 0)//移动
                    {
                        mode = 0;
                        s_skill = 0;
                    }
                    
                    else//技能
                    {
                        mode = 1;
                        Debug.Log("reselect");
                    }
                }
            }
        }

    }

    public void SelectMoveGrid()
    {
        selectedGrid = release.map.getCellPosition(mouseWorldPosition);
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
        Vector3Int select = release.map.getCellPosition(mouseWorldPosition);
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
        if(selectedGrid == release.map.getCellPosition(mouseWorldPosition))
            return true;
        else
            return false;
    }

    //读取列表，施放技能
    public void ActionRelease()
    {
        for(int i = 0 ; i < actions.Count ;i++)
        {
            if(actions[i].actionType == 0)//移动
            {
                release.Move(actions[i].target);

            }
            else if(actions[i].actionType == 1)
            {
                release.SkillHandle(set.skills[actions[i].skillNum],actions[i].target);
            }
        }
        AssetBuilder.SaveToAsset(set,player.name);
        player.movable = false;
    }

    public void End()
    {
        if(mode == 3)
        {
            ActionRelease();  
        }  
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
                //Debug.Log("UI");
                UIselect = true;
            }
            else
                UIselect = false;
        }
    }
}
