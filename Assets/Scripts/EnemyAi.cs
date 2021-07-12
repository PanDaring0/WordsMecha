using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    public GameObject map;
    public GameObject turnManager;
    public MapScript mapScript;
    public List<GameObject> gameObjectList = new List<GameObject>();

    public void initEnemyAi()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        mapScript = map.GetComponent<MapScript>();
    }

    // Update is called once per frame
    void Update()
    {
        gameObjectList = mapScript.gameObjectList;
        foreach(GameObject obj in gameObjectList)
        {
            if(obj.GetComponent<Character>().isMoveReleasing == true)
            {
                return;
            }
        }
        foreach(GameObject obj in gameObjectList)
        {
            if (string.Equals(obj.tag, "Enemy") && obj.GetComponent<Character>().movable == true)
            {
                obj.GetComponent<Enemy>().TakeActions();
                return;
            }
        }
    }
}
