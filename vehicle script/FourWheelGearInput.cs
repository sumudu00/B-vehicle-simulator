//------------------------------------------------------------------------------------------------
// Edy's Vehicle Physics
// (c) Angel Garcia "Edy" - Oviedo, Spain
// http://www.edy.es
//------------------------------------------------------------------------------------------------

//using BeardedManStudios.Forge.Networking.Generated;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine.UI;
//using UnityEditor;

namespace EVP
{

    public class FourWheelGearInput : MonoBehaviour
    {
        public VehicleControllerGear target;

        public KeyCode SystemONKey = KeyCode.Return;
        public KeyCode IgnitionKey = KeyCode.Return;
        public KeyCode LeftSteerKey = KeyCode.Return;
        public KeyCode RightSteerKey = KeyCode.Return;
        public KeyCode ThrottleKey = KeyCode.Return;
        public KeyCode BrakeKey = KeyCode.Return;
        public KeyCode HandBrakeKey = KeyCode.Return;
        public KeyCode resetVehicleKey = KeyCode.Return;

        bool m_doReset = false;

        private SerialCommunicationReadScript SerialReadScript;

        public float[] GearMaxSpeed; //highest speed the vehicle can achieve at a gear
        //public float[] GearMinSpeed; //minimum speed the vehicle can drive at a gear
        //public float[] GearDownSpeed; //highest speed the gear can be shifted in each gear
        //public float[] BrakeMinSpeed; //minimum speed the vehicle can slow down by braking at each gear
        public float[] GearMaxTorque; //= {0.0f, 1000.0f, 800.0f, 600.0f, 500.0f, 400.0f}; //Maximum torques in each gear
        public float[] FootBrakeTorque;
        public float[] AutoGearMaxTorque;
        public float AutoFrwrdMaxSpeed = 60;//100
        public float AutoRvrseMaxSpeed = 10;
        public int index = 0; //the gear no.
        public float MaxSpeed; //Current gear max speed
        public float MaximumTorque;
        public bool Clutch = false;

        public float CurrentMotorTorque = 0;

        public float CurrentBTRSpeed = 0;

        public Light BrakeLightLeft;
        public Light BrakeLightRight;
        public Light SignalLeftFront;
        public Light SignalRightFront;
        public Light SignalLeftBack;
        public Light SignalRightBack;
        public int LeftSignal = 0;
        public int RightSignal = 0;
        //public Light HeadLightFront;
        public Light HeadLightLeft;
        public Light HeadLightRight;
        public Light HLightLeftDim;
        public Light HLightRightDim;
        public Light ParkLightLeft;
        public Light ParkLightRight;
        public int ParkOn = 0;
        public bool ParkOnKeyboardIn = true;
        public int HeadOn = 0;
        //public Light HeadLightSide;

        public float steerInputb = 0.0f;
        public float SteerSerialIn;
        public float SteerAngle;
        public float curr_steer;
        public float Steer;
        public float PreviousSteer;
        public float handbrakeInputb = 0.0f;
        public float throttleInputb = 0.0f; // private -> public only for testing
        private float brakeInputb = 0.0f;
        private float clutchInputb = 0.0f;
        float clutchInput = 0;

        public float HandBrakeForce = 50000.0f;
        public float FootBrakeForce = 8000.0f;
        public float DecelerationForce = 2000.0f;

        public bool SandText = false;

        public bool SystemIsON = false;
        public int IgnitionVal = 0;
        private int PrevIgnitionVal = 0;
        public bool IgnitionIsON = false;
        public int SerialSystemIsON = 0;
        float PrevSerialIgnition = 0;
        public float CurrentIgnition = 0;
        public bool SerialIgnitionIsOn = false;

        //public float SteerAngle;
        public float AppliedSteer;
        //private float Steer;
        public bool CluthBalance = false;
        float ClutchCount = 0;
        public string ClutchState;
        public float ThrottleInput = 0; // remove public , public added only for testing

        float ThrottleSerial = 0;
        float BrakeSerial = 0;
        float ClutchSerial = 0;
        // "Get Ranges Mode ON" button parameters
        public bool SetMinMax;
        // Clutch Range parameters
        List<float> ClutchList = new List<float>();
        public float ClutchMin = 0;
        public float ClutchMax = 0;
        // Accelerator Range parameters
        List<float> AccList = new List<float>();
        public float AccMin = 0;
        public float AccMax = 0;
        // Foot Brake Range parameters
        List<float> BrakeList = new List<float>();
        public float BrakeMin = 0;
        public float BrakeMax = 0;
        // "Range Text" parameters
        string[] RangeList = new string[] { "ClutchMin=786", "ClutchMax=689", "AccMin=326", "AccMax=410", "BrakeMin=442", "BrakeMax=355" };
        string[] TextLines;
        // Ranges of clutch, accl and foot brake are updated to the instructor panel
        public Text ClutchMinText;
        public Text ClutchMaxText;
        public Text AccMinText;
        public Text AccMaxText;
        public Text BrakeMinText;
        public Text BrakeMaxText;

        private UIScript ButtonScript;

        bool Ready = false;

        public float ZRot;
        public float XRot;
        private float hieght1;
        private float hieght2;
        private float hieght3;

        private float P_Point_Height;
        private float Q_Point_Height;
        private float R_Point_Height;
        private float Q_Point_Height1;
        private float R_Point_Height1;

        public float P_Height;
        public float P_Heightnew;
        public float Q_Height;
        public float Q_Heightnew;
        public float R_Height;
        public float R_Heightnew;

        public float Lim_P_Height;
        public float Lim_Q_Height;
        public float Lim_R_Height;

        private float LimHieght;

        float prevXRot = 0;
        public bool climbing = false;
        public bool down = false;
        public bool Serial = false;

        int SignalCount = 0;
        int SignalInt = 0;
        public bool SignalON = false;

        public static bool autoGear = false;
        public bool ODButton = false;
        //public Text GearValT;


        ///// Wiper Effect /////
        public bool wiperON = false;
        public bool wiperFast = false;
        public Animator WiperLAnimator;
        public Animator WiperRAnimator;
        public Animator WiperLCollAnimator;
        public Animator WiperRCollAnimator;

        // public static bool HandBrakePressed; //This variable is used to detect the hand brake input to activate and deactivate instructor panel indicators. This variable is used in the Meter_Panel script.
        public GameObject HandBrakeOnIndicatorInstructorPanel;
        public GameObject HeadLightOnIndicatorInstructorPanel;
        public GameObject HeadLightOnIndicatorMeterPanel;
        public GameObject ParkLightOnIndicatorInstructorPanel;

        public GameObject ODButtonIndicatorMeterPanel;

        private int leftSignal = 0;
        private int rightSignal = 0;
        public GameObject leftSignalOn; //get the left signal on indicator
        public GameObject rightSignalOn;//get the right signal on indicator
        public GameObject leftSignalOnInstructorPanel; //get the left signal on indicator. This is the right sign on the instructor panel. Activates with the serial & Keyboard Inputs.
        public GameObject rightSignalOnInstructorPanel;//get the right signal on indicator. This is the right sign on the instructor panel. Activates with the serial & Keyboard Inputs.

        Rigidbody MainVehicleRigidbody;

        //Meter Panel Speedmeter
        public float SpeedoMeter = 0.0f;
        float SpeedoFullAngle = 273;
        float SpeedoFullRange = 120;
        float steerfactorS = 0;
        public Transform Speedo;

        //Meter Panel Fuel Meter
        //public float FuelMeter = 0.0f;
        float FuelFullAngle = 56;
        float FuelFullRange = 40;
        float steerfactorFuel = 0;
        public Transform FuelM;


        public GameObject StartKeyOnInstructorPanel; //Indicate the start key On state on instructor panel.
        public GameObject IgnitionKeyOnInstructorPanel; //Indicate the ignition on state key on instructor panel.
        public GameObject StartKeyOffInstructorPanel; //Indicate the start key off state on instructor panel.
        public GameObject IgnitionKeyOffInstructorPanel; //Indicate the ignition off state key on instructor panel.

        public GameObject ParkingGearIndicatorInstructorPanel;
        public GameObject ReverseGearIndicatorInstructorPanel;
        public GameObject NeutralGearIndicatorInstructorPanel;
        public GameObject DriveGearIndicatorInstructorPanel;
        public GameObject SportGearIndicatorInstructorPanel;
        public GameObject LowGearIndicatorInstructorPanel;
        public GameObject FirstGearIndicatorInstructorPanel;
        public GameObject SecondGearIndicatorInstructorPanel;
        public GameObject ThirdGearIndicatorInstructorPanel;
        public GameObject FourthGearIndicatorInstructorPanel;
        public GameObject FifthGearIndicatorInstructorPanel;


        public Text VehicleCurrentSpeedText;
        public Text TotalTravelledDistanceMeterPanel;

        public float FuelLevel = 40.0f;
        public int FuelConsumption = 14;
        public float VehicleTravelledDistanceFuelMeter;
        public float VehicleTravelledDistanceTotal = 0.0f;

        float RemainingFuel;

        int TotalTravelledDistance;
        public float VehicleTravelledDistance;
        public int VehicleTravelledDistanceRoundedInt;

        public GameObject FuelIndicator;

        float elapsedtime;
        float elapsedtime1;
        private float StartTime = 0f;

        public float VehicleTravelledDistanceResettable;
        public float VehicleTravelledDistanceResettableFinal = 0.0f;
        int TravelledDistanceResettable;
        public int VehicleTravelledDistanceRoundedIntResettable;
        public Text ResettableTravelledDistanceMeterPanel;

        public GameObject FuelLevel1;
        public GameObject FuelLevel2;
        public GameObject FuelLevel3;
        public GameObject FuelLevel4;
        public GameObject FuelLevel5;
        public GameObject FuelLevel6;

        public GameObject AutoGearGearP;
        public GameObject AutoGearGearR;
        public GameObject AutoGearGearN;
        public GameObject AutoGearGearD;
        public GameObject AutoGearGearS;
        public GameObject AutoGearGearL;

        public GameObject ManualGearGearN;
        public GameObject ManualGearGear1;
        public GameObject ManualGearGear2;
        public GameObject ManualGearGear3;
        public GameObject ManualGearGear4;
        public GameObject ManualGearGear5;
        public GameObject ManualGearGearR;

        public string CurrentVehicleGear;


        public float TempMeter = 1.0f;
        float TempFullAngle = 56;
        float TempFullRange = 40;
        float steerfactorTemp = 0;
        public Transform TempM;

        public bool OverHeat;
        public GameObject OverHeatIndicator;
        public GameObject OverheatIndicatorOn; //This is the right sign on the instructor panel. Activates with the instructor panel buttons.

        public bool EngineOverHeatTrueInstructorPanel;


        public bool IgnitionOnByOverHeatButtonInstructorPanel = false;


        public float RPMMeter = 0.0f;
        float RPMFullAngle = 56;
        float RPMFullRange = 40;
        float steerfactorRPM = 0;
        public Transform RPMM;

        public float CurrentEngineRPM;

        // Assuming this is where you will store the potentiometer value of steering wheel
        public float potValue = 0;

        //// This is the variable you want to store the steering value in
        //public float Steer = 0f;


        public bool IgnitionOffForSeriaclReadScript;

        public bool SeatBelt;
        public GameObject SeatBeltIndicator;
        public GameObject SeatBeltIndicatorOn; //This is the right sign on the instructor panel. Activates with the instructor panel buttons.

        public GameObject HandbrakeIndicatorInstructorPanel;


        void OnEnable()
        {
            // Cache vehicle
            SerialReadScript = GetComponent<SerialCommunicationReadScript>();
            //SpeedMeter = GetComponent<SerialWrite>();

            ButtonScript = GameObject.FindGameObjectWithTag("GameManagerDilanUSJ").GetComponent<UIScript>();

            if (target == null)
                target = GetComponent<VehicleControllerGear>();

            // Read the rangeText if existing
            if (!File.Exists(Application.dataPath + "/rangeText.txt"))
            {
                for (int i = 0; i < RangeList.Length; i++)
                {
                    System.IO.File.AppendAllText(Application.dataPath + "/rangeText.txt", RangeList[i] + "\r\n");
                }
            }
            else
            {
                // If not existing write the default range values to the text
                TextLines = System.IO.File.ReadAllLines(Application.dataPath + "/rangeText.txt");

                foreach (string line in TextLines)
                {

                    string[] LineArray = line.Split('=');
                    if (LineArray[0] == "ClutchMin")
                    {
                        ClutchMin = int.Parse(LineArray[1]);
                    
                    }

                    if (LineArray[0] == "ClutchMax")
                    {
                        ClutchMax = int.Parse(LineArray[1]);
                    }

                    if (LineArray[0] == "AccMin")
                    {
                        AccMin = int.Parse(LineArray[1]);
                    }

                    if (LineArray[0] == "AccMax")
                    {
                        AccMax = int.Parse(LineArray[1]);
                    }

                    if (LineArray[0] == "BrakeMin")
                    {
                        BrakeMin = int.Parse(LineArray[1]);
                    }

                    if (LineArray[0] == "BrakeMax")
                    {
                        BrakeMax = int.Parse(LineArray[1]);
                    }
                }


            }
        }

