using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugObject : MonoBehaviour
{
    private float debugLen = 0f;


    // Start is called before the first frame update
    void Start()
    {
        
        // cameleon
        // transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, debugLen);
    }

    // Update is called once per frame
    void Update()
    {

        
        if (Input.GetKey(KeyCode.Space)) {
            
            
            
            if (debugLen < 9.5f) {
                debugLen += 0.2f;
            }
            
        } else {
            
            debugLen -= 0.3f;
        }


        if (0 < debugLen) {
            // cameleon
            // transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, debugLen);
            // marble
            transform.position = new Vector3(transform.position.x, transform.position.y, debugLen);
        } else {
            debugLen = 0f;
        }
    }
}
