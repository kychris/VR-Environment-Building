using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudienceAnimationController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }

    [ContextMenu("Change Pose")]
    public void ChangePose()
    {
        print(gameObject.name);

        Animator curAnimator;

        foreach (Transform child in transform)
        {
            curAnimator = child.gameObject.GetComponent<Animator>();
            curAnimator.SetBool("Cheering", !curAnimator.GetBool("Cheering"));
        }
    }


}
