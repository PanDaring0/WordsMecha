﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInitScript : MonoBehaviour
{
    public SceneMap sceneMap;
    public int sceneLevel;
    public int sceneNumInLevel;
    // Start is called before the first frame update
    void Start()
    {
        sceneMap = new SceneMap(sceneLevel,sceneNumInLevel);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
