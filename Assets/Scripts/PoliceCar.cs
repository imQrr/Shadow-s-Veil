using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceCar : MonoBehaviour
{
    bool isBlue = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SwitchColor());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SwitchColor()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);

            isBlue = !isBlue;
            if (isBlue)
            {
                GetComponent<Light>().color = Color.blue;
            }
            else
            {
                GetComponent<Light>().color = Color.red;
            }
        }
    }
}
