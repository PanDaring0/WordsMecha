using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    public GameObject map;
    public GameObject turnManager;
    public MapScript mapScript;

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
        
    }
}
