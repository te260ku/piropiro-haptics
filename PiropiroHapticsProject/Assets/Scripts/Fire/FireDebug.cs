using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDebug : MonoBehaviour
{
    private float debugLen = 0f;
    
    private AudioSource audioSource;
    private AudioClip fireAudio;
    private bool isPlayingAudio = false;
    [HideInInspector]
    public ParticleSystem fire;
    public GameObject box;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        GameObject fireParticle = transform.GetChild(0).gameObject;
        fire = fireParticle.GetComponent<ParticleSystem>();
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
            if (debugLen > 0) {
                debugLen -= 0.3f;
            }
            
        }


        if (3f < debugLen) {
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
        box.transform.localScale = new Vector3(0.5f, 0.5f, debugLen);
    }
}
