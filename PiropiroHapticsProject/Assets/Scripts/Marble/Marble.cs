using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Marble : MonoBehaviour
{
    private float dist = 0.4f;
    
    [SerializeField]
    private PiropiroUdp udp;
    [SerializeField]
    private AudioSource[] audioSource;
    private AudioClip blowAudio;
    private AudioClip marbleAudio;
    private bool isPlayingAudio = false;


    void Start()
    {

    }

    void Update()
    {

        dist = udp.GetLen();

        
        if (dist < 1f) {
            
            if (isPlayingAudio) {
            audioSource[0].Stop();
            audioSource[1].Stop();
            isPlayingAudio = false;
            }
            
            dist = 0.4f;
            
        } else {
            if (!isPlayingAudio) {
                audioSource[0].Play();
                audioSource[1].Play();
                isPlayingAudio = true;
            }
        }
        
        transform.position = new Vector3(0, 0, dist);

    }


}