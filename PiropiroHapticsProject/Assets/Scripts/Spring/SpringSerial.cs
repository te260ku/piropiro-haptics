using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class SpringSerial : MonoBehaviour
{
    public SerialHandler serialHandler;
    private float dist;
    
    
    private float Map(float value, float start1, float stop1, float start2, float stop2)
    {
        return start2 + (stop2 - start2) * ((value - start1) / (stop1 - start1));
    }

    void Start()
    {
        serialHandler.OnDataReceived += OnDataReceived;
        dist = 0f;
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, dist);
    }

    void Update()
    {

        // if (0 < dist) {
            
        // } else {
        //     dist = 0f;
            
        // }
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, dist);
        

    }

    void OnDataReceived(string message)
    {
        var data = message.Split(
                new string[]{"\t"}, System.StringSplitOptions.None);
        if (data.Length < 2) return;

        try {
            float tmp = float.Parse(data[0]);
            Debug.Log(tmp);
            dist = tmp;
                
            // float raw = Map(tmp, 70f, 250f, 0f, 10f);
            // if (raw < 1f) {
            //     dist = 1f;
            // } else {
            //     dist = raw;
            // }
            
            
        } catch (System.Exception e) {
            Debug.LogWarning(e.Message);
        }
    }
}