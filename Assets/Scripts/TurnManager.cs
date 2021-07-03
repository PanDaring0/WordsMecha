using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public TurnStatus turnStatus = TurnStatus.heroTurn;
    public GameObject mapGameObject;
    public List<GameObject> gameObjectList;

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
        turnStatus = TurnStatus.heroTurn;
        gameObjectList = mapGameObject.GetComponent<MapScript>().gameObjectList;
        foreach(GameObject k in gameObjectList)
        {
            if(string.Equals(k.tag,"Hero"))
            {

            }
            else if(string.Equals(k.tag, "Enemy"))
            {

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
