using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MarbleSerial : MonoBehaviour
{
    public SerialHandler serialHandler;
    private float debugLen = 0f;

    
    [SerializeField]
    private AudioSource[] audioSource;
    private AudioClip blowAudio;
    private AudioClip marbleAudio;
    private bool isPlayingAudio = false;
    private bool isSwitched = false;
    
    private float Map(float value, float start1, float stop1, float start2, float stop2)
    {
        return start2 + (stop2 - start2) * ((value - start1) / (stop1 - start1));
    }

    void Start()
    {
        serialHandler.OnDataReceived += OnDataReceived;

        
        // transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, debugLen);
    }

    void Update()
    {
        if (isSwitched) {
            if (!isPlayingAudio) {
                audioSource[0].Play();
                audioSource[1].Play();
                
            } else {
                audioSource[0].Stop();
                audioSource[1].Stop();
                
            }
            isSwitched = false;
        }
            
        

        if (0 < debugLen) {
            transform.position = new Vector3(transform.position.x, transform.position.y, debugLen);
        } else {
            debugLen = 0f;
        }

    }

    void OnDataReceived(string message)
    {
        var data = message.Split(
                new string[]{"\t"}, System.StringSplitOptions.None);
        if (data.Length < 2) return;

        try {
            float tmp = float.Parse(data[0]);
            
            // debugLen = tmp;
                
            
            float raw = Map(tmp, 70f, 250f, 0f, 10f);
            Debug.Log(raw);
            if (raw < 1f) {
                
                if (isPlayingAudio) {
                audioSource[0].Stop();
                audioSource[1].Stop();
                isPlayingAudio = false;
                }
                
                debugLen = 0.4f;
                
            } else {
                if (!isPlayingAudio) {
                    audioSource[0].Play();
                    audioSource[1].Play();
                    isPlayingAudio = true;
                }

                debugLen = raw;
            }
            
            
        } catch (System.Exception e) {
            Debug.LogWarning(e.Message);
        }
    }
}