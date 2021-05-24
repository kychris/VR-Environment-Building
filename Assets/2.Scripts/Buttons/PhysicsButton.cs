using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PhysicsButton : MonoBehaviour
{
    [SerializeField] private float threshold = 0.1f;
    [SerializeField] private float deadZone = 0.025f;

    public Material idleMaterial;
    public Material pressedMaterial;

    public UnityEvent onPressed, onRealeased;

    private bool isPressed;
    private Vector3 startPos;
    private ConfigurableJoint joint;
    private MeshRenderer buttonMesh;



    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.localPosition;
        joint = GetComponent<ConfigurableJoint>();
        buttonMesh = GetComponentInChildren<MeshRenderer>();
        buttonMesh.material = idleMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPressed && GetValue() + threshold >= 1)
            Pressed();
        if (isPressed && GetValue() - threshold <= 0)
            Released();

    }

    private float GetValue()
    {
        float value = Vector3.Distance(startPos, transform.localPosition) / joint.linearLimit.limit;

        if (Mathf.Abs(value) < deadZone)
            value = 0;

        return Mathf.Clamp(value, -1, 1);
    }

    private void Pressed()
    {
        isPressed = true;
        onPressed.Invoke();
        buttonMesh.material = pressedMaterial;
        print("Pressed");
    }

    private void Released()
    {
        isPressed = false;
        onRealeased.Invoke();
        buttonMesh.material = idleMaterial;
        print("Released");
    }
}
