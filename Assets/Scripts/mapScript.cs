using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;
/*
保存地图格子和上面的游戏对象
*/
public class MapScript : MonoBehaviour
{
    public int width, height;
    public Tilemap tilemap;
    public MapCellType[,] mapCellTypes = new MapCellType[50, 50];
    public GameObject[,] gameObjectGroup = new GameObject[50,50];
    public Vector3Int heroPoint;
    public Vector3Int[] doorPoints;
    public RoomType roomType;
    public List<GameObject> gameObjectList = new List<GameObject>();
    public HashSet<Vector3Int> DamageHighLight = new HashSet<Vector3Int>();
    public HashSet<Vector3Int> SkillReleaseRangeHighLight = new HashSet<Vector3Int>();
    public Color damageColor, skillReleaseRangeColor;
    public GameObject turnManager;
    public GameObject enemyAi;

    // Start is called before the first frame update
    void Start()
    {
        initMapCells();
        initGameObject();
        turnManager.GetComponent<TurnManager>().initTurnManager();
    }

    // Update is called once per frame
    void Update()
    {
        updateColor();
    }

    public List<Vector3Int> findPath(Vector3Int S,Vector3Int T)
    {
        List<Vector3Int> pathList = new List<Vector3Int>();
        int[,] F = new int[50,50];
        SortedList list = new SortedList(new MyComparer());
        Vector3Int[,] fromPosition = new Vector3Int[50,50];
        List<Vector3Int> dir = new List<Vector3Int>();
        Vector3Int nearT = S;
        dir.Add(Vector3Int.left);
        dir.Add(Vector3Int.right);
        dir.Add(Vector3Int.up);
        dir.Add(Vector3Int.down);
        F[S.x,S.y] = 0;
        list.Add(disBetweenPosition(S, T), S);
        while (list.Count != 0)
        {
            Vector3Int nowP = (Vector3Int)list.GetByIndex(0);
            list.RemoveAt(0);
            if (disBetweenPosition(nearT, T) > disBetweenPosition(nowP, T))
            {
                nearT = nowP;
            }
            foreach (Vector3Int v in dir)
            {
                Vector3Int newP = nowP + v;
                if (Vector3Int.Equals(newP, S))
                {
                    continue;
                }
                if(isPositionInMap(newP) == false)
                {
                    continue;
                }
                if(gameObjectGroup[newP.x,newP.y] != null)
                {
                    continue;
                }
                if(getMapType(newP) == MapCellType.obstacle)
                {
                    continue;
                }
                if (F[newP.x, newP.y] == 0 || F[nowP.x, nowP.y] + 1 < F[newP.x, newP.y])
                {
                    F[newP.x, newP.y] = F[nowP.x, nowP.y] + 1;
                    fromPosition[newP.x, newP.y] = nowP;
                    list.Add(disBetweenPosition(newP, T) + F[newP.x, newP.y], newP);
                }
            }
        }
        list.Clear();
        for(Vector3Int v = nearT;v!= Vector3Int.zero;v = fromPosition[v.x, v.y])
        {
            pathList.Add(v);
        }
        pathList.Reverse();
        return pathList;
    }

    public void updateColor()
    {
        for(int i = 1; i <= width; i++)
        {
            for(int j = 1; j <= height; j++)
            {
                Vector3Int v = new Vector3Int(i, j, 0);
                if (DamageHighLight.Contains(v - getMouseCellPosition()))
                {
                    setCellColor(v, damageColor);
                }
                else if (SkillReleaseRangeHighLight.Contains(v - heroPoint))
                {
                    setCellColor(v, skillReleaseRangeColor);
                }
                else
                {
                    setCellColor(v, Color.white);
                }
            }
        }
    }

    public Vector3Int getMouseCellPosition()
    {
        Vector3 v = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return getCellPosition(v);
    }

    public void setDamageHighLight(List<Vector3Int> positionList)
    {
        DamageHighLight.Clear();
        foreach(Vector3Int position in positionList)
        {
            DamageHighLight.Add(position);
        }
    }

