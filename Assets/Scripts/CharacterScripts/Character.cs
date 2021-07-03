using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Vector3Int position;
    public int health = 100;
    public int atk = 10;
    public int def = 10;
    public int status = 0;//控制状态，0-正常，1-眩晕
    public List<Buff> bufflist;
    public SkillRelease release;
    public MapScript mapScript;

}
