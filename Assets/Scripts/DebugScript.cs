using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DebugScript : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase tile;
   // public GameObject gameObject;
    //public Camera camera;
    //public Ray ray;
    // Start is called before the first frame update
    void Start()
    {
        //gameObject.GetComponent<Character>().atk = 10;
        //ray = camera.ScreenPointToRay(new Vector3(0, 0, 0));
        //Debug.Log(ray.origin);
        //Debug.Log(ray.direction);
        //Debug.Log(tilemap);
        //Debug.Log(tilemap.GetSprite(new Vector3Int(-1,-1,0)));
        //tilemap.SetTile(new Vector3Int(0, 0, 0), tile);
        //Debug.Log(tilemap.GetTile(new Vector3Int(0, 0, 0)));
        //Debug.Log(tilemap.tileAnchor);
        /*SceneMap.setSceneMap(12);
        SceneMap.creatSceneMap();
        foreach(int k in SceneMap.sceneId)
        {
            if(k != 0)
            {
                Debug.Log(k);
                Debug.Log(SceneMap.scenePoint[k]);
            }
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