        void Start()
        {


            Invoke("DelayedAction", 0.005f);

            OverheatIndicatorOn.SetActive(false);

            //HandBrakePressed = false;

            // Get the Main vehicle Rigidbody component
            MainVehicleRigidbody = GetComponent<Rigidbody>();

            IgnitionKeyOnInstructorPanel.SetActive(false);
            IgnitionKeyOffInstructorPanel.SetActive(true);

            StartKeyOnInstructorPanel.SetActive(false);
            StartKeyOffInstructorPanel.SetActive(true);

            ParkingGearIndicatorInstructorPanel.SetActive(false);
            ReverseGearIndicatorInstructorPanel.SetActive(false);
            NeutralGearIndicatorInstructorPanel.SetActive(false);
            DriveGearIndicatorInstructorPanel.SetActive(false);
            SportGearIndicatorInstructorPanel.SetActive(false);
            LowGearIndicatorInstructorPanel.SetActive(false);

            FuelLevelTextFileRead();
            FuelLevel = RemainingFuel;

            TotalTravelledDistanceTextFileRead(); //Meter Panel
            VehicleTravelledDistanceTotal = TotalTravelledDistance;

            ResettableTravelledDistanceTextFileRead();//Meter Panel
            VehicleTravelledDistanceResettableFinal = TravelledDistanceResettable;


            if (IgnitionIsON && StartTime == 0f)
            {
                StartTime = Time.time;
                //Debug.Log("Main Vehicle time is " + StartTime);
            }

            elapsedtime = Time.time - StartTime;
            elapsedtime1 = Time.time - StartTime;

            //if (IgnitionIsON) {
            //    lastPosition = transform.position;

            //    totalDistance = 0;
            //}

        }

