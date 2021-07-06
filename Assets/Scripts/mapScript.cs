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

    // Start is called before the first frame update
    void Start()
    {
        initMapCells();
        initGameObject();

    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3Int vector = tilemap.WorldToCell(ray.GetPoint(-ray.origin.z / ray.direction.z));
        if (tilemap.HasTile(vector))
        {
            tilemap.SetColor(vector, Color.red);
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
