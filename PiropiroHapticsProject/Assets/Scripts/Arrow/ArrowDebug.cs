using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowDebug : MonoBehaviour
{
    private float debugLen = 0f;
    
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip blowAudio;
    [SerializeField]
    private AudioClip hitAudio;

    private bool isFired = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!isFired) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                
                Rigidbody rb = GetComponent<Rigidbody>();
                rb.AddForce(transform.forward*-15, ForceMode.Impulse);
                isFired = true;
                audioSource.PlayOneShot(blowAudio);
            }

        }
    
    }

    void OnTriggerEnter(Collider other)
    {
        audioSource.PlayOneShot(hitAudio);
        Destroy(gameObject, 1.5f);
    }
}
