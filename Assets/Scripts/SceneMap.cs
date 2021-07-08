using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneMap
{
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
    public static bool[,] isGridInSet = new bool[30,30]; 

    public static void setSceneMap(int N)
    {
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
        System.Array.Clear(isGridInSet, 0, sceneId.Length);
        System.Array.Clear(scenePoint, 0, scenePoint.Length);
        sortedList.Add(Random.Range(0, 100000),new Vector2Int(15,15));
        isGridInSet[15, 15] = true;
        for(int i = 1; i <= sceneNumInLevel; i++)
        {
            Vector2Int v = (Vector2Int)sortedList.GetByIndex(0);
            sceneId[v.x, v.y] = i;
            scenePoint[i] = new Vector2Int(v.x, v.y);
            sortedList.RemoveAt(0);
            foreach(Vector2Int k in directionGroup)
            {
                if(isGridInSet[v.x + k.x,v.y+k.y] == false)
                {
                    sortedList.Add(Random.Range(0,100000),new Vector2Int(v.x + k.x, v.y + k.y));
                    isGridInSet[v.x + k.x, v.y + k.y] = true;
                }
            }
        }
    }

}

/*public static class SceneMap
{
    public static int sceneNumInLevel;
    public static void setSceneMap(int N)
    {
        sceneNumInLevel = N;
    }
    public static void creatSceneMap()
    {

    }
}*/
