﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInitScript : MonoBehaviour
{
    public int sceneLevel;
    public int sceneNumInLevel;
    // Start is called before the first frame update
    void Start()
    {
        SceneManaging.setSceneManager(sceneLevel, sceneNumInLevel);
        SceneManaging.initScenes();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
