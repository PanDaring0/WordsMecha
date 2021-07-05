public class Skill
{
    public int skillNum;//技能编号
    public int skillType;//0-攻击,1-属性,2-王牌
    public string skillName;
    public int skillCost,skillMaxNum;
    public int skillRemained;//当前剩余数量
    public int skillRange;//施放距离、曼哈顿距离
    public int skillDamage;
    public int skillDamageRange;//伤害范围
    public int moveCount;//位移距离
    public int skillBuffType;//0-无，1-加攻击，2-加防御，3-减攻击，4-减防御，5-眩晕
    public int skillBuffTime;
    public int skillBuffImpact;//buff效果数值
    public int unlocked;//是否已解锁
    
    //新建技能
    public Skill(int num,int type,string name,int cost,int range,int maxnum,int remained,int damage,int damagerange,int movecount,int bufftype,int bufftime,int buffimpact,int ul)
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
        unlocked = ul;
    }
}
