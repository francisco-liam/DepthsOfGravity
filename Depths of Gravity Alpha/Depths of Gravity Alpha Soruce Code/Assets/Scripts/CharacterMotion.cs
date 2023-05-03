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

        bool isRunningBackLeft = animator.GetBool("isRunningBackLeft");

        bool isRunningBackRight = animator.GetBool("isRunningBackLeft");


        //run forward
        if (!isRunningForward && forwardPressed)
        {
            animator.SetBool("isRunningForward", true);
        }
        if(isRunningForward && !forwardPressed)
        {
            animator.SetBool("isRunningForward", false);
        }

        //run left
        if(isRunningForward && leftPressed)
        {
            animator.SetBool("isRunningRight", true);
        }
        if (isRunningForward && !leftPressed)
        {
            animator.SetBool("isRunningRight", false);
        }

        //run right
        if (isRunningForward && rightPressed)
        {
            animator.SetBool("isRunningLeft", true);
        }
        if (isRunningForward && !rightPressed)
        {
            animator.SetBool("isRunningLeft", false);
        }

        //run backwards
        if(!isRunningBackwards && backwardsPressed)
        {
            animator.SetBool("isRunningBackwards", true);
        }
        if (isRunningBackwards && !backwardsPressed)
        {
            animator.SetBool("isRunningBackwards", false);
        }

        //run backwards left
        if(isRunningBackwards && leftPressed)
        {
            animator.SetBool("isRunningBackLeft", true);
        }
        if(isRunningBackwards && !leftPressed)
        {
            animator.SetBool("isRunningBackLeft", false);
        }

        //run backwards right
        if (isRunningBackwards && rightPressed)
        {
            animator.SetBool("isRunningBackRight", true);
        }
        if (isRunningBackwards && !rightPressed)
        {
            animator.SetBool("isRunningBackRight", false);
        }
    }
}