        void Update()
        {
            //RPMMeterOperation();
            //SetRPMMeter(CurrentEngineRPM);

            //TempMeterOperation();

            //HeadLightKeyBoard();
            //SignalLight();
            //InstructorPanelCurrentGearIndicator();

            //// Calculate the main vehicle speed in km/h
            //SpeedoMeter = MainVehicleRigidbody.velocity.magnitude * 3.6f;
            ////Debug.Log("Speed: " + SpeedoMeter + " km/h");

            //double SpeedoMeterRounded = Math.Round(SpeedoMeter, 0);

            //VehicleCurrentSpeedText.text = SpeedoMeterRounded.ToString() + " kmph";

            //SetSpeedo();
            //SetFuelMeter();

            //FuelMeterOperation();

            //if (OverHeat == true)
            //{

            //    OverHeatIndicator.SetActive(true);
            //}
            //else
            //{
            //    OverHeatIndicator.SetActive(false);
            //}



            //if (IgnitionIsON)
            //{
            //    //Debug.Log("Main Vehicle time is " + elapsedtime);
            //    elapsedtime = Time.time - elapsedtime;
            //    VehicleTravelledDistance = ((SpeedoMeter * (elapsedtime / 3600)));
            //    VehicleTravelledDistanceTotal = VehicleTravelledDistanceTotal + VehicleTravelledDistance;
            //    double VehicleTravelledDistanceRounded = Math.Round(VehicleTravelledDistanceTotal, 0);
            //    VehicleTravelledDistanceRoundedInt = (int)VehicleTravelledDistanceRounded;
            //    TotalTravelledDistanceMeterPanel.text = (VehicleTravelledDistanceRoundedInt/1000).ToString("D6");

            //}

            //if (IgnitionIsON)
            //{
            //    //Debug.Log("Main Vehicle time is " + elapsedtime);
            //    elapsedtime1 = Time.time - elapsedtime1;
            //    VehicleTravelledDistanceResettable = ((SpeedoMeter * (elapsedtime1 / 3600)));
            //    VehicleTravelledDistanceResettableFinal = VehicleTravelledDistanceResettableFinal + VehicleTravelledDistanceResettable;
            //    double VehicleTravelledDistanceRoundedResettable = Math.Round(VehicleTravelledDistanceResettableFinal, 3);
            //    VehicleTravelledDistanceRoundedIntResettable = (int)VehicleTravelledDistanceRoundedResettable;
            //    ResettableTravelledDistanceMeterPanel.text = (VehicleTravelledDistanceRoundedIntResettable/1000).ToString("D4");

            //}

            //SeatBelt Indicators Operation
            if (SerialReadScript.SeatBelt == 1)
            {
                SeatBelt = true;
                SeatBeltIndicator.SetActive(true);
                SeatBeltIndicatorOn.SetActive(true);
            }

            else
            {
                SeatBelt = false;
                SeatBeltIndicator.SetActive(false);
                SeatBeltIndicatorOn.SetActive(false);
            }

            //HandBrake Indicator in instructor Panel Operation
            if (SerialReadScript.HandBrake == 1)
            {

                HandbrakeIndicatorInstructorPanel.SetActive(true);

            }

            else if (SerialReadScript.HandBrake == 0)
            {
                HandbrakeIndicatorInstructorPanel.SetActive(false);
            }


            //Return if there is no target script
            if (target == null) return;


            //Check for Serial data
            if (SerialReadScript.Serial == false)
            {
                Serial = false;
            }
            else
            {
                Serial = true;
            }

            if (!Serial)
            
            {
                //Initialy set all inputs to zero
                AppliedSteer = target.steerAngle;

                steerInputb = 0.0f;
                handbrakeInputb = 0.0f;
                throttleInputb = 0.0f;
                brakeInputb = 0.0f;

                //Get the key inputs
                Get_KeyInputs();
                //HeadLightKeyBoard();

                //Activate and Deactivate System On Indicator in instructor panel
                if (SystemIsON)
                {
                    StartKeyOnInstructorPanel.SetActive(true);
                    StartKeyOffInstructorPanel.SetActive(false);
                }

                else
                {
                    StartKeyOnInstructorPanel.SetActive(false);
                    StartKeyOffInstructorPanel.SetActive(true);
                }

                //Activate and Deactivate Ignition On Indicator in instructor panel
                if (IgnitionIsON)
                {
                    IgnitionKeyOnInstructorPanel.SetActive(true);
                    IgnitionKeyOffInstructorPanel.SetActive(false);
                }

                else
                {
                    IgnitionKeyOnInstructorPanel.SetActive(false);
                    IgnitionKeyOffInstructorPanel.SetActive(true);
                }



                if (SystemIsON)
                {
                    if (IgnitionIsON)
                    {
                        if (handbrakeInputb == 1)
                        {
                            target.maxBrakeForce = HandBrakeForce;
                        }
                        else if (brakeInputb == 1)
                            target.maxBrakeForce = FootBrakeTorque[index];
                        else
                        {
                            if (CurrentBTRSpeed > MaxSpeed)
                            {
                                target.maxBrakeForce = 4500;
                            }
                            else
                            {
                                target.maxBrakeForce = DecelerationForce;
                            }
                        }

                        if (!autoGear)
                        {
                            //GearBox :D Set Gear number

                            if (Input.GetKey(KeyCode.RightShift))
                                Clutch = true;
                            else
                                Clutch = false;

                            if (Clutch)
                            {
                                if (Input.GetKeyDown("2"))
                                    index = 0;

                                if (Input.GetKeyDown("3"))
                                    index = 1;

                                if (Input.GetKeyDown("4"))
                                    index = 2;

                                if (Input.GetKeyDown("5"))
                                    index = 3;

                                if (Input.GetKeyDown("6"))
                                    index = 4;

                                if (Input.GetKeyDown("7"))
                                    index = 5;

                                if (Input.GetKeyDown("8"))
                                    index = 6;

                            }

                            //Set the maximum speed and maximum mortorTorque according to the selected gear number

                            MaxSpeed = GearMaxSpeed[index];
                            MaximumTorque = GearMaxTorque[index];
                            target.maxSpeedForward = MaxSpeed / 3.6f;


                            //Set the current motorTorques and maximum speeds according to the current speed and gear number

                            if (target.speed * 3.6f > -target.maxSpeedReverse && handbrakeInputb == 0 && brakeInputb == 0 && throttleInputb == 1.0f)
                            {

                                if (Mathf.Round(target.speed) * 3.6f == 0)
                                {

                                    if (index > 1 && index < 6)
                                    {
                                        CurrentMotorTorque = 0.0f;
                                    }
                                    else if (index == 1 && target.speed * 3.6f < MaxSpeed)
                                    {
                                        CurrentMotorTorque = GearMaxTorque[1];
                                        target.maxSpeedForward = MaxSpeed / 3.6f;
                                    }

                                    else if (index == 6 && Mathf.Abs(target.speed * 3.6f) < MaxSpeed)
                                    {
                                        CurrentMotorTorque = GearMaxTorque[6];
                                        //if (Input.GetKey(ThrottleKey))
                                        // {
                                        //throttleInputb = -1.0f * throttleInputb;
                                        //}
                                    }
                                }

                                else if ((Mathf.Round(target.speed) * 3.6f) != 0 && index != 6)
                                {
                                    target.maxSpeedForward = MaxSpeed / 3.6f;
                                    CurrentMotorTorque = MaximumTorque;
                                }

                                else if (index == 6 && Mathf.Abs(target.speed * 3.6f) < MaxSpeed)
                                {
                                    CurrentMotorTorque = GearMaxTorque[6];
                                    //if (Input.GetKey(ThrottleKey))
                                    //{
                                    //throttleInputb = -1.0f * throttleInputb;
                                    //}

                                }
                            }
                        }
                        else
                        {
                            //Enable the OD Button Feature
                            if (Input.GetKeyDown(KeyCode.O))
                            {
                                ODButton = !ODButton;
                                if (ODButtonIndicatorMeterPanel.activeSelf == false && ODButton == true)
                                {
                                    ODButtonIndicatorMeterPanel.SetActive(true);
                                }
                                else if (ODButtonIndicatorMeterPanel.activeSelf == true && ODButton == false)
                                {
                                    ODButtonIndicatorMeterPanel.SetActive(false);
                                }
                            }

                            // set the gear value
                            if (Input.GetKeyDown("2") || SerialReadScript.CurrentGear == "P")
                                index = 0; // P

                            if (Input.GetKeyDown("3") || SerialReadScript.CurrentGear == "R")
                                index = 1; // R

                            if (Input.GetKeyDown("4") || SerialReadScript.CurrentGear == "N")
                                index = 2; // N

                            if (Input.GetKeyDown("5") || SerialReadScript.CurrentGear == "D")
                                index = 3; // D

                            //MaxSpeed = 90;
                            target.maxSpeedForward = AutoFrwrdMaxSpeed / 3.6f;
                            target.maxSpeedReverse = AutoRvrseMaxSpeed / 3.6f;

                            if (index == 0 || index == 2)
                            {
                                CurrentMotorTorque = AutoGearMaxTorque[index];
                            }
                            else if (index == 1)
                            {
                                if (Mathf.Abs(target.speed * 3.6f) < AutoRvrseMaxSpeed)
                                {
                                    CurrentMotorTorque = AutoGearMaxTorque[index];
                                }
                                else
                                {
                                    CurrentMotorTorque = 0;
                                }

                            }
                            else
                            {
                                if (Mathf.Abs(target.speed * 3.6f) < AutoFrwrdMaxSpeed)
                                {
                                    CurrentMotorTorque = AutoGearMaxTorque[index];
                                }
                                else
                                {
                                    CurrentMotorTorque = 0;
                                }
                            }

                        }

                    }

                    else
                    {
                        CurrentMotorTorque = 0;
                    }

                    HeadLight();
                    BrakeLight();
                    //SignalLight();


                    //Wiper Function
                    if (Input.GetKey(KeyCode.L))
                    {
                        wiperON = !wiperON;
                    }

                    if (wiperON)
                    {
                        WiperLAnimator.SetBool("IsOn", true);
                        WiperRAnimator.SetBool("IsOn", true);
                        WiperLCollAnimator.SetBool("IsOn", true);
                        WiperRCollAnimator.SetBool("IsOn", true);

                        if (Input.GetKey(KeyCode.P))
                        {
                            wiperFast = !wiperFast;
                        }

                        if (wiperFast)
                        {
                            WiperLAnimator.speed = 1.2f;
                            WiperRAnimator.speed = 1.2f;
                            WiperLCollAnimator.speed = 1.2f;
                            WiperRCollAnimator.speed = 1.2f;
                        }
                        else
                        {
                            WiperLAnimator.speed = 0.8f;
                            WiperRAnimator.speed = 0.8f;
                            WiperLCollAnimator.speed = 0.8f;
                            WiperRCollAnimator.speed = 0.8f;
                        }
                    }
                    else
                    {
                        wiperFast = false;

                        WiperLAnimator.SetBool("IsOn", false);
                        WiperRAnimator.SetBool("IsOn", false);
                        WiperLCollAnimator.SetBool("IsOn", false);
                        WiperRCollAnimator.SetBool("IsOn", false);
                    }
                }

                else
                {
                    CurrentMotorTorque = 0;
                }

            }

            //If Serial data is available//////////////////////////////.........................
            else
            {
                ThrottleSerial = SerialReadScript.AcceleratorArduino;
                BrakeSerial = SerialReadScript.Brake;

                if (!autoGear)
                {
                    ClutchSerial = SerialReadScript.ClutchArduino;

                    if (!ButtonScript.SetRangesMode && !SetMinMax)
                    {
                        //Read the clutch input and set the clutch state
                        // Clutch variable is set between 0 and 10
                        //float clutchInput = Mathf.Clamp((SerialReadScript.ClutchInput - 20) * 10 / 560, 0, 10);
                        //clutchInput = Mathf.Clamp(Mathf.Abs(ClutchSerial - ClutchMin) * 10 / Mathf.Abs(ClutchMax - ClutchMin), 0, 10);


                        float ClutchInputCenter = ClutchMin + (Mathf.Abs(ClutchMax - ClutchMin) / 3);
                        if (ClutchSerial < ClutchInputCenter)
                        {
                            if (ClutchSerial < ClutchMin)
                            {
                                clutchInput = 0;
                            }
                            else
                            {
                                clutchInput = Mathf.Clamp(Mathf.Abs(ClutchSerial - ClutchMin) * 10 / ((Mathf.Abs(ClutchMax - ClutchMin)) / 3), 0, 10);

                            }
                            //brakeInputb = Mathf.Abs(BrakeSerial - BrakeMin) / (Mathf.Abs(BrakeMax - BrakeMin) / 2);
                        }
                        else
                        {
                            clutchInput = 10;
                        }

                        if (clutchInput <= 7)
                        {
                            if (clutchInput > 5)
                                ClutchState = "Into Balance";
                            else if (clutchInput <= 4 && clutchInput > 0.2f)
                                ClutchState = "Out from Balance";
                            else if (clutchInput <= 0.2f)
                                ClutchState = "Released";
                            else
                                ClutchState = "Balanced";

                            //if (clutchInput > 5)
                            //    ClutchState = "Into Balance";
                            //else if (clutchInput <= 4 && clutchInput > 1f)
                            //    ClutchState = "Out from Balance";
                            //else if (clutchInput <= 1f)
                            //    ClutchState = "Released";
                            //else
                            //    ClutchState = "Balanced";
                        }
                        else
                        {
                            ClutchState = "Fully Pressed";

                            //index = int.Parse(SerialReadScript.CurrentGear);
                            CurrentMotorTorque = 0;

                            MaxSpeed = GearMaxSpeed[index];
                            MaximumTorque = GearMaxTorque[index];
                            target.maxSpeedForward = MaxSpeed / 3.6f;
                        }
                    }
                }
                else
                {
                    // Auto Gear Mode
                    // set the gear
                    //index = int.Parse(SerialReadScript.CurrentGear);

                    // set the gear value
                    if (SerialReadScript.CurrentGear == "P")
                        index = 0; // P

                    if (SerialReadScript.CurrentGear == "R")
                        index = 1; // R

                    if (SerialReadScript.CurrentGear == "N")
                        index = 2; // N

                    if (SerialReadScript.CurrentGear == "D")
                        index = 3; // D


                    CurrentMotorTorque = AutoGearMaxTorque[index];
                    target.maxSpeedForward = AutoFrwrdMaxSpeed / 3.6f;
                    target.maxSpeedReverse = AutoRvrseMaxSpeed / 3.6f;

                    //if (index == 0 || index == 2)
                    //{
                    //    CurrentMotorTorque = AutoGearMaxTorque[index];
                    //}
                    //else if (index == 1)
                    //{
                    //    if (Mathf.Abs(target.speed * 3.6f) < AutoRvrseMaxSpeed)
                    //    {
                    //        CurrentMotorTorque = AutoGearMaxTorque[index];
                    //    }
                    //    else
                    //    {
                    //        CurrentMotorTorque = 0;
                    //    }

                    //}
                    //else
                    //{
                    //    if (Mathf.Abs(target.speed * 3.6f) < AutoFrwrdMaxSpeed)
                    //    {
                    //        CurrentMotorTorque = AutoGearMaxTorque[index];
                    //    }
                    //    else
                    //    {
                    //        CurrentMotorTorque = 0;
                    //    }
                    //}
                    // P = 0, R = 1, N = 2, D = 3

                    //ODButton = (SerialReadScript.ODButtonVal == 1) ? true : false;

                }

                if (!ButtonScript.SetRangesMode && !SetMinMax)
                {

                    float SteerSerialIn = SerialReadScript.SteeringWheel;
                  

                    //if (SteerSerialIn <= 547)
                    //{
                    //    //Steer = (SerialReadScript.SteerAngleInput - 503f) / 309f;
                    //    if (SteerSerialIn < 365)
                    //        Steer = -1;
                    //    else
                    //        Steer = (SteerSerialIn - 547f) / 182f;
                    //}
                    //else if (SteerSerialIn >= 553)
                    //{
                    //    //Steer = (SerialReadScript.SteerAngleInput - 503f) / 309f;
                    //    if (SteerSerialIn > 735)
                    //        Steer = 1;
                    //    else
                    //        Steer = (SteerSerialIn - 553f) / 182f;
                    //}
                    //else
                    //    Steer = 0;


                    /*  potValue = SteerSerialIn;
                      float MapPotValueToSteer(float potValue)
                      {
                          // Since the potentiometer gives values between 0 and 1024 for one cycle,
                          // and the steering wheel rotates 2.5 cycles in both directions,
                          // we need to map the potentiometer value to a value between -1 and 1.
                          // First, normalize potValue to a value between 0 and 1.
                          float normalizedPotValue = potValue / 4096f;

                          // Since the steering wheel rotates 2.5 cycles in both directions,
                          // we multiply the normalizedPotValue by 2.5 and subtract 1.25 to get a value between -1.25 and 1.25.
                          // Then, we clamp this value to be between -1 and 1.
                          float steerValue = Mathf.Clamp(normalizedPotValue * 4.0f-2f, -1f, 1f);
                          //float steerValue = Mathf.Clamp(normalizedPotValue * 5f, -1f, 1f);

                           return steerValue;
                      }

                      // Map the potentiometer value to the steering valuer

                      Steer = MapPotValueToSteer(potValue);*/
                    /*float PreviousSteer;
                    float curr_steer = SteerSerialIn;
                    void DelayedAction()
                    {
                        //Debug.Log("After Delay");

                        if (PreviousSteer > potValue)
                        {
                            Steer = -1;
                        }
                        else if (PreviousSteer < potValue)
                        {
                            Steer = 1;
                        }

                    }*/
                    //float PreviousSteer;
                      curr_steer = SteerSerialIn;

                    //Debug.Log("After Delay");

                    //if (PreviousSteer - curr_steer < 20)
                    //{
                    //    Steer = 0;
                    //}
                    //else
                    //{

                        if (PreviousSteer - curr_steer > 0)
                        {
                            Steer = -1;
                        }
                        else if (PreviousSteer - curr_steer < 0)
                        {
                            Steer = 1;
                        }
                    /* else
                     {
                         Steer = 0;
                     }*/
                    //}

                    //if (SteerSerialIn <= 2140)
                    //{
                    //steerInputb = (((35f / 1740f) * SteerSerialIn) - 43f);
                    steerInputb = ((0.02f* SteerSerialIn) - 43f)/35f;
                    //Debug.Log("steerINPUTBBBB" + steerInputb);
                  //
                  //}
                    //else
                    //{
                    //    steerInputb = 0;
                    //}

                    //if (Steer < 0)
                    //{
                    //    //steerInputb = (35 / 2048) * SteerSerialIn - 35f;
                    //    //steerInputb = -0.968f * Mathf.Pow(Mathf.Abs(Steer), 2.33f);
                    //    //steerInputb = -Mathf.Pow(Mathf.Abs(Steer), 1.8f);

                    //    //if (CurrentBTRSpeed < 0)
                    //    //{
                    //    //    steerInputb = -Mathf.Pow(Mathf.Abs(Steer), 1f);
                    //    //    //steerInputb = -(0.089f + 1.97f * (Mathf.Abs(Steer)) - 1.12f * Mathf.Pow(Mathf.Abs(Steer), 2f));
                    //    //}
                    //    //else if (CurrentBTRSpeed > 100)
                    //    //{
                    //    //    steerInputb = (-Mathf.Pow(Mathf.Abs(Steer), 1f))/2;
                    //    //}
                    //    //else
                    //    //{
                    //    //    steerInputb = (-Mathf.Pow(Mathf.Abs(Steer), 1f)) / ((CurrentBTRSpeed/100) + 1);
                    //    //}

                    //    steerInputb = -Mathf.Pow(Mathf.Abs(Steer), 1f);
                    //    SteerAngle = steerInputb * 35;
                    //}
                    //else
                    //{

                    //    //steerInputb = (35 / 2048) * SteerSerialIn - 35f;
                    //    //steerInputb = 0.968f * Mathf.Pow(Mathf.Abs(Steer), 2.33f);
                    //    //steerInputb = Mathf.Pow(Mathf.Abs(Steer), 1.8f);

                    //    //if (CurrentBTRSpeed < 0)
                    //    //{
                    //    //    steerInputb = Mathf.Pow(Mathf.Abs(Steer), 1f);
                    //    //}
                    //    //else if (CurrentBTRSpeed > 100)
                    //    //{
                    //    //    steerInputb = (Mathf.Pow(Mathf.Abs(Steer), 1f)) / 2;
                    //    //}
                    //    //else
                    //    //{
                    //    //    steerInputb = (Mathf.Pow(Mathf.Abs(Steer), 1f)) / ((CurrentBTRSpeed / 100) + 1);
                    //    //}

                    //    steerInputb = Mathf.Pow(Mathf.Abs(Steer), 1f);
                    //    SteerAngle = steerInputb * 35;
                    //}
                     


                    //Read the serial brake inputs
                    // Brake variable is set between 0 and 1

                    //if (SerialReadScript.FootBrakeInput < 400)
                    //{
                    //    brakeInputb = SerialReadScript.FootBrakeInput / 400;
                    //}
                    //else
                    //{
                    //    brakeInputb = 1;
                    //}

                    float BrakeInputCenter = BrakeMin - Mathf.Abs(BrakeMax - BrakeMin) / 2;
                    if (BrakeSerial > BrakeInputCenter)
                    {
                        if (BrakeSerial > BrakeMin)
                        {
                            brakeInputb = 0;
                        }
                        else
                        {
                            brakeInputb = Mathf.Abs(BrakeSerial - BrakeMin) / (Mathf.Abs(BrakeMax - BrakeMin) / 2);


                        }
                    }
                    else
                    {
                        brakeInputb = 1;
                    }


                    //brakeInputb = SerialReadScript.FootBrakeTorqueInput / 300;
                    handbrakeInputb = SerialReadScript.HandBrake;


                    //Set the Brake force of the vehicle according to the braking state

                    if (handbrakeInputb > 0)
                    {
                        target.maxBrakeForce = HandBrakeForce;
                    }
                    else if (brakeInputb > 0)
                    {
                        target.maxBrakeForce = FootBrakeTorque[index];
                    }
                    else
                    {
                        if (!autoGear)
                        {
                            if (SerialIgnitionIsOn)
                            {
                                // a brake force is added when the vehicle is moving faster than the max speed in the current gear
                                if (CurrentBTRSpeed > MaxSpeed)
                                {
                                    target.maxBrakeForce = 4500;
                                }
                                // a brake force is added when there is no any throttle input
                                else
                                {
                                    target.maxBrakeForce = DecelerationForce;
                                }
                            }
                            else
                            {
                                target.maxBrakeForce = DecelerationForce;
                            }
                        }
                        else
                        {
                            // In Parking Gear a slight brake force is applied
                            if (index == 0)
                            {
                                target.maxBrakeForce = 4500;
                            }
                            else
                            {
                                target.maxBrakeForce = DecelerationForce;
                            }
                        }


                    }

                    //Read the serial throttle input
                    // Accl variable is set between 0 and 10
                    //if (SerialReadScript.AcceleratorInput >= 400)
                    //{
                    //    throttleInput = Mathf.Clamp((1000 - SerialReadScript.AcceleratorInput) * 10 / 600, 0, 10);
                    //}
                    //else
                    //    throttleInput = 10;

                    float ThrottleInputCenter = AccMin + Mathf.Abs(AccMax - AccMin) / 2.5f;
                    if (ThrottleSerial <= ThrottleInputCenter)
                    {
                        if (ThrottleSerial < AccMin)
                        {
                            ThrottleInput = 0;
                        }
                        else
                        {
                            ThrottleInput = Mathf.Abs(ThrottleSerial - AccMin) * 10 / (Mathf.Abs(AccMax - AccMin) / 2);

                        }
                    }
                    else
                        ThrottleInput = 10;

                    SerialSystemIsON = (SerialReadScript.EngineStart == 1) ? 1 : 0;
                    int TheSystemOn = (SerialReadScript.SystemOn);

                    //if the system is ON
                    if (TheSystemOn == 1)
                    {
                        ButtonScript.IgnitionVal = "1";
                        //Set the Ignition
                        CurrentIgnition = SerialReadScript.Ignition;

                        if (CurrentIgnition != 0  && !SerialIgnitionIsOn)
                        {
                            SerialIgnitionIsOn = true;

                        }
                        PrevSerialIgnition = CurrentIgnition;

                        //Activate and Deactivate Ignition On Indicator in instructor panel
                        if (SerialIgnitionIsOn)
                        {
                            IgnitionKeyOnInstructorPanel.SetActive(true);
                            IgnitionKeyOffInstructorPanel.SetActive(false);
                        }

                        else
                        {
                            IgnitionKeyOnInstructorPanel.SetActive(false);
                            IgnitionKeyOffInstructorPanel.SetActive(true);
                        }

                        if (SerialIgnitionIsOn)
                        {                           
                            ButtonScript.StartKeyVal = "1";

                            if (!autoGear)
                            {
                                SetMotorTorque();
                                SetThrottleInput();
                            }
                            else
                            {
                                AutomaticGear();
                            }

                        }
                        else
                        {
                            ButtonScript.StartKeyVal = "0";
                        }

                        //If the clutch pedal is fully pressed (Gear can be changed , Motor torque to the vehicle is zero)


                    }


                    else
                    {
                        //If the clutch pedal is fully pressed (Gear can be changed , Motor torque to the vehicle is zero)

                        CurrentMotorTorque = 0;
                        SerialIgnitionIsOn = false;
                        ButtonScript.IgnitionVal = "0";
                        ButtonScript.StartKeyVal = "0";

                        SignalRightFront.enabled = false;
                        SignalRightBack.enabled = false;
                        SignalLeftFront.enabled = false;
                        SignalLeftBack.enabled = false;
                    }

                    HeadLight();
                    //SignalLight();
                }

                //Wiper Function with Serial Data

                //if (Input.GetKey(KeyCode.L))
                //{
                //    wiperON = !wiperON;
                //}

                if (SerialReadScript.WiperHigh == 1 || SerialReadScript.WiperLow == 1 || SerialReadScript.WiperInt == 1)
                {
                    WiperLAnimator.SetBool("IsOn", true);
                    WiperRAnimator.SetBool("IsOn", true);
                    WiperLCollAnimator.SetBool("IsOn", true);
                    WiperRCollAnimator.SetBool("IsOn", true);

                    //if (Input.GetKey(KeyCode.P))
                    //{
                    //    wiperFast = !wiperFast;
                    //}

                    if (SerialReadScript.WiperHigh == 1)
                    {
                        WiperLAnimator.speed = 1.2f;
                        WiperRAnimator.speed = 1.2f;
                        WiperLCollAnimator.speed = 1.2f;
                        WiperRCollAnimator.speed = 1.2f;
                    }
                    else
                    {
                        WiperLAnimator.speed = 0.8f;
                        WiperRAnimator.speed = 0.8f;
                        WiperLCollAnimator.speed = 0.8f;
                        WiperRCollAnimator.speed = 0.8f;
                    }
                }
                else
                {
                    //wiperFast = false;

                    WiperLAnimator.SetBool("IsOn", false);
                    WiperRAnimator.SetBool("IsOn", false);
                    WiperLCollAnimator.SetBool("IsOn", false);
                    WiperRCollAnimator.SetBool("IsOn", false);
                }

                PreviousSteer = curr_steer;

            }

            //Activate and Deactivate System On Indicator in instructor panel
            if (SerialReadScript.SystemOn != 0)
            {
                StartKeyOnInstructorPanel.SetActive(true);
                StartKeyOffInstructorPanel.SetActive(false);
            }

            else
            {
                StartKeyOnInstructorPanel.SetActive(false);
                StartKeyOffInstructorPanel.SetActive(true);
            }


            //Set the Gear value to the text
            ButtonScript.GearVal = index.ToString(); //................................................


            // Apply input to vehicle
            target.steerInput = steerInputb;
            target.throttleInput = throttleInputb;
            target.brakeInput = brakeInputb;
            target.handbrakeInput = handbrakeInputb;
            target.maxDriveForce = CurrentMotorTorque;

            // Transfer the position and rotation of the tank from server to network object
            //networkObject.Position = transform.position;
            //networkObject.Rotation = transform.rotation;

            // Do the operation of RangesModeON button
            SetSerialMinMaxString();
            //}


            //If Serial data is not available



            // Do a vehicle reset

            if (m_doReset)
            {
                target.ResetVehicle();
                m_doReset = false;
            }


            RPMMeterOperation();
            SetRPMMeter(CurrentEngineRPM);

            TempMeterOperation();

            HeadLightKeyBoard();

            SignalLight();
            InstructorPanelCurrentGearIndicator();

            // Calculate the main vehicle speed in km/h
            SpeedoMeter = MainVehicleRigidbody.velocity.magnitude * 3.6f;
            //Debug.Log("Speed: " + SpeedoMeter + " km/h");

            double SpeedoMeterRounded = Math.Round(SpeedoMeter, 0);

            VehicleCurrentSpeedText.text = SpeedoMeterRounded.ToString() + " kmph";

            SetSpeedo();
            SetFuelMeter();

            FuelMeterOperation();

            if (OverHeat == true)
            {

                OverHeatIndicator.SetActive(true);
            }
            else
            {
                OverHeatIndicator.SetActive(false);
            }


            if (IgnitionIsON)
            {
                //Debug.Log("Main Vehicle time is " + elapsedtime);
                elapsedtime = Time.time - elapsedtime;
                VehicleTravelledDistance = ((SpeedoMeter * (elapsedtime / 3600)));
                VehicleTravelledDistanceTotal = VehicleTravelledDistanceTotal + VehicleTravelledDistance;
                double VehicleTravelledDistanceRounded = Math.Round(VehicleTravelledDistanceTotal, 0);
                VehicleTravelledDistanceRoundedInt = (int)VehicleTravelledDistanceRounded;
                TotalTravelledDistanceMeterPanel.text = (VehicleTravelledDistanceRoundedInt / 1000).ToString("D6");

            }

            if (IgnitionIsON)
            {
                //Debug.Log("Main Vehicle time is " + elapsedtime);
                elapsedtime1 = Time.time - elapsedtime1;
                VehicleTravelledDistanceResettable = ((SpeedoMeter * (elapsedtime1 / 3600)));
                VehicleTravelledDistanceResettableFinal = VehicleTravelledDistanceResettableFinal + VehicleTravelledDistanceResettable;
                double VehicleTravelledDistanceRoundedResettable = Math.Round(VehicleTravelledDistanceResettableFinal, 3);
                VehicleTravelledDistanceRoundedIntResettable = (int)VehicleTravelledDistanceRoundedResettable;
                ResettableTravelledDistanceMeterPanel.text = (VehicleTravelledDistanceRoundedIntResettable / 1000).ToString("D4");

            }
           

        }


