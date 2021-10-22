using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleDebug : MonoBehaviour
{
    private float debugLen = 0f;
    
    [SerializeField]
    private AudioSource[] audioSource;
    private AudioClip blowAudio;
    private AudioClip marbleAudio;
    private bool isPlayingAudio = false;

    // Start is called before the first frame update
    void Start()
    {
        // audioSource = GetComponent<AudioSource>();
        // cameleon
        // transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, debugLen);
    }

    // Update is called once per frame
    void Update()
    {

        
        if (Input.GetKey(KeyCode.Space)) {
            if (!isPlayingAudio) {
                audioSource[0].Play();
                audioSource[1].Play();
                isPlayingAudio = true;
            }
            if (debugLen < 9.5f) {
                debugLen += 0.2f;
            }
            
        } else {
            debugLen -= 0.3f;
            if (isPlayingAudio) {
                audioSource[0].Stop();
                audioSource[1].Stop();
                isPlayingAudio = false;
            }
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
