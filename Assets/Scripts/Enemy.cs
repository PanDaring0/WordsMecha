using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Vector3Int position;
    public SkillSet set;
    public int enemyType;
    // Start is called before the first frame update
    void Start()
    {
        set = new SkillSet();
        set.excelName = "EnemyExcel.xlsm";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