        void AutomaticGear()
        {
            // Set the Mortor Torque

            //// When the vehicle is not moving
            //if ((Mathf.Round(target.speed) * 3.6f) == 0)
            //{

            //}
            //// When the vehicle is moving
            //{

            //}

            if (index != 1 || CurrentMotorTorque == 0)
            {
                throttleInputb = ThrottleInput / 10;
                //throttleInputb = Mathf.Pow(Mathf.Abs(throttleInput / 10), 1.2f);
                
            }
            else if (index == 1)
            {
                //target.maxSpeedReverse = -MaxSpeed;
                throttleInputb = -ThrottleInput / 10;
                //throttleInputb = -Mathf.Pow(Mathf.Abs(throttleInput / 10), 1.2f);
            }

        }



        void SetMotorTorque()
        {
            // When the tank is not moving
            if ((Mathf.Round(target.speed) * 3.6f) == 0)
            {
                // When the tank has a gear and clutch is not pressed
                if ((index != 0) && (ClutchState == "Released"))  //((index != 0 && !Low8nuetral) && (ClutchState == "Released"))
                {
                    CurrentMotorTorque = 0;
                    SerialIgnitionIsOn = false;
                    Debug.Log("IM WORKING.............................");
                    return;
                }
                else if (index == 1 || index == 6)  //(((index == 1 || index == 6) && !Low8nuetral) || index == 7)
                {
                    // Mortor torque is changed according to clutch input
                    CurrentMotorTorque = MaximumTorque * Mathf.Clamp(((10 - clutchInput) / 10), 0, 1);
                    //CurrentMotorTorque = MaximumTorque * Mathf.Clamp(((clutchInput) / 9), 0, 1);
                }
                else
                {
                    CurrentMotorTorque = 0;
                }
            }
            // When the tank is moving
            else
            {
                if (ClutchState == "Fully Pressed" || (index == 6 && CurrentBTRSpeed < -GearMaxSpeed[6]))
                {
                    CurrentMotorTorque = 0;
                }
                else if (ClutchState != "Fully Pressed" && (index == 4 || index == 5) && (target.speed) * 3.6f < 20 && throttleInputb > 0.4f)
                {
                    CurrentMotorTorque = 0;
                    SerialIgnitionIsOn = false;
                    Debug.Log("IM WORKING Newwww.............................");
                }
                else
                {
                    //float CurrentTorque = MaximumTorque * Mathf.Clamp(((10 - clutchInput) / 10), 0, 1);
                    //CurrentMotorTorque = CurrentTorque * Mathf.Clamp(Mathf.Abs(throttleInputb), 0, 1);
                    CurrentMotorTorque = MaximumTorque * (9.0f - clutchInput) / 9;
                }

            }
        }

        void SetThrottleInput()
        {
            //if ((ClutchState != "Released" && ClutchState != "Fully Pressed") && throttleInputb < 0.3 && ((index == 1 && target.speed * 3.6f < GearMaxSpeed[1]) || (index == 6 && target.speed * 3.6f > -GearMaxSpeed[6])) && handbrakeInputb < 1 && brakeInputb < 2)
            //{
            //    CluthBalance = true;
            //    //CurrentMotorTorque = MaximumTorque * (9.0f - clutchInput) / 9;
            //    target.maxSpeedForward = 8 / 3.6f;
            //    target.maxSpeedReverse = 8 / 3.6f;
            //    if (index == 1)
            //        throttleInputb = 0.33f;
            //    else if (index == 6)
            //        throttleInputb = -(0.33f);
            //}
            if (index == 0 || CurrentMotorTorque == 0)
            {
                throttleInputb = ThrottleInput / 10;
            }
            else if (index != 6)
            {
                throttleInputb = ThrottleInput / 10;
                //throttleInputb = Mathf.Pow(Mathf.Abs(throttleInput / 10), 1.2f);
            }
            else if (index == 6)
            {
                //target.maxSpeedReverse = -MaxSpeed;
                throttleInputb = -ThrottleInput / 10;
                //throttleInputb = -Mathf.Pow(Mathf.Abs(throttleInput / 10), 1.2f);
            }

        }

        // Update the Instructor panel
        void SetSerialMinMaxString()
        {
            //ClutchMinText.text = ClutchMin.ToString();
            //ClutchMaxText.text = ClutchMax.ToString();
            //AccMinText.text = AccMin.ToString();
            //AccMaxText.text = AccMax.ToString();
            //BrakeMinText.text = BrakeMin.ToString();
            //BrakeMaxText.text = BrakeMax.ToString();
        }

        // Add the serial data to the lists
        void GetRanges()
        {
            ClutchList.Add(ClutchSerial);
            AccList.Add(ThrottleSerial);
            BrakeList.Add(BrakeSerial);
        }
        // sort out the minimum and maximum from the lists
        void GetRangeMinMax()
        {
            ClutchList.Sort();
            ClutchMin = ClutchList[0];
            //ClutchMin = ClutchList[ClutchList.Count - 1];
            RangeList[0] = "ClutchMin=" + ClutchMin.ToString();
            ClutchMax = ClutchList[ClutchList.Count - 1];
            //ClutchMax = ClutchList[0];
            RangeList[1] = "ClutchMax=" + ClutchMax.ToString();
            ClutchList.Clear();

            AccList.Sort();
            AccMin = AccList[0];
            //AccMin = AccList[AccList.Count - 1];
            RangeList[2] = "AccMin=" + AccMin.ToString();
            AccMax = AccList[AccList.Count - 1];
            //AccMax = AccList[0];
            RangeList[3] = "AccMax=" + AccMax.ToString();
            AccList.Clear();

            BrakeList.Sort();
            //BrakeMin = BrakeList[0];
            BrakeMin = BrakeList[BrakeList.Count - 1];
            RangeList[4] = "BrakeMin=" + BrakeMin.ToString();
            //BrakeMax = BrakeList[BrakeList.Count - 1];
            BrakeMax = BrakeList[0];
            RangeList[5] = "BrakeMax=" + BrakeMax.ToString();
            BrakeList.Clear();

            File.WriteAllText(Application.dataPath + "/rangeText.txt", String.Empty);

            for (int i = 0; i < RangeList.Length; i++)
            {
                System.IO.File.AppendAllText(Application.dataPath + "/rangeText.txt", RangeList[i] + "\r\n");
            }

            SetMinMax = false;
        }

        void FixedUpdate()
        {
            //FindTheSteppedOnTexture();

            CurrentBTRSpeed = Mathf.Abs(Mathf.Round(target.speed * 3.6f));

            ButtonScript.SpeedVal = CurrentBTRSpeed.ToString();
            //SpeedMeter.CabSpeed = CurrentBTRSpeed;

            if (target == null) return;

            if (Input.GetKeyDown(resetVehicleKey)) m_doReset = true;

            // Set the ranges when "SetRangesMode" is ON
            if (ButtonScript.SetRangesMode)
            {
                GetRanges();
                SetMinMax = true;
            }
            else
            {
                //GetRangeList = false;
                if (SetMinMax)
                    GetRangeMinMax();
            }

        }

        void LateUpdate()
        {
            SetZHieghts();
            SetXHeights();

            P_Height = Mathf.Round(P_Point_Height * 100);
            P_Heightnew = P_Height + 100;
            Q_Height = Mathf.Round((Q_Point_Height + Q_Point_Height1) * 100);
            Q_Heightnew = Q_Height + 100;
            R_Height = Mathf.Round((R_Point_Height + R_Point_Height1) * 100);
            R_Heightnew = R_Height + 100;

            Lim_P_Height = Mathf.Round(LimitHieght(P_Height));
            Lim_Q_Height = Mathf.Round(LimitHieght(Q_Height));
            Lim_R_Height = Mathf.Round(LimitHieght(R_Height));
        }

            
        public void Get_KeyInputs()
        {
            //Get the system ON input
            if (Input.GetKeyDown(SystemONKey))
            {
                if (SystemIsON)
                {
                    SystemIsON = false;
                    IgnitionIsON = false;
                    ButtonScript.StartKeyVal = "0";
                    ButtonScript.IgnitionVal = "0";
                }
                else
                {
                    SystemIsON = true;
                    ButtonScript.IgnitionVal = "1";
                }

            }

            //Get the Ignition input
            if (Input.GetKey(IgnitionKey))
                IgnitionVal = 1;

            if (PrevIgnitionVal == 1 && IgnitionVal == 1)
            {
                IgnitionIsON = true;
                IgnitionVal = 0;
                ButtonScript.StartKeyVal = "1";
            }
            PrevIgnitionVal = IgnitionVal;

            //Get the controlling inputs
            if (Input.GetKey(LeftSteerKey))
                steerInputb = -1.0f;

            if (Input.GetKey(RightSteerKey))
                steerInputb = 1.0f;

            if (Input.GetKey(ThrottleKey))
            {
                if (autoGear)
                {
                    if (index == 1)
                    {
                        throttleInputb = -1.0f;
                    }
                    else
                    {
                        throttleInputb = 1.0f;
                    }
                }
                else
                {
                    if (index == 6)
                    {
                        throttleInputb = -1.0f;
                    }
                    else
                    {
                        throttleInputb = 1.0f;
                    }
                }
            }


            if (Input.GetKey(BrakeKey))
                brakeInputb = 1.0f;

            if (Input.GetKey(HandBrakeKey) || SerialReadScript.HandBrake == 1)
            {
                handbrakeInputb = 1.0f;
                //HandBrakePressed = true;
                HandBrakeOnIndicatorInstructorPanel.SetActive(true);//activate the handbrake on indicator on the instructor panel
            }
            else { HandBrakeOnIndicatorInstructorPanel.SetActive(false); }
        }

