using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OSCRec_pose : MonoBehaviour {
    public string RemoteIP = "127.0.0.1";
    public int SendToPort = 3333;
    public int ListenerPort = 3334;
    public int channel = 0;
    public string OSC_address = "/pose";

    private Osc handler;
    private UDPPacketIO udp;
    private float nose_X;
    private float nose_Y;
    private GameObject nose;

    // Use this for initialization
    void Start () {
        udp = this.GetComponent<UDPPacketIO>();
        nose = GameObject.Find("Avatar_Nose");
        udp.init(RemoteIP, SendToPort, ListenerPort);
        handler = this.GetComponent<Osc>();
        handler.init(udp);

        handler.SetAddressHandler(OSC_address, ListenEvent);
    }
	
	public void ListenEvent(OscMessage oscMessage)
    {
        nose_X = (float)oscMessage.Values[1]-150;
        nose_Y = (float)oscMessage.Values[2]-150;
    }

    void Update()
    { 
        Vector3 nose_pos = nose.transform.position;
        nose_pos.x = nose_X;     
        nose.transform.position = nose_pos;
    }
}
