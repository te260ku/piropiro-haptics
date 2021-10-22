using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Arrow : MonoBehaviour
{
    [SerializeField]
    private PiropiroUdp udp;
    private float dist;
    private bool isFired = false;
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip blowAudio;
    [SerializeField]
    private AudioClip hitAudio;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
    }

    void Update()
    {

        dist = udp.GetLen();

        if (!isFired) {
            if (dist > 5f) {
                Rigidbody rb = GetComponent<Rigidbody>();
                rb.AddForce(transform.forward*-15, ForceMode.Impulse);
                isFired = false;
                audioSource.clip = blowAudio;
                audioSource.Play();
                isFired = true;
            } 
        }


    }


}