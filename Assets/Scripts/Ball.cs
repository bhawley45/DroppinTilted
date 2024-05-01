using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] AudioClip grassSFX;
    [SerializeField] AudioClip woodSFX;
    [SerializeField] AudioClip rockSFX;
    [SerializeField] AudioClip sandSFX;
    [SerializeField] AudioClip railSFX;
    [SerializeField] AudioClip deathTrack;
    [SerializeField] AudioClip winTrack;

    [SerializeField] float minSpeed = 5f; //For sound to play
    [SerializeField] float fadeOutTime = .15f;
    [SerializeField] float maxVolume = .8f;

    [SerializeField] AudioSource gameAudioSource;
    [SerializeField] UIManager uiManager;
    
    AudioSource audioSource;
    Rigidbody rb;

    [SerializeField] CameraFollow gameplayCam;
    //[SerializeField] Cinemachine.CinemachineBrain mainCam;

    private void Start()
    {
        audioSource = GameObject.Find("BallAudio").GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();

        rb.AddForce(300f, 0f, 0f);
    }

    private void OnCollisionStay(Collision collision)
    {
        float speed = collision.relativeVelocity.magnitude;
        float normalizedVolume = Mathf.Clamp01(speed / minSpeed) * maxVolume;
        audioSource.volume = normalizedVolume;

        while (!audioSource.isPlaying)
        {
            switch (collision.gameObject.tag)
            {
                case "Ground":
                    audioSource.clip = grassSFX;
                    break;
                case "Sand":
                    audioSource.clip = sandSFX;
                    break;
                case "Wood":
                    audioSource.clip = woodSFX;
                    break;
                case "Rock":
                    audioSource.clip = rockSFX;
                    break;
                case "Rail":
                    audioSource.clip = railSFX;
                    audioSource.volume += 2;
                    break;
            }
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        StartCoroutine(FadeOutSFX());
    }

    IEnumerator FadeOutSFX()
    {
        float startVolume = audioSource.volume;
        float elapsedTime = 0f;

        while (elapsedTime < fadeOutTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0f, elapsedTime / fadeOutTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the volume is set to 0 at the end of the fade-out, then stop
        audioSource.volume = 0f;
        audioSource.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Check if collision is with water layer
        if (other.CompareTag("Water"))
        {
            HandleWaterCollision();
        }

        if (other.CompareTag("Goal"))
        {
            HandleGameWin();
        }

        if (other.CompareTag("Trigger_NC"))
        {
            gameplayCam.offset = new Vector3(0f, 9f, -8.5f);
            gameplayCam.SetRotation(40f, 0f, 0f);
        }

        if (other.CompareTag("Trigger_FC"))
        {
            gameplayCam.offset = new Vector3(8.5f, 9f, 0f);
            gameplayCam.SetRotation(40f, -90f, 0f);
        }
    }

    private void HandleWaterCollision()
    {
        gameAudioSource.Stop();
        gameAudioSource.PlayOneShot(deathTrack);
        Destroy(gameObject);


        gameplayCam.GetComponent<CameraFollow>().ZoomOut();
        UIManager.Instance.ShowRetryScreen(); //Show retry Screen
    }

    private void HandleGameWin()
    {
        gameAudioSource.Stop();
        gameAudioSource.PlayOneShot(winTrack);

        gameplayCam.GetComponent<CameraFollow>().ZoomOut();
        UIManager.Instance.ShowWinScreen(); //Show Win Screen
    }
}
