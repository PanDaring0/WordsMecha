using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapScript : MonoBehaviour
{
    public int width, height;
    public Tilemap tilemap;
    public MapCellType[,] mapCellTypes = new MapCellType[50, 50];
    public GameObject[,] gameObjects = null;
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

    public GameObject getCellGameObject(Vector3Int position)
    {
        return gameObjects[position.x, position.y];
    }

    public Vector3Int getCellPosition(Vector3 worldPosition)
    {
        return tilemap.WorldToCell(worldPosition);
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

    public void initMapCells()
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
