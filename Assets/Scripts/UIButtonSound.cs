using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonSound : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    [SerializeField] AudioClip hoverSFX;
    [SerializeField] AudioClip clickSFX;

    AudioSource audioSource;

    private void Start()
    {
        audioSource = GameObject.Find("ButtonAudio").GetComponent<AudioSource>();
    }

    //Play hoverSFX
    public void OnPointerEnter(PointerEventData eventData)
    {
        audioSource.PlayOneShot(hoverSFX);
    }

    //Play clickSFX
    public void OnPointerClick(PointerEventData eventData)
    {
        audioSource.PlayOneShot(clickSFX);
    }

}
