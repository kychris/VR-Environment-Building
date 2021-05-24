using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public bool isTriggered = false;
    public bool isAction2 = false;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isTriggered", isTriggered);
        animator.SetBool("isAction2", isAction2);
    }
}
