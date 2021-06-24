using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorScript : MonoBehaviour
{
    public bool isClosed;
    public Scene sceneBehindDoor;
    public SpriteRenderer spriteRenderer;

    public void openDoor()
    {
        isClosed = false;
        //更换sprite
    }

    public void closeDoor()
    {
        isClosed = true;
        //更换sprite
    }

    // Start is called before the first frame update
    void Start()
    {
        isClosed = true;
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
