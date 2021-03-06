using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class PushableButton : MonoBehaviour
{

    public float MinLocalY = -4.33f;
    public float MaxLocalY = -4.11f;
    public float smooth = 0.1f;

    public bool isBeingTouched = false;
    public bool isClicked = false;

    public Material greenMat;
    public Material redMat;

    public UnityEvent m_MyEvent;

    private float initX;
    private float initZ;
    // Start is called before the first frame update
    void Start()
    {
        //start by being unpushed
        transform.localPosition = new Vector3(transform.localPosition.x, MaxLocalY, transform.localPosition.z);
        initX = transform.localPosition.x;
        initZ = transform.localPosition.z;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 buttonDownPosition = new Vector3(initX, MinLocalY, initZ);
        Vector3 buttonUpPosition = new Vector3(initX, MaxLocalY, initZ);
        transform.localPosition = new Vector3(initX, transform.localPosition.y, initZ);

        // If exceed max height, stay at max height
        if (transform.localPosition.y > MaxLocalY)
        {
            transform.localPosition = buttonUpPosition;
        }

        // If bring pushed down, come back up smoothly
        if (!isBeingTouched && transform.localPosition.y < MaxLocalY)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, buttonUpPosition, Time.deltaTime * smooth);
        }

        // If exceed min height, stay at min height, and trigger OnButtonDown
        if (transform.localPosition.y < MinLocalY)
        {
            // transform.localPosition = buttonDownPosition;
            OnButtonDown();
        }
        else
        {
            OnButtonUp();
        }

    }

    void OnButtonDown()
    {
        GetComponent<MeshRenderer>().material = greenMat;
        m_MyEvent.Invoke();
        print("test");
    }

    void OnButtonUp()
    {
        GetComponent<MeshRenderer>().material = redMat;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.gameObject.tag != "BackButton")
        {
            isBeingTouched = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.gameObject.tag != "BackButton")
        {
            isBeingTouched = false;
        }
    }
}
