using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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
        for(int i = 1; i <= width; i++)
        {
            for(int j = 1; j <= height; j++)
            {

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
