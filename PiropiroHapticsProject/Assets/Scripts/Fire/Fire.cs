using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Fire : MonoBehaviour
{
    [SerializeField]
    private PiropiroUdp udp;
    private float dist;
    private AudioSource audioSource;
    private AudioClip fireAudio;
    private bool isPlayingAudio = false;
    [HideInInspector]
    public ParticleSystem fire;
    public GameObject box;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        GameObject fireParticle = transform.GetChild(0).gameObject;
        fire = fireParticle.GetComponent<ParticleSystem>();
    }

    void Update()
    {

        dist = udp.GetLen();

        if (1f < dist) {
            fire.Play();
            
            if (!isPlayingAudio) {
                audioSource.Play();
                isPlayingAudio = true;
            }
        } else {
            fire.Stop();
            
            if (isPlayingAudio) {
                audioSource.Stop();
                isPlayingAudio = false;
            }
        }

        box.transform.localScale = new Vector3(1f, 1f, dist*1.5f);
    }


}