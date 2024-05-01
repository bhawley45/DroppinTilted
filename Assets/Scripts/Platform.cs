using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    [SerializeField] float rotationLimit;

    float rotationX = 0;
    float rotationZ = 0;

    private Rigidbody rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rotationX = normalizeRotation(rotationX);
        rotationZ = normalizeRotation(rotationZ);

        float inputX = Input.GetAxis("Vertical");
        float inputZ = -Input.GetAxis("Horizontal"); //"-" to Flip

        Vector3 rotationDirection = new Vector3(inputX, 0f, inputZ);

        if (Mathf.Abs(rotationX) <= rotationLimit) { rotationX += inputX * rotationSpeed * Time.deltaTime; }
        if (Mathf.Abs(rotationZ) <= rotationLimit) { rotationZ += inputZ * rotationSpeed * Time.deltaTime; }

        //Perform rotation  
        transform.localRotation = Quaternion.Euler(rotationX, 0, rotationZ);
    }

    float normalizeRotation(float axis)
    {
        if (Mathf.Abs(axis) > rotationLimit) { axis = Mathf.Round(axis); }
        return axis; //Return a whole# value
    }
}
