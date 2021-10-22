using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PiropiroHMD : MonoBehaviour
{
    private float dist = 0f;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, dist);
    }

    // Update is called once per frame
    void Update()
    {

        bool isPressed = OVRInput.Get(OVRInput.RawButton.RIndexTrigger); 
        if (isPressed || Input.GetKey(KeyCode.Space)) {
            dist += 0.2f;
        } else {
            dist -= 0.3f;
        }


        if (0 < dist) {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, dist);
        } else {
            dist = 0f;
        }
    }

}
