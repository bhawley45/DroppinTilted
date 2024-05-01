using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManuCameraMovement : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float rotationSpeed;

    private void Update()
    {
        //Rotate camera around target object
        transform.RotateAround(target.position, Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
