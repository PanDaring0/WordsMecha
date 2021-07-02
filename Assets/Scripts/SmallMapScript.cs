using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
/*
小地图，从SceneMap中获取当前层的地图，然后绘制在tilemap上，玩家按M控制小地图的显示和隐藏
*/
public class SmallMapScript : MonoBehaviour
{

    public Tile unknownRoom;
    public Tile fightRoom;
    public Tile eventRoom;
    public Tile shopRoom;
    public Tile horizontalRoad;
    public Tile verticalTile;
    public Tile backGround;
    public int width, height;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 1; i < 30; i++)
        {
            for(int j = 1; j < 30; j++)
            {
                if(SceneMap.sceneId[i,j] != 0 )
                {
                    Tilemap tilemap = GetComponent<Tilemap>();
                    tilemap.SetTile(new Vector3Int(i, j, 0), unknownRoom); 
                    
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("m"))
        {
            TilemapRenderer tilemapRenderer = this.GetComponent<TilemapRenderer>();
            tilemapRenderer.enabled ^= true;
        }
    }
}
