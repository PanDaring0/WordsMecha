using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public TurnStatus turnStatus = TurnStatus.heroTurn;
    public GameObject mapGameObject;
    public List<GameObject> gameObjectList;
    public int heroAndEnemyNum;

    public bool IsHeroTurn()
    {
        return turnStatus == TurnStatus.heroTurn;
    }

    public void ChangerTurn()
    {
        if(turnStatus == TurnStatus.heroTurn)
        {
            turnStatus = TurnStatus.enemyTurn;
        }
        else
        {
            turnStatus = TurnStatus.heroTurn;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        heroAndEnemyNum = 0;
        turnStatus = TurnStatus.heroTurn;
        gameObjectList = mapGameObject.GetComponent<MapScript>().gameObjectList;
        foreach(GameObject k in gameObjectList)
        {
            if(string.Equals(k.tag,"Hero"))
            {
                //设为回合内
                heroAndEnemyNum++;
            }
            else if(string.Equals(k.tag, "Enemy"))
            {
                //设为回合外
                heroAndEnemyNum++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject k in gameObjectList)
        {
            if (false)
            {

            }
        }
    }
}
