using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonRestricHeight : MonoBehaviour
{
    private float yRestriction;
    // Start is called before the first frame update
    void Start()
    {
        yRestriction = transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localPosition.y >= yRestriction)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, yRestriction, transform.localPosition.z);
        }

    }
}
