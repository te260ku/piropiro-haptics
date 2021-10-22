using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public GameObject arrowPrefab;
    [SerializeField]
    private GameObject center;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool isPressed = OVRInput.Get(OVRInput.RawButton.RIndexTrigger); 
        if (isPressed) {
            GameObject arrow = Instantiate(arrowPrefab);
            arrow.transform.parent = center.transform;

        }
    }
}
