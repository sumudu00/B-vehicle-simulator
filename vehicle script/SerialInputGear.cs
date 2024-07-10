using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.IO.Ports;
using System.Threading;

public class SerialInputGear : MonoBehaviour
{
    private const string PORT3 = "COM3";
    private SerialPort _portGear3 = new SerialPort("\\\\.\\" + PORT3, 115200);

    private bool _runThreadGear3 = true;

    private Thread pollingThreadGear3;

    string line3;
    public int len3;
    private string tempS3 = "";
    private string[] vec3 = new string[] { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0"};
    private string[] StopString3 = new string[] { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };

    public int SteerAngleInput = 0;
    public int ClutchInput = 0;
    public int FootBrakeInput = 0;
    public int AcceleratorInput = 0;
    public int GearVal = 0;
    public int HandBrakeInput = 0;
    public int SignalLight = 0;
    public int ParkingLight = 0;
    public int HeadLight = 0; //1, 2, 3, 4, 0
    public int PassDimLight = 0;
    public int EngineState = 0;
    public int IgnitionInput = 0;
    public int Horn = 0;
    public int ODButtonVal = 0;
    //MirrorInputs
    public int MirrorRL = 0;
    public int MirrorRR = 0;
    public int MirrorRU = 0;
    public int MirrorRD = 0;
    public int MirrorLR = 0;
    public int MirrorLL = 0;
    public int MirrorLD = 0;
    public int MirrorLU = 0;


    public bool Serial = false;

    private UIScript ButtonScript;

    public GameObject SerialCanvas;
    public Text SteerAngleInputT;
    public Text ClutchInputT;
    public Text FootBrakeInputT;
    public Text AcceleratorInputT;
    public Text GearValT;
    public Text HandBrakeInputT;
    public Text SignalLightT;
    public Text ParkingLightT;
    public Text HeadLightT;
    public Text PassDimLightT;
    public Text EngineStateT;
    public Text IgnitionInputT;
    public Text HornT;
    public Text ODButtonValT;
    public Text MirrorRLT;
    public Text MirrorRRT;
    public Text MirrorRUT;
    public Text MirrorRDT;
    public Text MirrorLRT;
    public Text MirrorLLT;
    public Text MirrorLDT;
    public Text MirrorLUT;
    //public Text MirrorInputT;
    //public Text WipersT;
    public Text ErrorMessageText;

    string errorMessage;

    void Start()
    {
        ButtonScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UIScript>();

        /*_portGear = new SerialPort(PORT, 9600);
        _portGear.Open();
        _portGear.ReadTimeout = 5;

        pollingThreadGear = new Thread(RunpollingThreadGear) { IsBackground = true };
        pollingThreadGear.Start();*/
    }

    public void Stop()
    {
        _runThreadGear3 = false;
        _portGear3.Close();
        
        Serial = false;
    }

    private void RunpollingThreadGear3()
    {
        while (_runThreadGear3)
        {
            PollArduino3();
        }
    }
    

    private void PollArduino3()
    {
        
        if (!_portGear3.IsOpen)
            return;


        tempS3 = "";
        line3 = null;
        try
        {
            byte tempB1 = (byte)_portGear3.ReadByte();
            while (tempB1 != 255)
            {
                tempS3 += ((char)tempB1);
                tempB1 = (byte)_portGear3.ReadByte();

                if (tempB1 == '$')
                {

                    line3 = tempS3;
                    tempS3 = "";
                }
            }

            Debug.Log("dATA lINE:  " + line3);
        }
        catch (System.Exception e)
        {
            Debug.Log("IO Gear Error : " + e);
            line3 = null;
        }

    }
    
    // Update is called once per frame
    void Update()
    {

        //Read the PORT3
        try
        {
            if (!_portGear3.IsOpen && ButtonScript.SerialStateDrive)
            {
                _portGear3 = new SerialPort("\\\\.\\" + ButtonScript.PortNameDrive, 115200);
                _portGear3.Open();
                _portGear3.ReadTimeout = 1000;

                pollingThreadGear3 = new Thread(RunpollingThreadGear3) { IsBackground = true };
                _runThreadGear3 = true;
                
                pollingThreadGear3.Start();
            }
            else if (_portGear3.IsOpen && !ButtonScript.SerialStateDrive)
            {
                Stop();
                Serial = false;
                line3 = null;
            }
            else if (!_portGear3.IsOpen)
            {
                errorMessage = "PORT is not initialized";
            }

        }
        catch (IOException e)
        {
            Debug.Log("IO Gear Error : " + e);
            Serial = false;
            errorMessage = "Error in Initializing the PORT";
        }


        

        //Assign the read data from PORT3 to the relevent parameters
        AssignDatatoParameters1();
        

        if (Input.GetKey(KeyCode.N))
        {
            if (SerialCanvas.activeSelf)
            {
                SerialCanvas.SetActive(false);
            }
            else
                SerialCanvas.SetActive(true);
        }

    }

    

    void AssignDatatoParameters1()
    {
        //Debug.Log(line3);
        if (line3 != null)
        {
            vec3 = line3.Split(',');
            len3 = vec3.Length;
            if (vec3[0] == "$" && len3 == 24)
            {

                if (vec3[1] == "DP")
                {
                    Serial = true;


                    SteerAngleInput = int.Parse(vec3[2]);
                    ClutchInput = int.Parse(vec3[3]);
                    FootBrakeInput = int.Parse(vec3[4]);
                    AcceleratorInput = int.Parse(vec3[5]);
                    GearVal = int.Parse(vec3[6]);
                    HandBrakeInput = int.Parse(vec3[7]);
                    SignalLight = int.Parse(vec3[8]);
                    ParkingLight = int.Parse(vec3[9]);
                    HeadLight = int.Parse(vec3[10]);
                    PassDimLight = int.Parse(vec3[11]);
                    EngineState = int.Parse(vec3[13]);
                    IgnitionInput = int.Parse(vec3[12]);
                    Horn = int.Parse(vec3[14]);
                    ODButtonVal = int.Parse(vec3[15]);
                    MirrorRL = int.Parse(vec3[16]);
                    MirrorRR = int.Parse(vec3[17]);
                    MirrorRU = int.Parse(vec3[18]);
                    MirrorRD = int.Parse(vec3[19]);
                    MirrorLR = int.Parse(vec3[20]);
                    MirrorLL = int.Parse(vec3[21]);
                    MirrorLD = int.Parse(vec3[22]);
                    MirrorLU = int.Parse(vec3[23]);

                    errorMessage = "Reading data successfully";

                    //ErrorMessageText.color = Color.green;

                    SetSerialDataString1();
                }
            }
            else
            {
                //SteerAngleInput = 0;
                //ClutchInput = 0;
                //FootBrakeInput = 0;
                //AcceleratorInput = 0;
                //GearVal = 0;
                //HandBrakeInput = 0;
                //SignalLight = 0;
                //ParkingLight = 0;
                //HeadLight = 0;
                //PassDimLight = 0;
                //EngineState = 0;
                //IgnitionInput = 0;
                //Horn = 0;
                //ODButtonVal = 0;
                //MirrorRL = 0;
                //MirrorRR = 0;
                //MirrorRU = 0;
                //MirrorRD = 0;
                //MirrorLR = 0;
                //MirrorLL = 0;
                //MirrorLD = 0;
                //MirrorLU = 0;

                errorMessage = "Reading data but not in correct format";
                //ErrorMessageText.color = Color.red;
            }
        }
        else
        {
            //ErrorMessageText.color = Color.red;
        }

        //ErrorMessageText.text = errorMessage.ToString();
    }

    void SetSerialDataString1()
    {
        //SteerAngleInputT.text = SteerAngleInput.ToString();
        //ClutchInputT.text = ClutchInput.ToString();
        //FootBrakeInputT.text = FootBrakeInput.ToString();
        //AcceleratorInputT.text = AcceleratorInput.ToString();
        //GearValT.text = GearVal.ToString();
        //HandBrakeInputT.text = HandBrakeInput.ToString();
        //SignalLightT.text = SignalLight.ToString();
        //ParkingLightT.text = ParkingLight.ToString();
        //HeadLightT.text = HeadLight.ToString();
        //PassDimLightT.text = PassDimLight.ToString();
        //EngineStateT.text = EngineState.ToString();
        //IgnitionInputT.text = IgnitionInput.ToString();
        //HornT.text = Horn.ToString();
        //ODButtonValT.text = ODButtonVal.ToString();
        //MirrorRLT.text = MirrorRL.ToString();
        //MirrorRRT.text = MirrorRR.ToString();
        //MirrorRUT.text = MirrorRU.ToString();
        //MirrorRDT.text = MirrorRD.ToString();
        //MirrorLRT.text = MirrorLR.ToString();
        //MirrorLLT.text = MirrorLL.ToString();
        //MirrorLDT.text = MirrorLD.ToString();
        //MirrorLUT.text = MirrorLU.ToString();
        ////MirrorInputT.text = MirrorInput.ToString();
        ////WipersT.text = Wipers.ToString();
        //ErrorMessageText.text = errorMessage;
    }
    
    

    public void OnApplicationQuit()
    {
        Stop();
        pollingThreadGear3.Abort();
    }
}
