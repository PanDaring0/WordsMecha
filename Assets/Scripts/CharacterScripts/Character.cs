using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public Vector3Int position;
    public int health = 100;
    public int atk = 10;
    public int def = 10;
    public int status = 0;//控制状态，0-正常，1-眩晕
    public bool done = false;
    public List<Buff> bufflist;//buff的状态清单
    public SkillRelease release;
    public MapScript mapScript;
    public GameObject map;
    public bool isAnimatorMoving = false;
    public bool isMoveReleasing = false;
    public float speed;
    public Vector3 transShouldBe;
    public List<Vector3Int> pathList = new List<Vector3Int>();
    public Animator animator;
    public Slider bloodBar;

    public bool movable;//是否结束行动

    void Start()
    {
        mapScript = map.GetComponent<MapScript>();
        position = mapScript.getCellPosition(transform.position);
        animator = GetComponent<Animator>();
        transShouldBe = transform.position;
        bloodBar.value = (float)1.0 * health / 100;
    }

    private void Update()
    {
        if(pathList.Count != 0 && isAnimatorMoving == false)
        {
            MoveSingle(pathList[0]);
            pathList.RemoveAt(0);
        }
        TransFormUpdate();
    }

    public void TransFormUpdate()
    {
        if (Vector3.Distance(transform.position, transShouldBe) > 0.1f)
        {
            transform.position = transform.position + speed * Vector3.Normalize(transShouldBe - transform.position) * Time.deltaTime;
        }
        else
        {
            transform.position = transShouldBe;
            isAnimatorMoving = false;
            animator.SetBool("isWalking", false);
            if (pathList.Count == 0)
            {
                isMoveReleasing = false;
            }
        }
    }
    
    public bool MoveSingle(Vector3Int pos)
    {
        mapScript.gameObjectGroup[position.x, position.y] = null;
        mapScript.gameObjectGroup[pos.x, pos.y] = this.gameObject;
        if (string.Equals(this.tag, "Hero"))
        {
            mapScript.heroPoint = pos;
        }
        isAnimatorMoving = true;
        animator.SetBool("isWalking", true);
        isMoveReleasing = true;
        if ((pos-position).x == 1)
        {
            animator.Play("HeroWalk_R");
        }
        else if((pos-position).x == -1)
        {
            animator.Play("HeroWalk_L");
        }
        else if ((pos - position).y == 1)
        {
            animator.Play("HeroWalk_U");
        }
        else if ((pos - position).y == -1)
        {
            animator.Play("HeroWalk_D");
        }
        position = pos;
        transShouldBe = mapScript.getCellCenter(position);


        return true;
    }

    public bool Move(Vector3Int pos)
    {
        pathList = mapScript.findPath(position, pos);

        return true;
    }
}