    public void setSkillReleaseRangeHighLight(List<Vector3Int> positionList)
    {
        SkillReleaseRangeHighLight.Clear();
        foreach (Vector3Int position in positionList)
        {
            SkillReleaseRangeHighLight.Add(position);
        }
    }

    public void setCellColor(Vector3Int position,Color color)
    {
        if (isPositionInMap(position))
        {
            Tile tile = (Tile)tilemap.GetTile(position);
            tile.color = color;
            tilemap.RefreshTile(position);
            //Debug.Log(tilemap.GetColor(position));
            tile.color = Color.white;
        }
    }

    public bool instantiateInCell(Vector3Int position,GameObject prefab)
    {
        return false;
    }

    public static int disBetweenPosition(Vector3Int A,Vector3Int B)
    {
        return Math.Abs(A.x - B.x) + Math.Abs(A.y - B.y);
    }

    public void setCellGameObject(Vector3Int position,GameObject gameObject)
    {
        gameObjectGroup[position.x, position.y] = gameObject;
        gameObject.transform.position = position;
    }

    public List<GameObject> getGameObjectList(List<Vector3Int> positionList)
    {
        List<GameObject> gameObjectListHere = new List<GameObject>();
        foreach (Vector3Int k in positionList)
        {
            gameObjectListHere.Add(getCellGameObject(k));
        }
        return gameObjectListHere;
    }

    public GameObject getCellGameObject(Vector3Int position)
    {
        if(isPositionInMap(position) == false)
        {
            return null;
        }
        return gameObjectGroup[position.x, position.y];
    }

    public Vector3Int getCellPosition(Vector3 worldPosition)
    {
        Vector3Int position = tilemap.WorldToCell(worldPosition);
        if (isPositionInMap(position) == false)
        {
            return new Vector3Int(-1, -1, -1);
        }
        return position;
    }

    public Vector3 getCellCenter(Vector3Int position)
    {
        return tilemap.GetCellCenterWorld(position);
    }

    public void initGameObject()
    {
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject gobj in enemys)
        {
            gameObjectList.Add(gobj);
            Vector3Int position = getCellPosition(gobj.transform.position);
            gameObjectGroup[position.x, position.y] = gobj;
        }
        GameObject hero = GameObject.FindGameObjectWithTag("Hero");
        gameObjectList.Add(hero);
        Vector3Int heroPosition = getCellPosition(hero.transform.position);
        //Debug.Log(heroPosition);
        //gameObjectGroup[8, 5] = hero;
        //Debug.Log(gameObjectGroup[8, 8]);
        gameObjectGroup[heroPosition.x, heroPosition.y] = hero;
        heroPoint = heroPosition;
    }

    public MapCellType getMapType(Vector3Int v)
    {
        return mapCellTypes[v.x, v.y];
    }

    public bool isPositionInMap(Vector3Int position)
    {
        if (position.x > width || position.x < 1 || position.y > height || position.y < 1)
            return false;
        else return true;
    }

    public void initMapCells()//根据Tile的类型初始化地面类型，分为地面，墙壁，陷阱，之后需要修改tile名称
    {
        for (int i = 1; i <= width; i++)
        {
            for (int j = 1; j <= height; j++)
            {
                TileBase tile = tilemap.GetTile(new Vector3Int(i, j, 0));
                if (tile != null)
                {
                    if (tile.name == "test_0")
                    {
                        mapCellTypes[i, j] = MapCellType.obstacle;
                    }
                    else if (tile.name == "test_1")
                    {
                        mapCellTypes[i, j] = MapCellType.ground;
                    }
                    else if (tile.name == "test_3")
                    {
                        mapCellTypes[i, j] = MapCellType.trap;
                    }
                }
            }
        }
        /*for (int i = 1; i <= width; i++)
        {
            for (int j = 1; j <= height; j++)
            {
                Debug.Log(mapTypes[i, j]);
            }
        }*/
    }
}
