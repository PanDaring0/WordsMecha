﻿using System.Collections;
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
    public GameObject[,] gameObjectGroup = null;
    public Vector3Int birthPoint;
    public Vector3Int[] doorPoints;
    public RoomType roomType;

    // Start is called before the first frame update
    void Start()
    {
        initMapCells();
        initGameObject();

    }

    // Update is called once per frame
    void Update()
    {
        
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
        List<GameObject> gameObjectList = new List<GameObject>();
        foreach (Vector3Int k in positionList)
        {
            gameObjectList.Add(getCellGameObject(k));
        }
        return gameObjectList;
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
        /*for (int i = 1; i <= width; i++)
        {
            for (int j = 1; j <= height; j++)
            {
                Debug.Log(mapTypes[i, j]);
            }
        }*/
    }
}
