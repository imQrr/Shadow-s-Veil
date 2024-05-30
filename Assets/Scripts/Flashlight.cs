using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Flashlight : MonoBehaviour
{
    AudioSource audioSource;
    bool isEquipped = true;
    [SerializeField] GameObject LightSource;
    [SerializeField] AudioClip ClickingSound;
    [SerializeField] GameObject PlayerThumb;
    Animator fingerAnimator;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        fingerAnimator = PlayerThumb.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && isEquipped)
        {
            fingerAnimator.SetTrigger("ClickedButton");
            audioSource.PlayOneShot(ClickingSound);
            LightSource.SetActive(!LightSource.activeSelf);
        }
    }
}
