using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : StateMachineBehaviour
{
    private float lastPoseChangeDuration = float.PositiveInfinity;
    private float poseChangeFrequency = 2f;
    private float startPose;
    private float endPose;

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // use hash for variable name
        lastPoseChangeDuration += Time.deltaTime;
        if (lastPoseChangeDuration >= poseChangeFrequency)
        {
            startPose = animator.GetFloat("FinishedPose");
            endPose = Random.Range(0f, 1f);
            if (endPose > 0.5)
                endPose = 1;
            else
                endPose = 0;
            lastPoseChangeDuration = 0;
        }
        // animator.SetFloat("FinishedPose", endPose);
        animator.SetFloat("FinishedPose", Mathf.Lerp(startPose, endPose, lastPoseChangeDuration / poseChangeFrequency * 4));

        // base.OnStateUpdate(animator, stateInfo, layerIndex);
    }
}
