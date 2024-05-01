using UnityEngine;
using Cinemachine;
using System.Collections;

public class SwitchCamera : MonoBehaviour
{
    [SerializeField] GameObject startingRailGroup;
    [SerializeField] CinemachineVirtualCamera startingCam;
    [SerializeField] CinemachineVirtualCamera gameplayCam;

    //reference to platform to disable movement while in begining sequence
    [SerializeField] Platform platformScript; 
 
    private void Start()
    {
        startingCam.gameObject.SetActive(true);
        gameplayCam.gameObject.SetActive(false);

        platformScript.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        SwitchCameras();
        platformScript.enabled = true;
        StartCoroutine(DestroyStartingRailGroup());
    }

    void SwitchCameras()
    {
        startingCam.gameObject.SetActive(false);
        gameplayCam.gameObject.SetActive(true);
    }

    IEnumerator DestroyStartingRailGroup()
    {
        yield return new WaitForSeconds(2);
        Destroy(startingRailGroup);
    }
}
