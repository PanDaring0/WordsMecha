using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyComparer : IComparer
{
    public int Compare(object x,object y)
    {
        if ((int)x < (int)y)
        {
            return -1;
        }
        else
        {
            return 1;
        }
        }
}