        void FindTheSteppedOnTexture()
        {
            //// Set up:
            //Vector3 TS; // terrain size
            //Vector2 AS; // control texture size

            //TS = Terrain.activeTerrain.terrainData.size;
            //AS.x = Terrain.activeTerrain.terrainData.alphamapWidth;
            //AS.y = Terrain.activeTerrain.terrainData.alphamapHeight;


            //// Lookup texture we are standing on:
            ////int AX = (int)((transform.position.x / TS.x) * AS.x + 0.5f);
            ////int AY = (int)((transform.position.z / TS.z) * AS.y + 0.5f);
            //int AX = (int)((transform.position.x / TS.x) * AS.x);
            //int AY = (int)((transform.position.z / TS.z) * AS.y);
            //float[,,] TerrCntrl = Terrain.activeTerrain.terrainData.GetAlphamaps(AX, AY, 1, 1);

            //// This can get a grid. Since we are only getting 1, we have a 1x1 array
            //// The 3rd is the 0-1 weight of that texture (if you have 4 textures, the 3rd
            ////   has size 4. TerrCntrl[0,0,0] is the weigth of texture#0.)
            //// TC[0,0, 0-??] add to 1
            //float c1 = TerrCntrl[0, 0, 0];
            //if (c1 > 0.5f)  // grass is 50% or more in this area
            //{
            //    SandText = true;
            //}
            //else
            //    SandText = false;

            Terrain T = Terrain.activeTerrain;

            Vector3 terrainPosition = gameObject.transform.position - T.transform.position;

            Vector3 mapPosition = new Vector3(terrainPosition.x / T.terrainData.size.x, 0, terrainPosition.z / T.terrainData.size.z);

            float xCoord = mapPosition.x * T.terrainData.alphamapWidth;
            float zCoord = mapPosition.z * T.terrainData.alphamapHeight;

            int posX = (int)xCoord;
            int posZ = (int)zCoord;

            float[,,] aMap = T.terrainData.GetAlphamaps(posX, posZ, 1, 1);

            float c1 = aMap[0, 0, 1];
            if (c1 > 0.5f)  // grass is 50% or more in this area
            {
                SandText = true;
            }
            else
                SandText = false;

        }


        void SetZHieghts()
        {
            ZRot = Mathf.Round(transform.localEulerAngles.z);
            if (ZRot < 180.0f)
            {
                hieght1 = 0.3f * Mathf.Tan(ZRot * Mathf.Deg2Rad);
                Q_Point_Height = -hieght1;
                R_Point_Height = hieght1;


            }
            else
            {
                hieght1 = 0.3f * Mathf.Tan((360.0f - ZRot) * Mathf.Deg2Rad);
                Q_Point_Height = hieght1;
                R_Point_Height = -hieght1;

            }
        }

        void SetXHeights()
        {

            XRot = Mathf.Round(transform.localEulerAngles.x);



            //    if (prevXRot < XRot)
            //    {
            //        climbing = false;
            //        down = true;
            //    }
            //    else if (prevXRot > XRot)
            //    {
            //        climbing = true;
            //        down = false;
            //    }
            //    else if(XRot == 360 || XRot == 0)
            //    {
            //        climbing = false;
            //        down = false;
            //    }


            //prevXRot = XRot;





            if (XRot < 180.0f)
            {
                hieght2 = 0.1732f * Mathf.Tan(XRot * Mathf.Deg2Rad);
                hieght3 = 0.3464f * Mathf.Tan(XRot * Mathf.Deg2Rad);
                //if (climbing)
                //{
                //    P_Point_Height = hieght3;
                //}
                //else if (down)
                //{
                //    P_Point_Height = -hieght3;
                //}
                //else
                //    P_Point_Height = -hieght3;

                P_Point_Height = -hieght3;

                Q_Point_Height1 = hieght2;
                R_Point_Height1 = hieght2;


            }
            else
            {
                hieght2 = 0.1732f * Mathf.Tan((360.0f - XRot) * Mathf.Deg2Rad);
                hieght3 = 0.3464f * Mathf.Tan((360.0f - XRot) * Mathf.Deg2Rad);
                //if (climbing)
                //{
                //    P_Point_Height = hieght3;
                //}
                //else if (down)
                //{
                //    P_Point_Height = -hieght3;
                //}
                //else
                //    P_Point_Height = hieght3;

                P_Point_Height = hieght3;

                Q_Point_Height1 = -hieght2;
                R_Point_Height1 = -hieght2;

            }


        }

        private float LimitHieght(float Hieght)
        {

            if (Hieght <= 9.335 && Hieght >= -10)
            {
                LimHieght = (Hieght / 10) * 512 + 512;
            }
            else if (Hieght > 9.335)
            {
                LimHieght = 990;
            }
            else
            {
                LimHieght = 0;
            }
            return LimHieght;
        }


        void BrakeLight()
        {

            if (brakeInputb > 0 || handbrakeInputb > 0)
            {
                BrakeLightLeft.enabled = true;
                BrakeLightRight.enabled = true;
            }
            else
            {
                BrakeLightLeft.enabled = false;
                BrakeLightRight.enabled = false;
            }
        }

        void SignalLight()
        {

            float floor = 2f;
            float ceiling = 12f;
            float emission = floor + Mathf.PingPong(Time.time * 20f, ceiling - floor);

            if (SignalCount <= 5)
            {
                SignalInt = 1;
                SignalCount++;
            }
            else if (SignalCount <= 10)
            {
                SignalInt = 0;
                SignalCount++;
            }
            else
            {
                SignalCount = 0;
            }

            if ((SerialReadScript.DoubleSignalLights == 1) || (Input.GetKey(KeyCode.M)))//if (Input.GetKey(KeyCode.O))//
            {
                SignalRightFront.enabled = true;
                SignalRightFront.intensity = emission;
                SignalRightBack.enabled = false;
                //SignalRightBack.intensity = emission;
                SignalLeftFront.enabled = true;
                SignalLeftFront.intensity = emission;
                SignalLeftBack.enabled = false;
                //SignalLeftBack.intensity = emission;
                RightSignal = SignalInt;
                LeftSignal = SignalInt;

                //Double Signal
                //networkObject.Signal = 3;
                SignalON = true;


            }


            else if ((/*SerialSystemIsON == 1 && */SerialReadScript.SignalLightsRight == 1) || (Input.GetKey(KeyCode.B)))//else if (IgnitionIsON && Input.GetKey(KeyCode.U))//
            {
                SignalRightFront.enabled = true;
                SignalRightBack.enabled = true;
                SignalLeftFront.enabled = false;
                SignalLeftBack.enabled = false;

                SignalRightFront.intensity = emission;
                SignalRightBack.intensity = emission;

                RightSignal = SignalInt;
                LeftSignal = 0;

                //Right Signal
                //networkObject.Signal = 2;
                SignalON = true;

            }
            else if ((/*SerialSystemIsON == 1 && */SerialReadScript.SignalLightsLeft == 1) || (Input.GetKey(KeyCode.C)))//else if (IgnitionIsON && Input.GetKey(KeyCode.I))//
            {
                SignalRightFront.enabled = false;
                SignalRightBack.enabled = false;
                SignalLeftFront.enabled = true;
                SignalLeftBack.enabled = true;

                SignalLeftFront.intensity = emission;
                SignalLeftBack.intensity = emission;

                RightSignal = 0;
                LeftSignal = SignalInt;

                //Left Signal
                //networkObject.Signal = 1;
                SignalON = true;

            }
            else
            {
                SignalRightFront.enabled = false;
                SignalRightBack.enabled = false;
                SignalLeftFront.enabled = false;
                SignalLeftBack.enabled = false;

                RightSignal = 0;
                LeftSignal = 0;

                //No Signal
                //networkObject.Signal = 0;
                SignalON = false;


            }

            //turn on left signal lights
            LeftSignalLightIndicators(IgnitionIsON, LeftSignal);

            //turn on right signal lights
            RightSignalLightIndicators(IgnitionIsON, RightSignal);

        }

        void HeadLight()
        {


            // Dim + Increased intensity
            if (SerialReadScript.ParkingLights == 1)
            {
                //ParkLightRight.spotAngle = 30;
                //ParkLightRight.intensity = 10;
                //ParkLightLeft.spotAngle = 30;
                //ParkLightLeft.intensity = 10;

                ParkLightRight.enabled = true;
                ParkLightLeft.enabled = true;
                ParkOn = 1;

                ParkLightOnIndicatorInstructorPanel.SetActive(true); // activate the Parking Light on indicator on the instructor panel


                if (ParkLightOnIndicatorInstructorPanel.activeSelf == false)
                {

                    ParkLightOnIndicatorInstructorPanel.SetActive(true); // activate the Parking Light on indicator on the instructor panel

                }
                else if (ParkLightOnIndicatorInstructorPanel.activeSelf == true)
                {
                    ParkLightOnIndicatorInstructorPanel.SetActive(false);
                }

                //networkObject.Parking = 1;
            }
            else
            {
                ParkOn = 0;
                ParkLightRight.enabled = false;
                ParkLightLeft.enabled = false;

                ParkLightOnIndicatorInstructorPanel.SetActive(false); //deactivate the Parking Light on indicator on the instructor panel

                //networkObject.Parking = 0;
            }

            // Head untill press
            if (SerialReadScript.HeadLightHigh == 1)
            {
                //HeadLightRight.range = 200;
                //HeadLightRight.intensity = 20;
                //HeadLightLeft.range = 200;
                //HeadLightLeft.intensity = 20;

                HeadLightRight.enabled = true;
                HeadLightLeft.enabled = true;

                HeadLightOnIndicatorInstructorPanel.SetActive(true); //activate the head Light on indicator on the instructor panel
                HeadLightOnIndicatorMeterPanel.SetActive(true);//activate the head Light on indicator on the Meter panel

                //HLightRightDim.enabled = false;
                //HLightLeftDim.enabled = false;

                HeadOn = 1;

                //Pass Light
                //networkObject.Head = 2;
            }
            // Head
            //else if (/*SerialSystemIsON == 1 && */SerialReadScript.HeadLightOn == 1)
            //{
                // Dim
                if (SerialReadScript.HeadLightDim == 1)
                {
                    //HeadLightRight.range = 100;
                    //HeadLightRight.intensity = 10;
                    //HeadLightLeft.range = 100;
                    //HeadLightLeft.intensity = 10;

                    HeadLightRight.enabled = false;
                    HeadLightLeft.enabled = false;

                    HeadLightOnIndicatorInstructorPanel.SetActive(false); //deactivate the head Light on indicator on the instructor panel
                    HeadLightOnIndicatorMeterPanel.SetActive(false);//deactivate the head Light on indicator on the Meter panel

                    //HLightRightDim.enabled = true;
                    //HLightLeftDim.enabled = true;

                    HeadOn = 0;

                    ////networkObject.Head = 1;
                }
                else if (SerialReadScript.HeadLightPass == 1)
                {
                    //HeadLightRight.range = 200;
                    //HeadLightRight.intensity = 20;
                    //HeadLightLeft.range = 200;
                    //HeadLightLeft.intensity = 20;

                    HeadLightRight.enabled = true;
                    HeadLightLeft.enabled = true;

                    HeadLightOnIndicatorInstructorPanel.SetActive(true); //activate the head Light on indicator on the instructor panel
                    HeadLightOnIndicatorMeterPanel.SetActive(true);//activate the head Light on indicator on the Meter panel

                    //HLightRightDim.enabled = false;
                    //HLightLeftDim.enabled = false;

                    HeadOn = 1;

                    //networkObject.Head = 2;
                }
           // }
            else
            {
                HeadLightRight.enabled = false;
                HeadLightLeft.enabled = false;
                //HLightRightDim.enabled = false;
                //HLightLeftDim.enabled = false;
                HeadOn = 0;

                HeadLightOnIndicatorInstructorPanel.SetActive(false); //deactivate the head Light on indicator on the instructor panel
                HeadLightOnIndicatorMeterPanel.SetActive(false);//deactivate the head Light on indicator on the Meter panel

                //networkObject.Head = 0;
            }

            //         // Head untill press
            //         if (Input.GetKey("4"))
            //         {
            //             HeadLightRight.spotAngle = 60;
            //             HeadLightRight.intensity = 20;
            //             HeadLightLeft.spotAngle = 60;
            //             HeadLightLeft.intensity = 20;

            //             HeadLightRight.enabled = true;
            //             HeadLightLeft.enabled = true;
            //         }

            //         if (IgnitionIsON)
            //         {
            //             // Head
            //             if (Input.GetKeyDown("3"))
            //             {
            //                 HeadLightRight.spotAngle = 60;
            //                 HeadLightRight.intensity = 20;
            //                 HeadLightLeft.spotAngle = 60;
            //                 HeadLightLeft.intensity = 20;

            //                 HeadLightRight.enabled = true;
            //                 HeadLightLeft.enabled = true;
            //             }
            //             // Dim + Increased intensity
            //             else if (Input.GetKeyDown("1"))
            //             {
            //                 HeadLightRight.spotAngle = 30;
            //                 HeadLightRight.intensity = 10;
            //                 HeadLightLeft.spotAngle = 30;
            //                 HeadLightLeft.intensity = 10;

            //                 HeadLightRight.enabled = true;
            //                 HeadLightLeft.enabled = true;
            //             }
            //             // Dim
            //             else if (Input.GetKeyDown("2"))
            //             {
            //                 HeadLightRight.spotAngle = 30;
            //                 HeadLightRight.intensity = 6;
            //                 HeadLightLeft.spotAngle = 30;
            //                 HeadLightLeft.intensity = 6;

            //                 HeadLightRight.enabled = true;
            //                 HeadLightLeft.enabled = true;
            //             }
            //             else if (Input.GetKeyDown("5"))
            //             {
            //		HeadLightRight.enabled = false;
            //		HeadLightLeft.enabled = false;
            //	}
            //}

        }

