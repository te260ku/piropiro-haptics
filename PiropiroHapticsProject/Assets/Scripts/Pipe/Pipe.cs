using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    private float dist = 0f;
    
    [SerializeField]
    private PiropiroUdp udp;
    
    [SerializeField]
    private AudioSource[] audioSource;
    private AudioClip blowAudio;
    private AudioClip marbleAudio;
    private bool isPlayingAudio = false;
    private Vector3 initPos;

    // Start is called before the first frame update
    void Start()
    {
        initPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }
 

    // Update is called once per frame
    void Update()
    {
        dist = udp.GetLen();

        
        if (dist < 1f) {
            
            if (isPlayingAudio) {
            audioSource[0].Stop();
            audioSource[1].Stop();
            isPlayingAudio = false;
            }
            
            dist = 0;
            
        } else {
            if (!isPlayingAudio) {
                audioSource[0].Play();
                audioSource[1].Play();
                isPlayingAudio = true;
            }
        }
        
        transform.position = new Vector3(transform.parent.transform.position.x, initPos.y+dist/10, transform.parent.transform.position.z+1.5f);
    }
}
