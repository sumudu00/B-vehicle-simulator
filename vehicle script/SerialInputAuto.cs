using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.IO.Ports;
using System.Threading;





public class SerialInputAuto : MonoBehaviour {

	SerialPort stream = new SerialPort("COM5", 9600);
	
	
	private float[] Inputs;
	
	public float EngineState;
	//public float ClutchInput;
	//public float GearVal;
	public float ReverseGear;
	public float MotorTorqueInput;
	public float HandBrakeTorqueInput;
	public float FootBrakeTorqueInput;
	public float SteerAngleInput;
	
	
	public bool Serial = false;
	
	private Thread t1;
	public bool stop = false;
	
	public string[] ports;

	// Use this for initialization
	void Start () {
		//stream.Open();
		//t1 = new Thread(SerialRead);
		//t1.Start();
		
		//stream.ReadTimeout = 100000;
		
			
		
		
		
	
	}
	
	
	
	// Update is called once per frame
	  void Update () {
		 try {
	if (!stop && !stream.IsOpen) {
	stream.Open();
	t1 = new Thread(SerialRead);
	t1.Start();
	stream.ReadTimeout = 100000;
	}
		 }
		 catch (IOException e){
			Debug.Log("error is " + e.ToString());
            Serial = false;
		}
	ports = SerialPort.GetPortNames();
	}
		
	void SerialRead () {
		
		while (!stop && stream.IsOpen) {
			//SerialReadValues ();
			try {
			
		string value = stream.ReadLine();
		Debug.Log (value);
		string[] vec3 = value.Split(',');
		
		if(vec3[0] != "" && vec3[1] != "" && vec3[2] != "" && vec3[3] != "" && vec3[4] != "" && vec3[5] != ""){
		
		Serial = true;
		
		Inputs = new float[6] {float.Parse(vec3[0]), float.Parse(vec3[1]), float.Parse(vec3[2]), float.Parse(vec3[3]) , float.Parse(vec3[4]), float.Parse(vec3[5])};
		
		EngineState = Inputs[0];
		
		//ClutchInput = Inputs[1];
		
		//GearVal = Inputs[2];
		
		ReverseGear = Inputs[1];
		
		MotorTorqueInput = Inputs[5]; //float.Parse(vec3[1]);
		
		HandBrakeTorqueInput = Inputs[2];
		
		FootBrakeTorqueInput = Inputs[4];
		
		SteerAngleInput = Inputs[3];
		}
		}
		catch (IOException e){
                Debug.Log("error is " + e.ToString());
                Serial = false;
				stream.Close();
				t1.Abort();
				//stop = true;
            }
		}
		
		
		
	}
	
	/* void SerialReadValues () {
		try {
			
		string value = stream.ReadLine();
		Debug.Log (value);
		string[] vec3 = value.Split(',');
		
		if(vec3[0] != "" && vec3[1] != "" && vec3[2] != "" && vec3[3] != ""){
		
		Serial = true;
		
		Inputs = new float[4] {float.Parse(vec3[0]), float.Parse(vec3[1]), float.Parse(vec3[2]), float.Parse(vec3[3])};
		
		EngineState = Inputs[0];
		
		MotorTorqueInput = Inputs[1]; //float.Parse(vec3[1]);
		
		BrakeTorqueInput = Inputs[2];
		
		SteerAngleInput = Inputs[3];
		}
		}
		catch (IOException e){
                Debug.Log("error is " + e.ToString());
                Serial = false;
				
				stop = true;
            }
	} */
	
	public void OnApplicationquit() {
		stop = true;
		t1.Abort();
	}
	
	public void OnDestroy() {
		stop = true;
		t1.Abort();
	}
	

}
