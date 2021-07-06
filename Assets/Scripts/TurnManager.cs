﻿using System.Collections;
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
        foreach(GameObject gobj in gameObjectList)
        {
            if(string.Equals(gobj.tag,"Hero"))
            {
                gobj.GetComponent<Character>().moveable = true;
                heroAndEnemyNum++;
            }
            else if(string.Equals(gobj.tag, "Enemy"))
            {
                gobj.GetComponent<Character>().moveable = false;
                heroAndEnemyNum++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject gobj in gameObjectList)
        {
            if (heroAndEnemyNum == 1)
            {
                return;
            }
            if(gobj.GetComponent<Character>().moveable == true)
            {
                return;
            }
        }
        if (turnStatus == TurnStatus.heroTurn)
        {
            turnStatus = TurnStatus.enemyTurn;
            foreach(GameObject gobj in gameObjectList)
            {
                if (string.Equals(gobj.tag, "Enemy"))
                {
                    gobj.GetComponent<Character>().moveable = true;
                }
            }
        }
        if (turnStatus == TurnStatus.enemyTurn)
        {
            turnStatus = TurnStatus.heroTurn;
            foreach (GameObject gobj in gameObjectList)
            {
                if (string.Equals(gobj.tag, "Hero"))
                {
                    gobj.GetComponent<Character>().moveable = true;
                }
            }
        }
    }
}