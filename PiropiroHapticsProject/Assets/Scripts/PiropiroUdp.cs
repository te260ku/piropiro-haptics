using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;


public class PiropiroUdp : MonoBehaviour
{
    int LOCAL_PORT = 22222;
    static UdpClient udp;
    static UdpClient udp_send;
    Thread thread;
    string host = "192.168.11.11";
    // 192.168.100.138
    // 192.168.100.123 
    int port = 22224;

    private static float len = 0.4f;
    public float GetLen(){
        return len;
    }

    private static float Map(float value, float start1, float stop1, float start2, float stop2)
    {
        return start2 + (stop2 - start2) * ((value - start1) / (stop1 - start1));
    }

    // Start is called before the first frame update
    void Start()
    {
        udp = new UdpClient(LOCAL_PORT);
        udp_send = new UdpClient();
        udp.Client.ReceiveTimeout = 0;
        udp_send.Connect(host, port);
        thread = new Thread(new ThreadStart(ThreadMethod));
        thread.Start();

    }


    void OnApplicationQuit()
    {
        udp_send.Close();
        thread.Abort();
    }

    private static void ThreadMethod()
    {
        while (true)
        {
            IPEndPoint remoteEP = null;
            byte[] data = udp.Receive(ref remoteEP);
            string test = "";
            test = "";

            for (int a = data.Length - 1; a >= 0; a--)
            {
                string tmp = data[a].ToString();
                int f = int.Parse(tmp);
                // f /= 100f;
                
                // len = Map(f, 70f, 250f, 0f, 10f);
                // len /= 100f;

                float raw = Map(f, 70f, 250f, 0f, 10f);
                if (raw < 1f) {
                    len = 0.4f;
                } else {
                    len = raw;
                }

                test += f.ToString();

  
                
            

            }

            Debug.Log(test); 
        }
    }


    public void senddataUDP()
    {

        byte[] data = { 0x33, 0x34, 0x35, };
        udp_send.Send(data, 3);
    }

}