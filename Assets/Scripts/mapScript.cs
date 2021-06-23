using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class mapScript : MonoBehaviour
{
    public int width, height;
    public Tilemap tilemap;
    public MapCellType[,] mapCellTypes = new MapCellType[50,50];
    public GameObject[,] gameObjects;

    public MapCellType getMapType(Vector3Int v)
    {
        return mapCellTypes[v.x, v.y];
    }

    public void initmap()
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
    // Start is called before the first frame update
    void Start()
    {
        initmap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
