using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charactor : MonoBehaviour
{
    public int health = 100;
    public int atk = 10;
    public int def = 10;
    public SkillRelease release;
    public MapScript mapScript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public bool SelectConfirm(List<GameObject> list,Vector3Int vector3Int)
    {
        foreach (GameObject item in list)
        {
            if(item.GetComponent<Enemy>().position==vector3Int)
                return true;
        }
        return false;
    }
    public void SelectDirection()
    {

    }
}
