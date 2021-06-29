using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff
{
    int bufftype;
    int bufftime;
    int buffimpact;
    public Buff(int type,int time,int impact)
    {
        bufftype = type;
        bufftime = time;
        buffimpact = impact;
    }
}
