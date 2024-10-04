using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Text;

public class SerialWriteActuators : MonoBehaviour
{
    public string ipAddress = "255.255.255.255"; // FlyPTMover IP Address
    public int port = 20777; // FlyPTMover Port
    private UdpClient udpClient;

    EVP.FourWheelGearInput FourWheelGearTo6DOFScript;
    EVP.VehicleControllerGear VehicleControllerTo6DOF;
    //public float CarVelocity;
    public Vector3 CarVelocity;
    public Vector3 CarPosition;
    public Vector3 CarAcceleration;
    //public float CarAcceleration;
    //public Vector3 CarAcceleration;
    Rigidbody rb;
    public Vector3 gravityForce;
    float carPosRoundX;
    float carVelRoundX;
    float carAccRoundX;
    float carPosRoundY;
    float carVelRoundY;
    float carAccRoundY;
    public string data;

    void Start()
    {
        FourWheelGearTo6DOFScript = GameObject.FindGameObjectWithTag("RallyCar").GetComponent<EVP.FourWheelGearInput>();
        VehicleControllerTo6DOF = GameObject.FindGameObjectWithTag("RallyCar").GetComponent<EVP.VehicleControllerGear>();
        rb = GetComponent<Rigidbody>();

        udpClient = new UdpClient();
    }


    // Update is called once per frame
    void Update()
    {
        SendData();

    }

    void SendData()
    {
        /*CarVelocity = FourWheelGearTo6DOFScript.SpeedoMeter;
        CarPosition = transform.localPosition;
        //CarAcceleration = VehicleControllerTo6DOF.throttleInput;
        CarAcceleration = FourWheelGearTo6DOFScript.SpeedoMeter/ Time.deltaTime;*/

        CarVelocity = rb.velocity;
        CarPosition = transform.localPosition;
        //CarAcceleration = VehicleControllerTo6DOF.throttleInput;
        CarAcceleration = rb.velocity / Time.deltaTime;

        gravityForce = rb.mass * Physics.gravity;
        carPosRoundX = Mathf.Round(CarPosition.x * 10f) / 10f;
        carVelRoundX = Mathf.Round(CarVelocity.x * 10f) / 10f;
        carAccRoundX = Mathf.Round(CarAcceleration.x * 10f) / 10f;

        carPosRoundY = Mathf.Round(CarPosition.y * 10f) / 10f;
        carVelRoundY = Mathf.Round(CarVelocity.y * 10f) / 10f;
        carAccRoundY = Mathf.Round(CarAcceleration.y * 10f) / 10f;

        data = $"{carPosRoundX},{carVelRoundX},{carAccRoundX}," + 
                $"{carPosRoundY},{carVelRoundY},{carAccRoundY}";
        //string formattedData = "<" + variable1.ToString("F2") + "><" + variable2.ToString("F2") + "><" + variable3.ToString("F2") + ">";
        //data = "<" + carPosRoundX + "><" + carVelRoundX+ "><" + carAccRoundX + ">";
        //data = "10,20,30,40,50,60";
        //data = $"<carPosRoundX>,<carVelRoundX>,<carAccRoundX>," +
         //       $"<carPosRoundY>,<carVelRoundY>,<carAccRoundY>,";
        //string data = $"{Mathf.Round(CarPosition.x * 1000f) / 1000f},{Mathf.Round(CarVelocity.x * 1000f) / 1000f},{Mathf.Round(CarAcceleration.x * 1000f) / 1000f}";
        /*+
                  $"{Mathf.Round(CarPosition.y * 1000f) / 1000f},{CarVelocity.y},{CarAcceleration.y}," +
                  $"{CarPosition.z},{CarVelocity.z},{CarAcceleration.z}";*/

        byte[] sendBytes = Encoding.ASCII.GetBytes(data);
        udpClient.Send(sendBytes, sendBytes.Length, ipAddress, port);
    }
    void OnDestroy()
    {
        udpClient.Close();
    }
}
