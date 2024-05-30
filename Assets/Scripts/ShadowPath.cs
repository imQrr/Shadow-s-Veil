using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShadowPath : MonoBehaviour
{
    [SerializeField] GameObject destination;
    Animator animator;
    [SerializeField] TMP_Text text;
    
    float speed = 0.1f;
    bool move = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination.transform.position, speed);
            animator.SetBool("IsRunning", true);

            if (transform.position == destination.transform.position)
            {
                move = false;
                animator.SetBool("IsRunning", false);
                text.text = "Objective: Investigate the shadow";
            }
        }
    }

    void startMoving ()
    {
        move = true;
    }
}
