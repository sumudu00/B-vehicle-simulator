using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System;
//using BeardedManStudios.Forge.Networking.Generated;

namespace EVP
{
    public class PanalMetersScript : MonoBehaviour
    {
        private bool SerialR = false;
        public bool IgnitionON = false;
        public bool SystemON = false;

        //private Vector3 LastPosition;
        //public float DistanceTraveled = 0;
        //float Distance_Rounded = 0;
        //private float DistanceTank1 = 0;
        //private float DistanceTank2 = 0;
        //public float FuelTank1 = 150;
        //public float FuelTank2 = 150;
        //private float IniFuelTank1 = 150;
        //private float IniFuelTank2 = 150;
        //public float seconds = 0;
        //public int minutes = 0;
        //public int hours = 0;
        //public string[] textLine;
        
        public float RPMMeter = 0;
        public float SpeedoMeter = 0;
        //public float FuelMeter = 0;
        //public float HourMeter = 0;
        //public int Minutes09 = 0;
        //public int Distance = 0;
        //public int Distance09 = 0;
        
        private string RPMMeters;
        private string SpeedoMeters;
        //private string HourMeters;
        //private string Minutes09s;
        //private string Distances;
        //private string Distance09s;

        public Transform RPM;
        public Transform Speedo;

        float RPMFullAngle = 151;
        float RPMFullRange = 6000;
        float steerfactorRPM = 0;

        float SpeedoFullAngle = 225;
        float SpeedoFullRange = 200;
        float steerfactorS = 0;

        //public Text RPMMeterT;
        //public Text SpeedoMeterT;

        private SerialInputGear SerialInputScript;
        private FourWheelGearInput BTRInputScript;
        //private SerialWrite SerialWriteIndScrpt;
        private VehicleControllerGear VehicleScript;
        private UIScript ButtonScript;

        void Start()
        {
            SerialInputScript = GetComponent<SerialInputGear>();
            BTRInputScript = GetComponent<FourWheelGearInput>();
            //SerialWriteIndScrpt = GetComponent<SerialWrite>();
            VehicleScript = GetComponent<VehicleControllerGear>();
            ButtonScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UIScript>();
            
        }
        

        void Update()
        {
            //Indicate the SystemON IgnitionON state in either serial On or OFF states

            //if (networkObject.IsServer)
            //{
            //    if (SerialInputScript.Serial)
            //    {
            //        SerialR = true;
            //        SystemON = (BTRInputScript.SerialSystemIsON == 1) ? true : false;
            //        IgnitionON = (BTRInputScript.SerialIgnitionIsOn) ? true : false;
            //    }
            //    else
            //    {
            //        SerialR = false;
            //        SystemON = (BTRInputScript.SystemIsON) ? true : false;
            //        IgnitionON = (BTRInputScript.IgnitionIsON) ? true : false;
            //    }

            //    SpeedoMeter = (IgnitionON) ? (BTRInputScript.CurrentBTRSpeed) : 0;

            //    networkObject.RPM = RPMMeter;
            //    networkObject.Speed = SpeedoMeter;
            //}
            //else
            //{
            //    SetRPM(networkObject.RPM);
            //    SetSpeedo(networkObject.Speed);
            //}
            
        }

        void SetRPM( float RPMVal)
        {
            float currentRPM = 0;

            if (RPMVal > 0)
            {
                currentRPM = RPMVal * RPMFullAngle / RPMFullRange;
            }
            steerfactorRPM = Mathf.Lerp(steerfactorRPM, currentRPM, 0.05f);

            RPM.localEulerAngles = new Vector3(0, 0, -steerfactorRPM);
        }

        void SetSpeedo(float SpeedVal)
        {
            float currentS = 0;

            if (SpeedVal > 0)
            {
                currentS = SpeedVal * SpeedoFullAngle / SpeedoFullRange;
            }
            steerfactorS = Mathf.Lerp(steerfactorS, currentS, 0.05f);

            Speedo.localEulerAngles = new Vector3(0, 0, -steerfactorS);
        }

        

    }
}
