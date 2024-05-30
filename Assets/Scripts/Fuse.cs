using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuse : MonoBehaviour
{
    GameObject Player;

    float Distance = 1000f; // Initialize a high value to avoid being able to pick up the fuse on start up.

    bool CanPickUp = false;

    bool isInteractable = true;

    void Start()
    {
        Player = GameObject.FindWithTag("Security");
    }

    void Update()
    {
        if (isInteractable)
        {
            Distance = Vector3.Distance(transform.position, Player.transform.position);

            if (Distance < 2)
            {
                CanPickUp = true;
            }
            else
            {
                CanPickUp = false;
            }

            if (Input.GetKeyDown(KeyCode.F) && CanPickUp)
            {
                Player.SendMessage("PickedFuse");
                Player.SendMessage("DisableInteractable");
                Destroy(gameObject);
            }
        }
    }

    void CannotInteract()
    {
        isInteractable = false;
        GetComponent<CapsuleCollider>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Security")
        {
            other.SendMessage("EnableInteractable");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Security")
        {
            other.SendMessage("DisableInteractable");
        }
    }
}

