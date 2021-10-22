using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ArrowSerial : MonoBehaviour
{
    public SerialHandler serialHandler;
    private float dist;
    private bool isFired = false;
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip blowAudio;
    [SerializeField]
    private AudioClip hitAudio;
    
    
    private float Map(float value, float start1, float stop1, float start2, float stop2)
    {
        return start2 + (stop2 - start2) * ((value - start1) / (stop1 - start1));
    }

    void Start()
    {
        serialHandler.OnDataReceived += OnDataReceived;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {


    }

    void OnDataReceived(string message)
    {
        var data = message.Split(
                new string[]{"\t"}, System.StringSplitOptions.None);
        if (data.Length < 2) return;

        try {
            float tmp = float.Parse(data[0]);
                
            float raw = Map(tmp, 70f, 250f, 0f, 10f);
        
            Debug.Log(raw);
            
            if (!isFired) {
                if (raw > 5f) {
                    Rigidbody rb = GetComponent<Rigidbody>();
                    rb.AddForce(transform.forward*-15, ForceMode.Impulse);
                    isFired = false;
                    audioSource.clip = blowAudio;
                    audioSource.Play();
                    isFired = true;
                } 
            }
            
            
            
        } catch (System.Exception e) {
            Debug.LogWarning(e.Message);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // audioSource.PlayOneShot(hitAudio);
        Destroy(gameObject, 1.5f);
    }
}