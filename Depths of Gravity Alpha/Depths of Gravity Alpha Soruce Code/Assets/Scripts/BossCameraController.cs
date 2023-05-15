using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCameraController : MonoBehaviour
{
    public float backBound;
    public float frontBound;
    public float rightBound;
    public float leftBound;
    public GameObject player;

    public float switchTime;

    private float bottomY = 4.27f;
    private float topY = 10f;
    private float startY;
    bool onGround;
    bool switching;
    float lerpTimer;

    // Start is called before the first frame update
    void Start()
    {
        onGround = true;
        switching = false; 
        lerpTimer = 0.0f;
        startY = 4.27f;
    }

    // Update is called once per frame
    void Update()
    {
        if(switching)
            lerpTimer += Time.deltaTime;

        if(Input.GetKeyUp(KeyCode.LeftShift)) 
        {
            startY = transform.position.y;
            lerpTimer = 0;
            switching = true;
            onGround = !onGround;
        }
        
        float xPos = player.transform.position.x;
        xPos = Mathf.Clamp(xPos, leftBound, rightBound);
        float yPos;
        if (!onGround)
            yPos = Mathf.SmoothStep(startY, topY, lerpTimer / switchTime);
        else
            yPos = Mathf.SmoothStep(startY, bottomY, lerpTimer / switchTime);
        float zPos = player.transform.position.z - 7;
        zPos = Mathf.Clamp(zPos, backBound, frontBound);

        transform.position = new Vector3(xPos, yPos, zPos);

        if(lerpTimer > switchTime)
        {
            lerpTimer = switchTime;
            switching = false;
        }
    }
}
