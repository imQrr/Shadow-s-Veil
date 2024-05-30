using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UVLight : MonoBehaviour
{
    [SerializeField] GameObject Light;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshCollider>().enabled = Light.activeSelf;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Light.SetActive(!Light.activeSelf);
            GetComponent<MeshCollider>().enabled = Light.activeSelf;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.SendMessage("TakeDamage");
        }
    }
}
