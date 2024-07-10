using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.IO.Ports;
using System.Threading;

namespace EVP
{
    public class SerialWrite : MonoBehaviour
    {
        /////// Variables of Serial Port of Motion Platform /////////
        ///
        private const string PORTPlat = "COM8";
        private SerialPort _portWritePlat = new SerialPort("\\\\.\\" + PORTPlat, 9600);

        public string LatestLinePlat { get; set; }


        public bool _runThreadPlat = true;

        private Thread pollingThreadWritePlat;

        string linePlat;
        public int lenPlat;

        public bool SerialPlat = false;

        public int YawAngle;
        public int PitchAngle;
        public int RollAngle;

        public string YawAngleS;
        public string PitchAngleS;
        public string RollAngleS;

        //public Text YawAngleText;
        //public Text PitchAngleText;
        //public Text RollAngleText;
        string errorMessagePlat;
        public string WrittingDataPlat;

        //public Text ErrorMessageTextPlat;

        private int CountPlat = 0;
        private string StopStringPlat = "$:100:100:100:*\n";

        private FourWheelGearInput VehicleScript;

        private UIScript ButtonScript;
        private SerialInputGear SerialReadScript;
        private WarningUIScript WarningScript;


        public bool SystemOn = false;
        public bool IgnitionOn = false;

        bool SystemWasON = false;


        // Start is called before the first frame update
        void Start()
        {
            VehicleScript = GetComponent<FourWheelGearInput>();
            SerialReadScript = GetComponent<SerialInputGear>();
            ButtonScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UIScript>();

        }

        public void Stop()
        {
           // Debug.Log("Stopping Function is Working.......");


            ////// Stop writing to Serial Port of Motion Platform ///////
            _runThreadPlat = false;
            //pollingThreadWritePlat.Join();
            pollingThreadWritePlat.Abort();
            _portWritePlat.Close();

            SerialPlat = false;
        }


        private void RunPollingThreadPlat()
        {
            while (_runThreadPlat)
            {
                PollArduinoPlat();
            }
        }



        ////// Write data to Serial Port of Platform ///////
        private void PollArduinoPlat()
        {
            if (!_portWritePlat.IsOpen)
                return;

            //string tempS = "";
            try
            {

                WrittingDataPlat = "$" + ":" + YawAngle + ":" + PitchAngle + ":" + RollAngle + ":" + "*";


                _portWritePlat.WriteLine(WrittingDataPlat);

                //string Data = "C" + val1 + "," + val2 + "," + val3;
                //byte[] buf = System.Text.Encoding.UTF8.GetBytes(WrittingData);
                Debug.Log(WrittingDataPlat);
                errorMessagePlat = "Writting data successfully";
                //ErrorMessageTextPlat.color = Color.green;

                //                byte[] buf = System.Text.ASCIIEncoding.Default.GetBytes(WrittingData);
                //
                //_port.Write(buf, 0, buf.Length);

                //byte[] buf = System.Text.ASCIIEncoding.Default.GetBytes(WrittingDataPlat);

                //_portWritePlat.Write(buf, 0, buf.Length);

            }
            catch (System.Exception e)
            {
                Debug.Log(e);
                errorMessagePlat = "Error in Initializing the PORT";
                //Stop();
                //pollingThreadWritePlat.Abort();
                //SerialPlat = false;
            }

        }

        // Update is called once per frame
        void Update()
        {

            if (SerialReadScript.Serial)
            {
                SystemOn = (VehicleScript.SerialSystemIsON == 1) ? true : false;
                IgnitionOn = (VehicleScript.SerialIgnitionIsOn) ? true : false;
            }
            else
            {
                SystemOn = (VehicleScript.SystemIsON) ? true : false;
                IgnitionOn = (VehicleScript.IgnitionIsON) ? true : false;
            }


            //Write to the Serial PORT
            SetSerialPlatConnection();

            SetSerialDataStringPlat();
        }



        ////// Set Serial connection of Serial Port of Platform ///////
        void SetSerialPlatConnection()
        {
            YawAngle = (int)this.transform.localEulerAngles.y;
            PitchAngle = (int)this.transform.localEulerAngles.x;
            RollAngle = (int)this.transform.localEulerAngles.z;

            YawAngleS = YawAngle.ToString();
            PitchAngleS = PitchAngle.ToString();
            RollAngleS = RollAngle.ToString();


            if (_portWritePlat.IsOpen)
                SerialPlat = true;


            try
            {
                if (!_portWritePlat.IsOpen && ButtonScript.SerialWritePlat)
                {
                    _portWritePlat = new SerialPort("\\\\.\\" + ButtonScript.PortNameWritePlat, 9600);
                    _portWritePlat.Open();
                    //_portWritePlat.WriteTimeout = 1000;
                    //_port.ReadTimeout = 1000;

                    _portWritePlat.DtrEnable = false;       // Since DTR = 0.~DTR = 1 So  DE = 1 
                    _portWritePlat.RtsEnable = true;       // Since RTS = 0,~RTS = 1 So ~RE = 1


                    pollingThreadWritePlat = new Thread(RunPollingThreadPlat) { IsBackground = true };
                    _runThreadPlat = true;
                    pollingThreadWritePlat.Start();

                    CountPlat = 0;
                }
                else if (_portWritePlat.IsOpen && !ButtonScript.SerialWritePlat)
                {
                    while (CountPlat < 11)
                    {
                        WrittingDataPlat = StopStringPlat;
                        PollArduinoPlat();
                        CountPlat++;
                    }
                    Stop();
                    SerialPlat = false;
                    linePlat = null;
                }
                else if (!_portWritePlat.IsOpen)
                {
                    SerialPlat = false;
                    errorMessagePlat = "PORT is not initialized";
                    //ErrorMessageTextPlat.color = Color.red;
                }
                /*else if (_port.IsOpen)
                {
                    Stop();
                    _port.Close();
                    pollingThread.Abort();
                    Serial = false;
                }
                */


            }
            catch (IOException e)
            {
                Debug.Log("IO Error : " + e);
                SerialPlat = false;
                errorMessagePlat = "Error in Initializing the PORT";
                //ErrorMessageTextPlat.color = Color.red;
            }
        }



        void SetSerialDataStringPlat()
        {
            //YawAngleText.text = YawAngleS;
            //PitchAngleText.text = PitchAngleS;
            //RollAngleText.text = RollAngleS;
            //ErrorMessageTextPlat.text = errorMessagePlat;
        }

        public void OnApplicationQuit()
        {
            Stop();
            pollingThreadWritePlat.Abort();
            SerialPlat = false;
        }
    }
}
