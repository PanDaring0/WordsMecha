using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputController : MonoBehaviour
{
    private SkillRelease release;
    private SkillSet set;
    public static int s_skill;//选中的技能
    public static bool skillSetted = false;//技能选择的初始化
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
    private List<Vector3Int> range = new List<Vector3Int>();
    public int energyRemained;//本回合剩余的能量
    public int moveCost = 1;//移动所需能量
    public List<Action> actions;//指令序列
    public Action newAction;

    private int formalAction = 0;//将读取的指令

    public ActionBackground background;
    public SkillButtonManager manager;
    public Text positionText;
    
    void Start()
    {
        player = GetComponent<Hero>(); 
        release = GetComponent<SkillRelease>();
        actions = new List<Action>();
        player.heroName = gameObject.name;

        selectedGrid = new Vector3Int();
        newAction = new Action();

        release.ReleaseStart();
        position = player.position;
        Debug.Log(position);
        release.map.setSkillReleaseRangeHighLight(range);

        set = new SkillSet(gameObject.name);
        AssetBuilder.CreateSkillAsset(set);//从excel读取人物的技能表
        minCost = set.MinCost(player);
        manager.CreatePreButton(set);
        //Test();

        energyRemained = 10;
        //Debug.Log(energyRemained);
    }

    //测试方法
    public void Test()
    {
        Action action = new Action();
        action.pos = player.position;
        for(int i = 0;i<4;i++)
        {
            action.actionNum = i;
            action.actionType = 0;
            action.skillNum = 0;
            action.target = action.pos + new Vector3Int(1,1,0);

            actions.Add(action);
            background.ShowAction(action,set);
            action = new Action();
            action.pos = actions[i].target;
        }
        mode = 3;
    }

    void Update()
    {
        MouseFlow();
        RayCheck();
        SkillSelect();
        TurnEndding();
        if(player.movable)
        {
            MouseClick();
        }
        if(mode == 3)
        {
            UpdateActionRelease();
        }
    }

    public void TurnEndding()
    {
        if(Input.GetKeyUp(KeyCode.Return))
        {
            mode = 3;
        }
        
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
                positionText.text = "(" + position.x.ToString() + "," + position.y.ToString() + ")";
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
                        background.ShowAction(newAction,set);
                    }
                    else if(newAction.actionType == 0)//如果是移动
                    {
                        //下一个初始点变为本次的目标点
                        position = newAction.target;
                        //player.transform.position = newAction.target;
                        Debug.Log(position);
                        energyRemained -= MapScript.disBetweenPosition(newAction.pos,newAction.target)*moveCost;
                        background.ShowAction(newAction,set);
                    }
                    else//结束回合
                    {
                        mode = 3;
                        manager.gameObject.SetActive(false);//隐藏选技能界面
                        newAction.actionType = 2;
                        newAction.skillNum = energyRemained;
                        energyRemained = 0;
                        actions.Add(newAction);
                        return;
                    }
                    mode = 0;
                    newAction.actionNum++;//写入下一条指令

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
                        s_skill = 0;
                        skillSetted = false;
                    }
                }
            }
        }


        if(Input.GetMouseButtonUp(1))
        {
            s_skill = 0;
            mode = 0;
            selectedGrid = new Vector3Int();
        }
    }

    public void SelectMoveGrid()
    {
        selectedGrid = release.map.getCellPosition(mouseWorldPosition);
        newAction.target = selectedGrid;
    }

    //选定技能
    public void SkillSelect()
    {
        if(s_skill!=0&&!skillSetted)
        {
            skillSetted = true;
            Debug.Log(set.skills[s_skill].skillName);
            Debug.Log(energyRemained);
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

        SkillRangeHandle();
        release.map.setSkillReleaseRangeHighLight(range);
        newAction.actionType = 1;
        newAction.skillNum = s_skill;
        newAction.pos = position;
        mode = 1;

        }        
    }

    //输出技能的可选范围
    public void SkillRangeHandle()
    {
        range = release.ReleaseRange(skillSelected);
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
    public void UpdateActionRelease()
    {
        if(player.isMoveReleasing)//如果正在移动、攻击状态
        {
            return;
        }
        else    //可以移动
        {
            if(formalAction > 0)
                background.FinishAction();

            Debug.Log(actions.Count);
            Debug.Log(formalAction);
            if(actions.Count <= formalAction)
            {
                actions = new List<Action>();
                Debug.Log("oop");
                mode = 0;
                s_skill = 0;
                player.movable = false;
            }
            else
            {
                Action action = actions[formalAction];
                if(action.actionType == 0)//移动
                {
                    release.Move(action.target);
                }
                else if(action.actionType == 1)
                {
                    release.SkillHandle(set.skills[action.skillNum],action.target);
                }
                
                //AssetBuilder.SaveToAsset(set,player.heroName);
                formalAction++;
                //Debug.Log(formalAction);

            }
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
            {
                UIselect = false;
            }
        }
    }
}
