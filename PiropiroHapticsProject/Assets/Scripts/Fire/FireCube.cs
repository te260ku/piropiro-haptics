using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCube : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        GameObject target = other.gameObject;
        if (target.tag == "Target") {
            Destroy(target);
        }
    }
}
