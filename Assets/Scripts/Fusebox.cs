using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Fusebox : MonoBehaviour
{
    [SerializeField] GameObject Fuse;
    [SerializeField] GameObject Lights;
    [SerializeField] AudioClip powerOn;
    [SerializeField] Material skyBox;
    [SerializeField] TMP_Text text;
    [SerializeField] GameObject shadowTriggerMovement;
    AudioSource audioSource;
    int Count = 0;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AddFuse()
    {
        Count++;

        if (Count == 1)
        {
            GameObject SetFuse = GameObject.Instantiate(Fuse);
            SetFuse.GetComponent<CapsuleCollider>().enabled = false;
            SetFuse.transform.position = new Vector3(-47.926f, 1.55f, 23.676f);
            SetFuse.transform.rotation = new Quaternion(0, 0, -0.707106829f, 0.707106829f);
            SetFuse.SendMessage("CannotInteract");
        }

        if (Count == 2)
        {
            GameObject SetFuse = GameObject.Instantiate(Fuse);
            SetFuse.GetComponent<CapsuleCollider>().enabled = false;
            SetFuse.transform.position = new Vector3(-47.9259987f, 1.54999995f, 23.4330006f);
            SetFuse.transform.rotation = new Quaternion(0, 0, -0.707106829f, 0.707106829f);
            SetFuse.SendMessage("CannotInteract");
        }

        if (Count == 3)
        {
            GameObject SetFuse = GameObject.Instantiate(Fuse);
            SetFuse.transform.position = new Vector3(-47.9259987f, 1.45899999f, 23.5200005f);
            SetFuse.transform.rotation = new Quaternion(0, 0, -0.707106829f, 0.707106829f);
            SetFuse.SendMessage("CannotInteract");
            Lights.SendMessage("EnableLights");
            audioSource.PlayOneShot(powerOn);
            RenderSettings.skybox = skyBox;
            RenderSettings.ambientIntensity = 1.9f;
            RenderSettings.reflectionIntensity = 0;
            RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Custom; // This switch of ambient mode fixes the issue where loading the skybox doesn not change the lighting in the building
            RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Skybox; // Had to light up the building with the skybox instead of point/spotlights because those reduce fps significantly
            DynamicGI.UpdateEnvironment();
            text.text = "Objective: Return to the security room";
            shadowTriggerMovement.SetActive(true);
        }
    }
}
