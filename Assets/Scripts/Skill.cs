public class Skill
{
    public int skillNum;//技能编号
    public int skillType;//0-攻击,1-属性,2-王牌
    public string skillName;
    public int skillCost,skillMaxNum;
    public int skillRemained;//当前剩余数量
    public int skillRange;//最远距离
    public int skillDamage;
    public int skillDamageRange;//0-四选一，1-某方向三格，2-十字AOE，3-全范围AOE，4-四选一线性2格
    public int moveCount;//当前格与目标格的x、y轴差之和
    public int skillBuffType;//0-无，1-加攻击，2-加防御，3-减攻击，4-减防御，5-眩晕
    public int skillBuffTime;
    public int skillBuffImpact;//buff效果数值
    
    //新建技能
    public Skill(int num,int type,string name,int cost,int range,int maxnum,int remained,int damage,int damagerange,int movecount,int bufftype,int bufftime,int buffimpact)
    {
        skillNum = num;
        skillName = name;
        skillType = type;
        skillCost = cost;
        skillRange = range;
        skillMaxNum = maxnum;
        skillRemained = remained;
        skillDamage = damage;
        skillDamageRange = damagerange;
        moveCount = movecount;
        skillBuffType = bufftype;
        skillBuffTime = bufftime;
        skillBuffImpact = buffimpact;
    }
}