        void HeadLightKeyBoard()

        {
            // Dim + Increased intensity
            if (Input.GetKey(KeyCode.G))
            {
                //ParkLightRight.spotAngle = 30;
                //ParkLightRight.intensity = 10;
                //ParkLightLeft.spotAngle = 30;
                //ParkLightLeft.intensity = 10;

                ParkLightRight.enabled = !ParkLightRight.enabled;
                ParkLightLeft.enabled = !ParkLightLeft.enabled;
                ParkOnKeyboardIn = !ParkOnKeyboardIn;

                if (ParkLightOnIndicatorInstructorPanel.activeSelf == false)
                {

                    ParkLightOnIndicatorInstructorPanel.SetActive(true); // activate the Parking Light on indicator on the instructor panel

                } else if (ParkLightOnIndicatorInstructorPanel.activeSelf == true)
                {
                    ParkLightOnIndicatorInstructorPanel.SetActive(false);
                }

                //networkObject.Parking = 1;
            }
            //else
            //{
            //    ParkOn = 0;
            //    ParkLightRight.enabled = false;
            //    ParkLightLeft.enabled = false;

            //    ParkLightOnIndicatorInstructorPanel.SetActive(false); //deactivate the Parking Light on indicator on the instructor panel

            //    //networkObject.Parking = 0;
            //}

            // Head untill press
            if (Input.GetKey(KeyCode.J) || (SerialReadScript.HeadLightHigh == 1 && SerialReadScript.ParkingLights == 1))
            {
                //HeadLightRight.range = 200;
                //HeadLightRight.intensity = 20;
                //HeadLightLeft.range = 200;
                //HeadLightLeft.intensity = 20;

                HeadLightRight.enabled = true;
                HeadLightLeft.enabled = true;

                HeadLightOnIndicatorInstructorPanel.SetActive(true); //activate the head Light on indicator on the instructor panel
                HeadLightOnIndicatorMeterPanel.SetActive(true);//activate the head Light on indicator on the Meter panel

                //HLightRightDim.enabled = false;
                //HLightLeftDim.enabled = false;

                HeadOn = 1;

                //Pass Light
                //networkObject.Head = 2;
            }
            // Head
            else if (Input.GetKey(KeyCode.Q))
            {
                // Dim
                if (SerialReadScript.HeadLightDim == 1)
                {
                    //HeadLightRight.range = 100;
                    //HeadLightRight.intensity = 10;
                    //HeadLightLeft.range = 100;
                    //HeadLightLeft.intensity = 10;

                    HeadLightRight.enabled = false;
                    HeadLightLeft.enabled = false;

                    HeadLightOnIndicatorInstructorPanel.SetActive(false); //deactivate the head Light on indicator on the instructor panel
                    HeadLightOnIndicatorMeterPanel.SetActive(false);//deactivate the head Light on indicator on the Meter panel

                    //HLightRightDim.enabled = true;
                    //HLightLeftDim.enabled = true;

                    HeadOn = 0;

                    ////networkObject.Head = 1;
                }
                else if (Input.GetKey(KeyCode.J))
                {
                    //HeadLightRight.range = 200;
                    //HeadLightRight.intensity = 20;
                    //HeadLightLeft.range = 200;
                    //HeadLightLeft.intensity = 20;

                    HeadLightRight.enabled = true;
                    HeadLightLeft.enabled = true;

                    HeadLightOnIndicatorInstructorPanel.SetActive(true); //activate the head Light on indicator on the instructor panel
                    HeadLightOnIndicatorMeterPanel.SetActive(true);//activate the head Light on indicator on the Meter panel

                    //HLightRightDim.enabled = false;
                    //HLightLeftDim.enabled = false;

                    HeadOn = 1;

                    //networkObject.Head = 2;
                }
            }
            else
            {
                HeadLightRight.enabled = false;
                HeadLightLeft.enabled = false;
                //HLightRightDim.enabled = false;
                //HLightLeftDim.enabled = false;
                HeadOn = 0;

                HeadLightOnIndicatorInstructorPanel.SetActive(false); //deactivate the head Light on indicator on the instructor panel
                HeadLightOnIndicatorMeterPanel.SetActive(false);//deactivate the head Light on indicator on the Meter panel

                //networkObject.Head = 0;
            }

            //void HeadNetwork()
            //{
            //    if (networkObject.Head == 2)
            //    {
            //        //HeadLightRight.spotAngle = 60;
            //        //HeadLightRight.intensity = 30;
            //        //HeadLightLeft.spotAngle = 60;
            //        //HeadLightLeft.intensity = 30;

            //        HeadLightRight.enabled = true;
            //        HeadLightLeft.enabled = true;

            //        HLightRightDim.enabled = false;
            //        HLightLeftDim.enabled = false;
            //    }
            //    else if (networkObject.Head == 1)
            //    {
            //        //HeadLightRight.spotAngle = 30;
            //        //HeadLightRight.intensity = 10;
            //        //HeadLightLeft.spotAngle = 30;
            //        //HeadLightLeft.intensity = 10;

            //        HeadLightRight.enabled = false;
            //        HeadLightLeft.enabled = false;

            //        HLightRightDim.enabled = true;
            //        HLightLeftDim.enabled = true;
            //    }
            //    else
            //    {
            //        HeadLightRight.enabled = false;
            //        HeadLightLeft.enabled = false;

            //        HLightRightDim.enabled = false;
            //        HLightLeftDim.enabled = false;
            //    }

            //    if (networkObject.Parking == 1)
            //    {
            //        ParkLightRight.spotAngle = 30;
            //        ParkLightRight.intensity = 10;
            //        ParkLightLeft.spotAngle = 30;
            //        ParkLightLeft.intensity = 10;

            //        ParkLightRight.enabled = true;
            //        ParkLightLeft.enabled = true;
            //    }
            //    else
            //    {
            //        ParkLightRight.enabled = false;
            //        ParkLightLeft.enabled = false;
            //    }
            //}

            //void SignalNetwork()
            //{
            //    float floor = 2f;
            //    float ceiling = 12f;
            //    float emission = floor + Mathf.PingPong(Time.time * 20f, ceiling - floor);

            //    if (networkObject.Signal == 3)
            //    {
            //        SignalRightFront.enabled = true;
            //        SignalRightFront.intensity = emission;
            //        SignalRightBack.enabled = false;
            //        //SignalRightBack.intensity = emission;
            //        SignalLeftFront.enabled = true;
            //        SignalLeftFront.intensity = emission;
            //        SignalLeftBack.enabled = false;
            //        //SignalLeftBack.intensity = emission;
            //    }
            //    else if (networkObject.Signal == 2)
            //    {
            //        SignalRightFront.enabled = true;
            //        SignalRightBack.enabled = true;
            //        SignalLeftFront.enabled = false;
            //        SignalLeftBack.enabled = false;

            //        SignalRightFront.intensity = emission;
            //        SignalRightBack.intensity = emission;
            //    }
            //    else if (networkObject.Signal == 1)
            //    {
            //        SignalRightFront.enabled = false;
            //        SignalRightBack.enabled = false;
            //        SignalLeftFront.enabled = true;
            //        SignalLeftBack.enabled = true;

            //        SignalLeftFront.intensity = emission;
            //        SignalLeftBack.intensity = emission;
            //    }
            //    else
            //    {
            //        SignalRightFront.enabled = false;
            //        SignalRightBack.enabled = false;
            //        SignalLeftFront.enabled = false;
            //        SignalLeftBack.enabled = false;
            //    }
            //}



        }

        void RightSignalLightIndicators(bool StartEngine, int RightSignal)
        {
            float tempSig = 0;

            if (tempSig == 0)
            {
                tempSig = Mathf.PingPong(Time.time, 0.8f);

            }

            if (tempSig == 0.8)
            {
                tempSig = Mathf.PingPong(Time.time, -0.8f);

            }

            if (((IgnitionIsON == true && LeftSignal == 0 && RightSignal != 0) || (SerialReadScript.DoubleSignalLights == 1) || (SerialReadScript.SignalLightsRight == 1) || (Input.GetKey(KeyCode.B)) || (Input.GetKey(KeyCode.M))) && (tempSig >= 0.4f))
            {
                rightSignalOn.SetActive(true);
                rightSignalOnInstructorPanel.SetActive(true);
            }
            else
            {
                rightSignalOn.SetActive(false);
                rightSignalOnInstructorPanel.SetActive(false);
            }

        }

        void LeftSignalLightIndicators(bool StartEngine, int SignalLeft)
        {
            float tempSig = 0;

            if (tempSig == 0)
            {
                tempSig = Mathf.PingPong(Time.time, 0.8f);

            }

            if (tempSig == 0.8)
            {
                tempSig = Mathf.PingPong(Time.time, -0.8f);

            }

            if (((IgnitionIsON == true && LeftSignal != 0 && RightSignal == 0) || (SerialReadScript.SignalLightsLeft == 1) || (SerialReadScript.DoubleSignalLights == 1) || (Input.GetKey(KeyCode.C)) || (Input.GetKey(KeyCode.M))) && (tempSig >= 0.4f))
            {
                leftSignalOn.SetActive(true);
                leftSignalOnInstructorPanel.SetActive(true);
            }
            else
            {
                leftSignalOn.SetActive(false);
                leftSignalOnInstructorPanel.SetActive(false);

            }

        }

        void SetSpeedo()
        {

            float currentS = 0;

            if (SpeedoMeter > 0)
            {
                currentS = SpeedoMeter * SpeedoFullAngle / SpeedoFullRange;

            }
            steerfactorS = Mathf.Lerp(steerfactorS, currentS, 0.05f);

            Speedo.localEulerAngles = new Vector3(0, 0, ((-steerfactorS / 1.53f) + 130.8f));

        }

        void SetFuelMeter()
        {
            float currentF = 0;

            if (FuelLevel > 0)
            {
                currentF = FuelLevel * FuelFullAngle / FuelFullRange;
            }
            steerfactorFuel = Mathf.Lerp(steerfactorFuel, currentF, 0.05f);

            FuelM.localEulerAngles = new Vector3(0, 0, (-steerfactorFuel + 176));


        }


