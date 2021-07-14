using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneManaging
{
    //控制场景转换、保存数据
    public static Scene scenes;
    public static int sceneLevel;
    public static int sceneNumInLevel;

    public static void setSceneManager(int L,int N)
    {
        sceneLevel = L;
        sceneNumInLevel = N;
        SceneMap.setSceneMap(N);
    }

    public static void initScenes()
    {
        SceneMap.creatSceneMap();
    }

}
