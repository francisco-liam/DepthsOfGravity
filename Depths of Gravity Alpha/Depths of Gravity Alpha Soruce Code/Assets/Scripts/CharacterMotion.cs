using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMotion : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isRunningForward = animator.GetBool("isRunningForward");
        bool forwardPressed = Input.GetKey(KeyCode.W);

        bool isRunningLeft = animator.GetBool("isRunningLeft");
        bool leftPressed = Input.GetKey(KeyCode.A);

        bool isRunningRight = animator.GetBool("isRunningRight");
        bool rightPressed = Input.GetKey(KeyCode.D);

        bool isRunningBackwards = animator.GetBool("isRunningBackwards");
        bool backwardsPressed = Input.GetKey(KeyCode.S);

        //run forward
        if (!isRunningForward && forwardPressed)
        {
            animator.SetBool("isRunningForward", true);
        }
        if(isRunningForward && !forwardPressed)
        {
            animator.SetBool("isRunningForward", false);
        }

        if(!isRunningLeft && leftPressed)
        {
            animator.SetBool("isRunningForward", true);
        }
        if(isRunningLeft && !leftPressed)
        {
            animator.SetBool("isRunningForward", false);
        }

        if (!isRunningRight && rightPressed)
        {
            animator.SetBool("isRunningForward", true);
        }
        if (isRunningRight && !rightPressed)
        {
            animator.SetBool("isRunningForward", false);
        }
        if (!isRunningBackwards && backwardsPressed)
        {
            animator.SetBool("isRunningForward", true);
        }
        if (isRunningBackwards && !backwardsPressed)
        {
            animator.SetBool("isRunningForward", false);
        }
    }
}