        void InstructorPanelCurrentGearIndicator()
        {

            if (autoGear == false)
            {
                if (index == 0)
                {
                    ParkingGearIndicatorInstructorPanel.SetActive(false);
                    ReverseGearIndicatorInstructorPanel.SetActive(false);
                    NeutralGearIndicatorInstructorPanel.SetActive(true);
                    DriveGearIndicatorInstructorPanel.SetActive(false);
                    SportGearIndicatorInstructorPanel.SetActive(false);
                    LowGearIndicatorInstructorPanel.SetActive(false);
                    FirstGearIndicatorInstructorPanel.SetActive(false);
                    SecondGearIndicatorInstructorPanel.SetActive(false);
                    ThirdGearIndicatorInstructorPanel.SetActive(false);
                    FourthGearIndicatorInstructorPanel.SetActive(false);
                    FifthGearIndicatorInstructorPanel.SetActive(false);

                    ManualGearGearN.SetActive(true);
                    ManualGearGear1.SetActive(false);
                    ManualGearGear2.SetActive(false);
                    ManualGearGear3.SetActive(false);
                    ManualGearGear4.SetActive(false);
                    ManualGearGear5.SetActive(false);
                    ManualGearGearR.SetActive(false);

                    CurrentVehicleGear = "N";
                }

               else if (index == 1 )
                {
                    ParkingGearIndicatorInstructorPanel.SetActive(false);
                    ReverseGearIndicatorInstructorPanel.SetActive(false);
                    NeutralGearIndicatorInstructorPanel.SetActive(false);
                    DriveGearIndicatorInstructorPanel.SetActive(false);
                    SportGearIndicatorInstructorPanel.SetActive(false);
                    LowGearIndicatorInstructorPanel.SetActive(false);
                    FirstGearIndicatorInstructorPanel.SetActive(true);
                    SecondGearIndicatorInstructorPanel.SetActive(false);
                    ThirdGearIndicatorInstructorPanel.SetActive(false);
                    FourthGearIndicatorInstructorPanel.SetActive(false);
                    FifthGearIndicatorInstructorPanel.SetActive(false);

                    ManualGearGearN.SetActive(false);
                    ManualGearGear1.SetActive(true);
                    ManualGearGear2.SetActive(false);
                    ManualGearGear3.SetActive(false);
                    ManualGearGear4.SetActive(false);
                    ManualGearGear5.SetActive(false);
                    ManualGearGearR.SetActive(false);

                    CurrentVehicleGear = "1";
               }

               else if (index == 2)
               {
                    ParkingGearIndicatorInstructorPanel.SetActive(false);
                    ReverseGearIndicatorInstructorPanel.SetActive(false);
                    NeutralGearIndicatorInstructorPanel.SetActive(false);
                    DriveGearIndicatorInstructorPanel.SetActive(false);
                    SportGearIndicatorInstructorPanel.SetActive(false);
                    LowGearIndicatorInstructorPanel.SetActive(false);
                    FirstGearIndicatorInstructorPanel.SetActive(false);
                    SecondGearIndicatorInstructorPanel.SetActive(true);
                    ThirdGearIndicatorInstructorPanel.SetActive(false);
                    FourthGearIndicatorInstructorPanel.SetActive(false);
                    FifthGearIndicatorInstructorPanel.SetActive(false);

                    ManualGearGearN.SetActive(false);
                    ManualGearGear1.SetActive(false);
                    ManualGearGear2.SetActive(true);
                    ManualGearGear3.SetActive(false);
                    ManualGearGear4.SetActive(false);
                    ManualGearGear5.SetActive(false);
                    ManualGearGearR.SetActive(false);

                    CurrentVehicleGear = "2";
               }

               else if (index == 3)
               {
                    ParkingGearIndicatorInstructorPanel.SetActive(false);
                    ReverseGearIndicatorInstructorPanel.SetActive(false);
                    NeutralGearIndicatorInstructorPanel.SetActive(false);
                    DriveGearIndicatorInstructorPanel.SetActive(false);
                    SportGearIndicatorInstructorPanel.SetActive(false);
                    LowGearIndicatorInstructorPanel.SetActive(false);
                    FirstGearIndicatorInstructorPanel.SetActive(false);
                    SecondGearIndicatorInstructorPanel.SetActive(false);
                    ThirdGearIndicatorInstructorPanel.SetActive(true);
                    FourthGearIndicatorInstructorPanel.SetActive(false);
                    FifthGearIndicatorInstructorPanel.SetActive(false);

                    ManualGearGearN.SetActive(false);
                    ManualGearGear1.SetActive(false);
                    ManualGearGear2.SetActive(false);
                    ManualGearGear3.SetActive(true);
                    ManualGearGear4.SetActive(false);
                    ManualGearGear5.SetActive(false);
                    ManualGearGearR.SetActive(false);

                    CurrentVehicleGear = "3";
               }
               else if (index == 4)
               {
                    ParkingGearIndicatorInstructorPanel.SetActive(false);
                    ReverseGearIndicatorInstructorPanel.SetActive(false);
                    NeutralGearIndicatorInstructorPanel.SetActive(false);
                    DriveGearIndicatorInstructorPanel.SetActive(false);
                    SportGearIndicatorInstructorPanel.SetActive(false);
                    LowGearIndicatorInstructorPanel.SetActive(false);
                    FirstGearIndicatorInstructorPanel.SetActive(false);
                    SecondGearIndicatorInstructorPanel.SetActive(false);
                    ThirdGearIndicatorInstructorPanel.SetActive(false);
                    FourthGearIndicatorInstructorPanel.SetActive(true);
                    FifthGearIndicatorInstructorPanel.SetActive(false);

                    ManualGearGearN.SetActive(false);
                    ManualGearGear1.SetActive(false);
                    ManualGearGear2.SetActive(false);
                    ManualGearGear3.SetActive(false);
                    ManualGearGear4.SetActive(true);
                    ManualGearGear5.SetActive(false);
                    ManualGearGearR.SetActive(false);

                    CurrentVehicleGear = "4";
               }
               else if (index == 5)
               {
                    ParkingGearIndicatorInstructorPanel.SetActive(false);
                    ReverseGearIndicatorInstructorPanel.SetActive(false);
                    NeutralGearIndicatorInstructorPanel.SetActive(false);
                    DriveGearIndicatorInstructorPanel.SetActive(false);
                    SportGearIndicatorInstructorPanel.SetActive(false);
                    LowGearIndicatorInstructorPanel.SetActive(false);
                    FirstGearIndicatorInstructorPanel.SetActive(false);
                    SecondGearIndicatorInstructorPanel.SetActive(false);
                    ThirdGearIndicatorInstructorPanel.SetActive(false);
                    FourthGearIndicatorInstructorPanel.SetActive(false);
                    FifthGearIndicatorInstructorPanel.SetActive(true);

                    ManualGearGearN.SetActive(false);
                    ManualGearGear1.SetActive(false);
                    ManualGearGear2.SetActive(false);
                    ManualGearGear3.SetActive(false);
                    ManualGearGear4.SetActive(false);
                    ManualGearGear5.SetActive(true);
                    ManualGearGearR.SetActive(false);

                    CurrentVehicleGear = "5";
                }

               else if (index == 6)
                {
                    ParkingGearIndicatorInstructorPanel.SetActive(false);
                    ReverseGearIndicatorInstructorPanel.SetActive(true);
                    NeutralGearIndicatorInstructorPanel.SetActive(false);
                    DriveGearIndicatorInstructorPanel.SetActive(false);
                    SportGearIndicatorInstructorPanel.SetActive(false);
                    LowGearIndicatorInstructorPanel.SetActive(false);
                    FirstGearIndicatorInstructorPanel.SetActive(false);
                    SecondGearIndicatorInstructorPanel.SetActive(false);
                    ThirdGearIndicatorInstructorPanel.SetActive(false);
                    FourthGearIndicatorInstructorPanel.SetActive(false);
                    FifthGearIndicatorInstructorPanel.SetActive(false);

                    ManualGearGearN.SetActive(false);
                    ManualGearGear1.SetActive(false);
                    ManualGearGear2.SetActive(false);
                    ManualGearGear3.SetActive(false);
                    ManualGearGear4.SetActive(false);
                    ManualGearGear5.SetActive(false);
                    ManualGearGearR.SetActive(true);

                    CurrentVehicleGear = "R";
                }




            }
            else if (autoGear == true)
            {

                if (index == 0)
                {
                    ParkingGearIndicatorInstructorPanel.SetActive(true);
                    ReverseGearIndicatorInstructorPanel.SetActive(false);
                    NeutralGearIndicatorInstructorPanel.SetActive(false);
                    DriveGearIndicatorInstructorPanel.SetActive(false);
                    SportGearIndicatorInstructorPanel.SetActive(false);
                    LowGearIndicatorInstructorPanel.SetActive(false);
                    FirstGearIndicatorInstructorPanel.SetActive(false);
                    SecondGearIndicatorInstructorPanel.SetActive(false);
                    ThirdGearIndicatorInstructorPanel.SetActive(false);
                    FourthGearIndicatorInstructorPanel.SetActive(false);
                    FifthGearIndicatorInstructorPanel.SetActive(false);

                    AutoGearGearP.SetActive(true);
                    AutoGearGearR.SetActive(false);
                    AutoGearGearN.SetActive(false);
                    AutoGearGearD.SetActive(false);
                    AutoGearGearS.SetActive(false);
                    AutoGearGearL.SetActive(false);

                    CurrentVehicleGear = "P";

                }

                else if (index == 1)
                {
                    ParkingGearIndicatorInstructorPanel.SetActive(false);
                    ReverseGearIndicatorInstructorPanel.SetActive(true);
                    NeutralGearIndicatorInstructorPanel.SetActive(false);
                    DriveGearIndicatorInstructorPanel.SetActive(false);
                    SportGearIndicatorInstructorPanel.SetActive(false);
                    LowGearIndicatorInstructorPanel.SetActive(false);
                    FirstGearIndicatorInstructorPanel.SetActive(false);
                    SecondGearIndicatorInstructorPanel.SetActive(false);
                    ThirdGearIndicatorInstructorPanel.SetActive(false);
                    FourthGearIndicatorInstructorPanel.SetActive(false);
                    FifthGearIndicatorInstructorPanel.SetActive(false);

                    AutoGearGearP.SetActive(false);
                    AutoGearGearR.SetActive(true);
                    AutoGearGearN.SetActive(false);
                    AutoGearGearD.SetActive(false);
                    AutoGearGearS.SetActive(false);
                    AutoGearGearL.SetActive(false);

                    CurrentVehicleGear = "R";
                }

                else if (index == 2)
                {
                    ParkingGearIndicatorInstructorPanel.SetActive(false);
                    ReverseGearIndicatorInstructorPanel.SetActive(false);
                    NeutralGearIndicatorInstructorPanel.SetActive(true);
                    DriveGearIndicatorInstructorPanel.SetActive(false);
                    SportGearIndicatorInstructorPanel.SetActive(false);
                    LowGearIndicatorInstructorPanel.SetActive(false);
                    FirstGearIndicatorInstructorPanel.SetActive(false);
                    SecondGearIndicatorInstructorPanel.SetActive(false);
                    ThirdGearIndicatorInstructorPanel.SetActive(false);
                    FourthGearIndicatorInstructorPanel.SetActive(false);
                    FifthGearIndicatorInstructorPanel.SetActive(false);

                    AutoGearGearP.SetActive(false);
                    AutoGearGearR.SetActive(false);
                    AutoGearGearN.SetActive(true);
                    AutoGearGearD.SetActive(false);
                    AutoGearGearS.SetActive(false);
                    AutoGearGearL.SetActive(false);

                    CurrentVehicleGear = "N";
                }

                else if (index == 3)
                {
                    ParkingGearIndicatorInstructorPanel.SetActive(false);
                    ReverseGearIndicatorInstructorPanel.SetActive(false);
                    NeutralGearIndicatorInstructorPanel.SetActive(false);
                    DriveGearIndicatorInstructorPanel.SetActive(true);
                    SportGearIndicatorInstructorPanel.SetActive(false);
                    LowGearIndicatorInstructorPanel.SetActive(false);
                    FirstGearIndicatorInstructorPanel.SetActive(false);
                    SecondGearIndicatorInstructorPanel.SetActive(false);
                    ThirdGearIndicatorInstructorPanel.SetActive(false);
                    FourthGearIndicatorInstructorPanel.SetActive(false);
                    FifthGearIndicatorInstructorPanel.SetActive(false);

                    AutoGearGearP.SetActive(false);
                    AutoGearGearR.SetActive(false);
                    AutoGearGearN.SetActive(false);
                    AutoGearGearD.SetActive(true);
                    AutoGearGearS.SetActive(false);
                    AutoGearGearL.SetActive(false);

                    CurrentVehicleGear = "D";
                }

                if (SerialReadScript.CurrentGear == "S")
                {
                    ParkingGearIndicatorInstructorPanel.SetActive(false);
                    ReverseGearIndicatorInstructorPanel.SetActive(false);
                    NeutralGearIndicatorInstructorPanel.SetActive(false);
                    DriveGearIndicatorInstructorPanel.SetActive(false);
                    SportGearIndicatorInstructorPanel.SetActive(true);
                    LowGearIndicatorInstructorPanel.SetActive(false);
                    FirstGearIndicatorInstructorPanel.SetActive(false);
                    SecondGearIndicatorInstructorPanel.SetActive(false);
                    ThirdGearIndicatorInstructorPanel.SetActive(false);
                    FourthGearIndicatorInstructorPanel.SetActive(false);
                    FifthGearIndicatorInstructorPanel.SetActive(false);

                    AutoGearGearP.SetActive(false);
                    AutoGearGearR.SetActive(false);
                    AutoGearGearN.SetActive(false);
                    AutoGearGearD.SetActive(false);
                    AutoGearGearS.SetActive(true);
                    AutoGearGearL.SetActive(false);

                    CurrentVehicleGear = "S";
                }

                if (SerialReadScript.CurrentGear == "L")
                {
                    ParkingGearIndicatorInstructorPanel.SetActive(false);
                    ReverseGearIndicatorInstructorPanel.SetActive(false);
                    NeutralGearIndicatorInstructorPanel.SetActive(false);
                    DriveGearIndicatorInstructorPanel.SetActive(false);
                    SportGearIndicatorInstructorPanel.SetActive(false);
                    LowGearIndicatorInstructorPanel.SetActive(true);
                    FirstGearIndicatorInstructorPanel.SetActive(false);
                    SecondGearIndicatorInstructorPanel.SetActive(false);
                    ThirdGearIndicatorInstructorPanel.SetActive(false);
                    FourthGearIndicatorInstructorPanel.SetActive(false);
                    FifthGearIndicatorInstructorPanel.SetActive(false);

                    AutoGearGearP.SetActive(false);
                    AutoGearGearR.SetActive(false);
                    AutoGearGearN.SetActive(false);
                    AutoGearGearD.SetActive(false);
                    AutoGearGearS.SetActive(false);
                    AutoGearGearL.SetActive(true);

                    CurrentVehicleGear = "L";
                }

            }


        }

        static void FuelLevelTextFileWrite(float CurrentFuelLevel)
        {

            string path = "Assets/DILAN/Meter Panel/FuelLevelDilanUSJ.txt"; // Specify the file path
            //StreamWriter writer = new StreamWriter(path, true); // Create a StreamWriter
            File.WriteAllText(path, CurrentFuelLevel.ToString()); // Write some text to the file
            //writer.Close(); // Close the writer
            //AssetDatabase.ImportAsset(path); // Re-import the file to update the reference in the editor
            //TextAsset asset = Resources.Load<TextAsset>("FuelLevelDilanUSJ"); // Load the file as a TextAsset
            //Debug.Log("Current Fuel Level" + asset.text); // Print the text from the file
        }

        public float FuelLevelTextFileRead()
        {

            string path = "Assets/DILAN/Meter Panel/FuelLevelDilanUSJ.txt"; // Specify the file path

            // Check if the file exists at the path
            if (File.Exists(path))
            {
                // Read the value from the file
                string valueString = File.ReadAllText(path);

                // Convert the string to a float
                RemainingFuel = float.Parse(valueString);

                // Now you can use the value
                //Debug.Log("The value from the file is: " + FuelValue);
            }
            else
            {
                Debug.Log("File not found at: " + path);
            }

            return RemainingFuel;
        }



        public void RefillFuelTank()
        {
;
            FuelLevel = 4000f;
            IgnitionOffForSeriaclReadScript = false;


        }

        public void WriteCurrentFuelLevelToTheTextFile()
        {

            FuelLevelTextFileWrite(FuelLevel);

        }

