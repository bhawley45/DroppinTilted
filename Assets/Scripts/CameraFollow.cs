using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] float zoomOutFOV = 85f; //Amount to zoom out (on death)
    [SerializeField] float zoomOutDuration = 2f; //Time to zoom out (on death)

    [SerializeField] Transform target;
    public Vector3 offset;
    [SerializeField] float smoothing = 1000f;

    void FixedUpdate()
    {
        if (target != null)
        {
            //Calculate the position to move to
            Vector3 nextPosition = target.position + offset;

            //Dampen Transform
            transform.position = Vector3.Lerp(transform.position, nextPosition, 1 - Mathf.Exp(-smoothing * Time.deltaTime));
        }
    }

    public void ZoomOut()
    {
        StartCoroutine(ZoomOutCorouine());
    }

    private IEnumerator ZoomOutCorouine()
    {
        float currentFOV = Camera.main.fieldOfView;

        float elapsedTime = 0f;
        while (elapsedTime < zoomOutDuration)
        {
            Camera.main.fieldOfView = Mathf.Lerp(currentFOV, zoomOutFOV, elapsedTime / zoomOutDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    public void SetRotation(float x, float y, float z)
    {
        transform.rotation = Quaternion.Euler(x, y, z);
    }
}
