using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePose : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;

    void Start()
    {

    }
    public void SetPose(int poseNum)
    {
        animator.SetInteger("poseNum", poseNum);
    }
}