        public void FuelMeterOperation()
        {

            if ((IgnitionIsON || SerialIgnitionIsOn) && StartTime == 0f)
            {
                StartTime = Time.time;
                //Debug.Log("Main Vehicle time is " + StartTime);
            }

            if (IgnitionIsON || SerialIgnitionIsOn)
            {
                float elapsedtime2 = Time.time - StartTime;
                //Debug.Log("Main Vehicle time is " + elapsedtime2);
                elapsedtime2 = Time.time - elapsedtime2;
                VehicleTravelledDistanceFuelMeter = ((SpeedoMeter * (elapsedtime2 / 3600)) - VehicleTravelledDistanceFuelMeter);

                FuelLevel = FuelLevel - (VehicleTravelledDistanceFuelMeter / 14f);

            }


            if (!IgnitionIsON || !SystemIsON || !SerialIgnitionIsOn)
            {
                FuelLevelTextFileWrite(FuelLevel);
            }

            if (FuelLevel <= 0)
            {
                if (!Serial)
                {
                    IgnitionIsON = false;
                }
                else
                {
                    IgnitionOffForSeriaclReadScript = true;
                    CurrentIgnition = 0;
                    SerialReadScript.Ignition = 0;
                    SerialIgnitionIsOn = false;
                }
            }

            //Fuel Indicator Meter Panel
            if (FuelLevel <= 5.0f)
            {

                FuelIndicator.SetActive(true);
            }
            else
            {
                FuelIndicator.SetActive(false);
            }

            //Fuel Meter in the Meter Panel Temp Meter

            if (FuelLevel >= 0.5f && FuelLevel <= 6.66f)
            {
                FuelLevel1.SetActive(true);
                FuelLevel2.SetActive(false);
                FuelLevel3.SetActive(false);
                FuelLevel4.SetActive(false);
                FuelLevel5.SetActive(false);
                FuelLevel6.SetActive(false);
            }
            else if (FuelLevel >= 6.66f && FuelLevel <= 13.32f)
            {
                FuelLevel1.SetActive(true);
                FuelLevel2.SetActive(true);
                FuelLevel3.SetActive(false);
                FuelLevel4.SetActive(false);
                FuelLevel5.SetActive(false);
                FuelLevel6.SetActive(false);
            }
            else if (FuelLevel >= 13.32f && FuelLevel <= 19.98f)
            {
                FuelLevel1.SetActive(true);
                FuelLevel2.SetActive(true);
                FuelLevel3.SetActive(true);
                FuelLevel4.SetActive(false);
                FuelLevel5.SetActive(false);
                FuelLevel6.SetActive(false);
            }
            else if (FuelLevel >= 19.98f && FuelLevel <= 26.64f)
            {
                FuelLevel1.SetActive(true);
                FuelLevel2.SetActive(true);
                FuelLevel3.SetActive(true);
                FuelLevel4.SetActive(true);
                FuelLevel5.SetActive(false);
                FuelLevel6.SetActive(false);
            }
            else if (FuelLevel >= 26.64f && FuelLevel <= 33.3f)
            {
                FuelLevel1.SetActive(true);
                FuelLevel2.SetActive(true);
                FuelLevel3.SetActive(true);
                FuelLevel4.SetActive(true);
                FuelLevel5.SetActive(true);
                FuelLevel6.SetActive(false);
            }
            else if (FuelLevel >= 33.3f )
            {
                FuelLevel1.SetActive(true);
                FuelLevel2.SetActive(true);
                FuelLevel3.SetActive(true);
                FuelLevel4.SetActive(true);
                FuelLevel5.SetActive(true);
                FuelLevel6.SetActive(true);
            }


        }

        static void TotalTravelledDistanceTextFileWrite(int VehicleTravelledDistanceRoundedInt)
        {

            string path = "Assets/DILAN/Meter Panel/TotalTravelledDistance.txt"; // Specify the file path
            //StreamWriter writer = new StreamWriter(path, true); // Create a StreamWriter
            File.WriteAllText(path, VehicleTravelledDistanceRoundedInt.ToString()); // Write some text to the file
            //writer.Close(); // Close the writer
            //AssetDatabase.ImportAsset(path); // Re-import the file to update the reference in the editor
            //TextAsset asset = Resources.Load<TextAsset>("FuelLevelDilanUSJ"); // Load the file as a TextAsset
            //Debug.Log("Current Fuel Level" + asset.text); // Print the text from the file
        }

        public float TotalTravelledDistanceTextFileRead()
        {

            string path = "Assets/DILAN/Meter Panel/TotalTravelledDistance.txt"; // Specify the file path

            // Check if the file exists at the path
            if (File.Exists(path))
            {
                // Read the value from the file
                string valueString = File.ReadAllText(path);

                // Convert the string to a float
                TotalTravelledDistance = int.Parse(valueString);

                // Now you can use the value
                //Debug.Log("The value from the file is: " + FuelValue);
            }
            else
            {
                Debug.Log("File not found at: " + path);
            }

            return TotalTravelledDistance;
        }

        public void WriteTotalDistanceToTheTextFile()
        {

            TotalTravelledDistanceTextFileWrite(VehicleTravelledDistanceRoundedInt);

        }

        static void ResettableTravelledDistanceTextFileWrite(int VehicleTravelledDistanceRoundedIntResettable)
        {

            string path = "Assets/DILAN/Meter Panel/TravelledDistance.txt"; // Specify the file path
            //StreamWriter writer = new StreamWriter(path, true); // Create a StreamWriter
            File.WriteAllText(path, VehicleTravelledDistanceRoundedIntResettable.ToString()); // Write some text to the file
            //writer.Close(); // Close the writer
            //AssetDatabase.ImportAsset(path); // Re-import the file to update the reference in the editor
            //TextAsset asset = Resources.Load<TextAsset>("FuelLevelDilanUSJ"); // Load the file as a TextAsset
            //Debug.Log("Current Fuel Level" + asset.text); // Print the text from the file
        }

        public float ResettableTravelledDistanceTextFileRead()
        {

            string path = "Assets/DILAN/Meter Panel/TravelledDistance.txt"; // Specify the file path

            // Check if the file exists at the path
            if (File.Exists(path))
            {
                // Read the value from the file
                string valueString = File.ReadAllText(path);

                // Convert the string to a float
                TravelledDistanceResettable = int.Parse(valueString);

                // Now you can use the value
                //Debug.Log("The value from the file is: " + FuelValue);
            }
            else
            {
                Debug.Log("File not found at: " + path);
            }

            return TravelledDistanceResettable;
        }


        static void ResetResettableTravelledDistanceTextFileWrite()
        {
            float zerovalue = 0.0f;
            string path = "Assets/DILAN/Meter Panel/TravelledDistance.txt"; // Specify the file path
            //StreamWriter writer = new StreamWriter(path, true); // Create a StreamWriter
            File.WriteAllText(path, zerovalue.ToString()); // Write some text to the file
            //writer.Close(); // Close the writer
            //AssetDatabase.ImportAsset(path); // Re-import the file to update the reference in the editor
            //TextAsset asset = Resources.Load<TextAsset>("FuelLevelDilanUSJ"); // Load the file as a TextAsset
            //Debug.Log("Current Fuel Level" + asset.text); // Print the text from the file
        }

        public void WriteResettableDistanceToTheTextFile()
        {

            ResettableTravelledDistanceTextFileWrite(VehicleTravelledDistanceRoundedIntResettable);

        }

        public void ResetButtonResettableDistanceToTheTextFile()
        {

            ResetResettableTravelledDistanceTextFileWrite();
            VehicleTravelledDistanceRoundedIntResettable = 0;
            elapsedtime1 = 0;
            VehicleTravelledDistanceResettable = 0;
            VehicleTravelledDistanceResettableFinal = 0;

        }



        public void EngineHeatIndicatorONbyInstructor()
        {
            if (OverheatIndicatorOn.activeSelf == false)
            {
                //OverHeat = true;
                //OverHeatIndicator.SetActive(true);
                EngineOverHeatTrueInstructorPanel = true;
                OverheatIndicatorOn.SetActive(true);
                IgnitionOnByOverHeatButtonInstructorPanel = false;
                //SetTempMeterOverHeat();


            }

            else if (OverheatIndicatorOn.activeSelf != false)
            {
                OverHeat = false;
                if (!Serial)
                {
                    IgnitionIsON = true;
                }
                else
                {
                    SerialIgnitionIsOn = true;
                    IgnitionOffForSeriaclReadScript = false;
                }
                EngineOverHeatTrueInstructorPanel = false;
                IgnitionOnByOverHeatButtonInstructorPanel = true;

                //SetTempMeter();
                OverHeatIndicator.SetActive(false);
                OverheatIndicatorOn.SetActive(false);
            }
        }

        void SetTempMeter()
        {

            OverHeat = false;
            //EngineOverHeatTrueInstructorPanel = false;
            if (!Serial)
            {
                IgnitionIsON = true;
            }
            else
            {
                SerialIgnitionIsOn = true;
                IgnitionOffForSeriaclReadScript = false;
            }

            float currentT = 0;

            if (TempMeter > 0)
            {
                currentT = TempMeter * TempFullAngle / TempFullRange;
            }
            steerfactorTemp = Mathf.Lerp(steerfactorTemp, (currentT + (23.2f)), 0.05f);

            TempM.localEulerAngles = new Vector3(0, 0, (-steerfactorTemp));

            //OverHeatIndicator.SetActive(false);
            //OverheatIndicatorOn.SetActive(false);

            //Debug.Log("SetTempmeter Function is called");

        }

        void SetTempMeterIgnitionOff()
        {
            OverHeat = false;

            float currentT = 0;

            if (TempMeter > 0)
            {
                currentT = TempMeter * TempFullAngle / TempFullRange;
            }
            steerfactorTemp = Mathf.Lerp(steerfactorTemp, currentT, 0.05f);

            TempM.localEulerAngles = new Vector3(0, 0, (-steerfactorTemp + 5.0f));

            //Debug.Log("SetTempmeterIgnitionOff Function is called");


        }

        void SetTempMeterOverHeat()
        {

            float currentT = 0;

            if (TempMeter > 0)
            {
                currentT = TempMeter * TempFullAngle / TempFullRange;
            }
            steerfactorTemp = Mathf.Lerp(steerfactorTemp, (currentT + (56.2f)), 0.005f);

            TempM.localEulerAngles = new Vector3(0, 0, (-steerfactorTemp));

            //Debug.Log("SetTempmeterOverHeat Function is called");

            StartCoroutine(SetVariableTrueAfterDelay());

            IEnumerator SetVariableTrueAfterDelay()
            {
                yield return new WaitForSeconds(20);
                OverHeat = true;
                yield return new WaitForSeconds(30);
                if (!Serial)
                {
                    IgnitionIsON = false;
                }
                else
                {
                    IgnitionOffForSeriaclReadScript = true;
                    CurrentIgnition = 0;
                    SerialReadScript.Ignition = 0;
                    SerialIgnitionIsOn = false;
                }

                EngineOverHeatTrueInstructorPanel = false;


                if (IgnitionOnByOverHeatButtonInstructorPanel == true)
                {
                    if (!Serial)
                    {
                        IgnitionIsON = true;
                    }
                    else
                    {
                        SerialIgnitionIsOn = true;
                        IgnitionOffForSeriaclReadScript = false;
                    }
                }
            }

        }


        public void TempMeterOperation()
        {

            if ((IgnitionIsON == true || SerialIgnitionIsOn == true) && EngineOverHeatTrueInstructorPanel == false)
            {
                SetTempMeter();
                OverHeatIndicator.SetActive(false);
                OverheatIndicatorOn.SetActive(false);
            }
            else if ((IgnitionIsON == false || IgnitionOffForSeriaclReadScript == true) && EngineOverHeatTrueInstructorPanel == false)
            {
                SetTempMeterIgnitionOff();
            }
            else if ((IgnitionIsON == true || SerialIgnitionIsOn == true) && EngineOverHeatTrueInstructorPanel == true)
            {
                //StartCoroutine(

                SetTempMeterOverHeat();

                //IEnumerator SetTempMeterOverHeat()
                //{
                //    yield return new WaitForSeconds(15);
                //    SetTempMeterExtremeOverHeat();

                //}
            }


        }

        public void IgnitionOnEngineHeatIndicator()
        {
            if (!Serial)
            {
                IgnitionIsON = true;
            }
            else
            {
                SerialIgnitionIsOn = true;
                IgnitionOffForSeriaclReadScript = false;

            }

        }



        void SetRPMMeter(float RPMMeter)
        {

            float currentRPM = 0;

            if (RPMMeter > 0)
            {
                currentRPM = RPMMeter * RPMFullAngle / RPMFullRange;
            }
            steerfactorRPM = Mathf.Lerp(steerfactorRPM, currentRPM - 116, 0.05f);

            RPMM.localEulerAngles = new Vector3(0, 0, (-steerfactorRPM));

        }

        public void RPMMeterOperation()
        {
            if ((IgnitionIsON == true || SerialIgnitionIsOn == true) && CurrentVehicleGear == "N")
            {
                CurrentEngineRPM = 20.0f;
            }
            else if ((IgnitionIsON == true || SerialIgnitionIsOn == true) && CurrentVehicleGear == "1")
            {
                CurrentEngineRPM = 20.0f + SpeedoMeter * 1.5f;
            }

            else if ((IgnitionIsON == true || SerialIgnitionIsOn == true) && CurrentVehicleGear == "2")
            {
                CurrentEngineRPM = 20.0f + SpeedoMeter * 0.8f;
            }

            else if ((IgnitionIsON == true || SerialIgnitionIsOn == true) && CurrentVehicleGear == "3")
            {
                CurrentEngineRPM = 20.0f + SpeedoMeter * 0.7f;
            }

            else if ((IgnitionIsON == true || SerialIgnitionIsOn == true) && CurrentVehicleGear == "4")
            {
                CurrentEngineRPM = 20.0f + SpeedoMeter * 0.6f;
            }

            else if ((IgnitionIsON == true || SerialIgnitionIsOn == true) && CurrentVehicleGear == "5")
            {
                CurrentEngineRPM = 20.0f + SpeedoMeter * 0.5f;
            }

            else if ((IgnitionIsON == true || SerialIgnitionIsOn == true) && CurrentVehicleGear == "R")
            {
                CurrentEngineRPM = 20.0f + SpeedoMeter * 1.5f;
            }

            else if ((IgnitionIsON == true || SerialIgnitionIsOn == true) && CurrentVehicleGear == "P")
            {
                CurrentEngineRPM = 20.0f;
            }

            else if ((IgnitionIsON == true || SerialIgnitionIsOn == true) && CurrentVehicleGear == "D")
            {
                CurrentEngineRPM = 20.0f + SpeedoMeter * 0.5f;
            }

        }




    }
}