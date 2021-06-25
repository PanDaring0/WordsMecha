using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMap
{
    public static Scene[] Scenes;
    public static int sceneLevel;
    public static int sceneNumInLevel;
    public static int[,] sceneId = new int[30, 30];
    public static SortedList sortedList = new SortedList();
    public static Vector2Int[] scenePoint = new Vector2Int[15];
    public static Vector2Int 
    east = new Vector2Int(1,0), 
    west = new Vector2Int(-1,0), 
    north = new Vector2Int(0,1), 
    south = new Vector2Int(0,-1);
    public static HashSet<Vector2Int> directionGroup = new HashSet<Vector2Int>();

    public static void setSceneMap(int L,int N)
    {
        sceneLevel = L;
        sceneNumInLevel = N;
        directionGroup.Clear();
        directionGroup.Add(east);
        directionGroup.Add(west);
        directionGroup.Add(north);
        directionGroup.Add(south);
    }

    public static void creatSceneMap()
    {
        System.Array.Clear(sceneId, 0, sceneId.Length);
        sortedList.Add(Random.Range(0, 1000),new Vector2Int(15,15));
        for(int i = 1; i <= 12; i++)
        {
            Vector2Int v = (Vector2Int)sortedList.GetByIndex(0);
            sceneId[v.x, v.y] = i;
            scenePoint[i] = new Vector2Int(v.x, v.y);
            for(int j = 1; j <= i; j++)
            {
                foreach(Vector2Int k in directionGroup)
                {
                    if(sceneId[v.x,v.y] == 0)
                    {
                        sortedList.Add(Random.Range(0,1000),new Vector2Int(v.x, v.y));
                    }
                }
            }
        }
    }

}
