using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float backBound;
    public float frontBound;
    public GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float xPos = transform.position.x;
        float yPos = transform.position.y;
        float zPos = player.transform.position.z - 7;
        zPos = Mathf.Clamp(zPos, backBound, frontBound);

        transform.position = new Vector3(xPos, yPos, zPos);
    }
}
