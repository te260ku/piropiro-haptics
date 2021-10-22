using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Piropiro : MonoBehaviour
{
    private float dist = 0.4f;
    // public Text distText;
    [SerializeField]
    private PiropiroUdp udp;
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip lickAudio;
    [SerializeField]
    private AudioClip eatAudio;
    private bool isStreched = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, dist);
    }

    void Update()
    {

        dist = udp.GetLen();

        // if (0 < dist) {
        //     transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, dist);
        // } else {
        //     dist = 0f;
        // }

        if (dist > 5f) {
            if (!isStreched) {
                audioSource.clip = lickAudio;
                audioSource.Play();
                isStreched = true;
            }
            
        } else if (dist < 1f) {
            if (isStreched) {
                audioSource.clip = eatAudio;
                audioSource.Play();
                isStreched = false;
            }
        }

        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, dist);

        // distText.text = dist.ToString();

    }


}