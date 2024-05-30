using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class Security : MonoBehaviour
{
    [SerializeField] GameObject FuseBox;
    [SerializeField] AudioClip powerOff;
    [SerializeField] AudioClip monsterGrowl;
    [SerializeField] AudioClip heavyBreathing;
    [SerializeField] AudioClip manDeath;
    [SerializeField] TMP_Text text;
    [SerializeField] GameObject blackScreen;
    [SerializeField] GameObject shadowMonster;
    [SerializeField] GameObject DeathTrigger;
    [SerializeField] GameObject ShadowHideTrigger;
    [SerializeField] GameObject InteractablePopup;

    int Count = 0;
    float Distance;
    AudioSource audioSource;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(powerOff, 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        Distance = Vector3.Distance(transform.position, FuseBox.transform.position);
        if (Input.GetKeyDown(KeyCode.F) && Count > 0 && Distance < 2)
        {
            Count--;
            FuseBox.SendMessage("AddFuse");

        }
    }

    void PickedFuse()
    {
        Count++;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "FuseTrigger")
        {
            other.gameObject.SetActive(false);
            text.text = "Objective: Find 3 fuses to restore power";
        }

        if (other.name == "RunningSoundTrigger")
        {
            other.GetComponent<AudioSource>().Play();
            other.gameObject.SetActive(false);
        }

        if (other.name == "DeathTrigger")
        {
            other.gameObject.SetActive(false);
            gameObject.GetComponent<FirstPersonController>().enabled = false;
            StartCoroutine(Monster());
        }
        if (other.name == "ShadowMovementTrigger")
        {
            shadowMonster.SetActive(true);
            shadowMonster.SendMessage("startMoving");
            DeathTrigger.SetActive(true);
            ShadowHideTrigger.SetActive(true);
            other.gameObject.SetActive(false);
        }
        if (other.name == "ShadowHideTrigger")
        {
            shadowMonster.SetActive(false);
        }

    }

    IEnumerator Monster()
    {
        audioSource.PlayOneShot(monsterGrowl);

        yield return new WaitForSeconds(2);

        StartCoroutine(Breathing());
    }

    IEnumerator Breathing()
    {
        audioSource.PlayOneShot(heavyBreathing);

        yield return new WaitForSeconds(2);

        StartCoroutine(BlackScreen());
    }

    IEnumerator BlackScreen()
    {
        blackScreen.SetActive(true);

        yield return new WaitForSeconds(1);

        audioSource.PlayOneShot(manDeath);

        yield return new WaitForSeconds(4f);

        SceneManager.LoadScene("Act2");
    }

    void EnableInteractable()
    {
        InteractablePopup.SetActive(true);
    }

    void DisableInteractable()
    {
        InteractablePopup.SetActive(false);
    }
}
