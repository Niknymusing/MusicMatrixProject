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

    private GameObject nose;
    private float nose_X;
    private float nose_Z;

    private GameObject leftWrist;
    private float leftWrist_X;
    private float leftWrist_Z;

    private GameObject rightWrist;
    private float rightWrist_X;
    private float rightWrist_Z;

    private GameObject leftAnkle;
    private float leftAnkle_X;
    private float leftAnkle_Z;

    private GameObject rightAnkle;
    private float rightAnkle_X;
    private float rightAnkle_Z;

    private GameObject body;
    private float body_X;
    private float body_Z;
    
    private float xOffset = 150;
    private float yOffset = 150;

    // Use this for initialization
    void Start () {
        udp = this.GetComponent<UDPPacketIO>();
        nose = GameObject.Find("Avatar_Nose");
        leftWrist = GameObject.Find("Avatar_leftWrist");
        rightWrist = GameObject.Find("Avatar_rightWrist");
        leftAnkle = GameObject.Find("Avatar_leftAnkle");
        rightAnkle = GameObject.Find("Avatar_rightAnkle");
        body = GameObject.Find("Avatar_Body");
        udp.init(RemoteIP, SendToPort, ListenerPort);
        handler = this.GetComponent<Osc>();
        handler.init(udp);

        handler.SetAddressHandler(OSC_address, ListenEvent);
    }
	
	public void ListenEvent(OscMessage oscMessage)
    {
        nose_X = (float)oscMessage.Values[1]-xOffset;
        nose_Z = (float)oscMessage.Values[2]-yOffset;

        leftWrist_X = (float)oscMessage.Values[37]-xOffset;
        leftWrist_Z = (float)oscMessage.Values[38]-yOffset;

        rightWrist_X = (float)oscMessage.Values[41]-xOffset;
        rightWrist_Z = (float)oscMessage.Values[42]-yOffset;

        leftAnkle_X = (float)oscMessage.Values[61]-xOffset;
        leftAnkle_Z = (float)oscMessage.Values[62]-yOffset;

        rightAnkle_X = (float)oscMessage.Values[65]-xOffset;
        rightAnkle_Z = (float)oscMessage.Values[66]-yOffset;

        body_X = ((float)oscMessage.Values[21]+(float)oscMessage.Values[25])/2-xOffset;
        body_Z = ((float)oscMessage.Values[21]+(float)oscMessage.Values[45])/2-yOffset;
    }

    void Update()
    { 
        if (nose != null)
        { 
            Vector3 nose_pos = nose.transform.position;
            nose_pos.x = nose_X;  
            nose_pos.z = nose_Z;   
            nose.transform.position = nose_pos;
        }

        if (leftWrist != null)
        {  
            Vector3 leftWrist_pos = leftWrist.transform.position;
            leftWrist_pos.x = leftWrist_X;  
            leftWrist_pos.z = leftWrist_Z;   
            leftWrist.transform.position = leftWrist_pos;
        }

        if (rightWrist != null)
        {
            Vector3 rightWrist_pos = rightWrist.transform.position;
            rightWrist_pos.x = rightWrist_X;  
            rightWrist_pos.z = rightWrist_Z;   
            rightWrist.transform.position = rightWrist_pos;
        }
        
        if (leftAnkle != null)
        {         
            Vector3 leftAnkle_pos = leftAnkle.transform.position;
            leftAnkle_pos.x = leftAnkle_X;  
            leftAnkle_pos.z = leftAnkle_Z;   
            leftAnkle.transform.position = leftAnkle_pos;
        }

        if  (rightAnkle != null)
        {
            Vector3 rightAnkle_pos = rightAnkle.transform.position;
            rightAnkle_pos.x = rightAnkle_X;  
            rightAnkle_pos.z = rightAnkle_Z;   
            rightAnkle.transform.position = rightAnkle_pos;
        }
            
        if  (body != null)
        {
            Vector3 body_pos = body.transform.position;
            body_pos.x = body_X;  
            body_pos.z = body_Z;   
            body.transform.position = body_pos;
        }       
    }
}
