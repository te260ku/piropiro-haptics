using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

//Unity でUDP通信受信
//https://qiita.com/nenjiru/items/8fa8dfb27f55c0205651

//パケット構造作ってる
//https://younaship.com/2018/12/31/unity%E4%B8%8A%E3%81%A7websocket%E3%82%92%E7%94%A8%E3%81%84%E3%81%9F%E9%80%9A%E4%BF%A1%E6%A9%9F%E8%83%BD%E3%83%9E%E3%83%AB%E3%83%81%E3%83%97%E3%83%AC%E3%82%A4%E3%82%92%E5%AE%9F%E8%A3%85%E3%81%99/

//タイムアウトについて
//https://teratail.com/questions/56000

public class UdpTest : MonoBehaviour
{
    int LOCAL_PORT = 22222;
    UdpClient udp;
    UdpClient udp_send;

    Thread thread;


    string host = "192.168.100.123";
    int port = 22224;

    // Start is called before the first frame update
    void Start()
    {
        udp = new UdpClient(LOCAL_PORT);
        udp_send = new UdpClient();
        udp.Client.ReceiveTimeout = 0;          //0にしてみたら通信できたけど、タイムアウト０＝
        udp_send.Connect(host, port);
        thread = new Thread(new ThreadStart(ThreadMethod));
        thread.Start();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnApplicationQuit()
    {
        udp_send.Close();
        thread.Abort();
    }

    private void ThreadMethod()
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
                float f = float.Parse(tmp);
                f /= 100f;
                test += f.ToString();
                // test += data[a].ToString();
                

            }


            Debug.Log(test);


            //連続したバイトを2byte や4byte のデータタイプ(int や float など)に変換するときは、ビットシフトなど利用
            

        }
    }


    public void senddataUDP()
    {

        byte[] data = { 0x33, 0x34, 0x35, };
        udp_send.Send(data, 3);
    }

}