using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DebugScript : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase tile;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(tilemap);
        //Debug.Log(tilemap.GetSprite(new Vector3Int(-1,-1,0)));
        //tilemap.SetTile(new Vector3Int(0, 0, 0), tile);
        //Debug.Log(tilemap.GetTile(new Vector3Int(0, 0, 0)));
        //Debug.Log(tilemap.tileAnchor);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
