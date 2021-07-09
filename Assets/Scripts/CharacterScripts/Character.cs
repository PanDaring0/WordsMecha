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
    public bool done = false;
    public List<Buff> bufflist;//buff的状态清单
    public SkillRelease release;
    public MapScript mapScript;
    public GameObject map;

    public bool movable;//是否结束行动

    void Start()
    {
        mapScript = map.GetComponent<MapScript>();
    }

    public bool move(Vector3Int pos)
    {
        if(mapScript.gameObjectGroup[pos.x,pos.y] != null)
        {
            return false;
        }
        if(mapScript.mapCellTypes[pos.x,pos.y] == MapCellType.obstacle)
        {
            return false;
        }
        mapScript.gameObjectGroup[position.x, position.y] = null;
        mapScript.gameObjectGroup[pos.x, pos.y] = this.gameObject;
        if (string.Equals(this.tag,"Hero"))
        {
            mapScript.heroPoint = pos;
        }
        position = pos;

        return true;
    }
}
