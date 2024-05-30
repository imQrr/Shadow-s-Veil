using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    GameObject[] lamps; 
    [SerializeField] Material lightOn;
    // Start is called before the first frame update
    void Start()
    {
        lamps = GameObject.FindGameObjectsWithTag("Lamp");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void EnableLights()
    {
        foreach (GameObject gameObject in lamps)
        {
            gameObject.GetComponent<Renderer>().material = lightOn;
        }
    }
}
