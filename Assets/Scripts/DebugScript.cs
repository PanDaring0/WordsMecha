using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DebugScript : MonoBehaviour
{
    public GameObject map;
    public MapScript mapScript;
    public List<Vector3Int> list = new List<Vector3Int>();
    //public Tilemap tilemap;
    //public TileBase tile;
   // public GameObject gameObject;
    //public Camera camera;
    //public Ray ray;
    // Start is called before the first frame update
    void Start()
    {
        mapScript = map.GetComponent<MapScript>();
        list.Add(new Vector3Int(1, 0, 0));
        list.Add(new Vector3Int(0, -1, 0));
        list.Add(new Vector3Int(-1,0, 0));
        list.Add(new Vector3Int(0, 1, 0));
        list.Add(new Vector3Int(0, 0, 0));
        //mapScript.setDamageHighLight(list);
        //mapScript.setSkillReleaseRangeHighLight(list);
        //List<Vector3Int> pathList = mapScript.findPath(new Vector3Int(15,3,0), new Vector3Int(8,5,0));
        //foreach(Vector3Int v in pathList)
        //{
        //    Debug.Log(v);
        //}
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
