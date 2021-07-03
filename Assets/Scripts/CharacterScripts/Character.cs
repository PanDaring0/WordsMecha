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
    public List<Buff> bufflist;
    public SkillRelease release;
    public MapScript mapScript;
    public GameObject map;

    public bool moveable;//是否结束行动

    void Start()
    {
        mapScript = map.GetComponent<MapScript>();
    }

    public bool move(Vector3Int position)
    {
        if(mapScript.gameObjectGroup[position.x,position.y] != null)
        {
            return false;
        }
        if(mapScript.mapCellTypes[position.x,position.y] == MapCellType.obstacle)
        {
            return false;
        }
        Vector3Int nowPosition = mapScript.getCellPosition(transform.position);
        mapScript.gameObjectGroup[nowPosition.x, nowPosition.y] = null;
        mapScript.gameObjectGroup[position.x, position.y] = this.gameObject;
        if (string.Equals(this.tag,"Hero"))
        {
            mapScript.heroPoint = position;
        }

        return true;
    }
}
