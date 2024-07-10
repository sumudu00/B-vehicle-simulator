using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.IO.Ports;
using System.Threading;

namespace EVP
{
    public class SerialWriteInd : MonoBehaviour
    {

        private const string PORTPlat = "COM6";
        private SerialPort _portWritePlat = new SerialPort("\\\\.\\" + PORTPlat, 9600);

        public string LatestLinePlat { get; set; }

        public bool _runThreadPlat = true;

        private Thread pollingThreadWritePlat;

        string linePlat;
        public int lenPlat;

        public bool SerialPlat = false;

        public float P;
        public float Q;
        public float R;

        public string PString;
        public string QString;
        public string RString;
        string errorMessagePlat;

        public Text PHeightText;
        public Text QHeightText;
        public Text RHeightText;
        public Text ErrorMessageTextPlat;

        private int CountPlat = 0;
        private string StopStringPlat = "$:100:100:100:*\n";
        public string WrittingDataPlat;

        private FourWheelGearInput VehicleScript;
        private UIScript ButtonScript;
        private SerialInputGear SerialReadScript;
        
        public bool SystemOn = false;
        public bool IgnitionOn = false;

        bool SystemWasON = false;

        // Use this for initialization
        void Start()
        {
            VehicleScript = GetComponent<FourWheelGearInput>();
            SerialReadScript = GetComponent<SerialInputGear>();
            ButtonScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UIScript>();
        }

        public void Stop()
        {
            _runThreadPlat = false;
            _portWritePlat.Close();
        }

        private void RunPollingThreadPlat()
        {
            while (_runThreadPlat)
            {
                PollArduinoPlat();
            }
        }

        private void PollArduinoPlat()
        {
            if (!_portWritePlat.IsOpen)
                return;

            //string tempS = "";
            try
            {
                //string WrittingData = "$" + ":" + PString + ":" + QString + ":" + RString + ":" + "*" + "\n";

                WrittingDataPlat = "$" + ":" + P + ":" + Q + ":" + R + ":" + "*" + "\n";
                
                _portWritePlat.Write(WrittingDataPlat);
                
                Debug.Log(WrittingDataPlat);
                errorMessagePlat = "Writting data successfully";
                ErrorMessageTextPlat.color = Color.green;

                //                byte[] buf = System.Text.ASCIIEncoding.Default.GetBytes(WrittingData);
                //
                //_port.Write(buf, 0, buf.Length);

                //byte[] buf = System.Text.ASCIIEncoding.Default.GetBytes(WrittingDataPlat);

                //_portWritePlat.Write(buf, 0, buf.Length);

            }
            catch (System.Exception)
            {
                //Debug.Log(e);
                Stop();
                pollingThreadWritePlat.Abort();
                SerialPlat = false;
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

            SetPlatformSerial();
            SetSerialDataStringPlat();
        }

        void SetPlatformSerial()
        {
            //P = VehicleScript.Lim_P_Height;
            //Q = VehicleScript.Lim_Q_Height;
            //R = VehicleScript.Lim_R_Height;
            
            //PString = string.Format("{0:G}", P);
            //QString = string.Format("{0:G}", Q);

            P = VehicleScript.P_Heightnew;
            R = VehicleScript.R_Heightnew;
            Q = VehicleScript.Q_Heightnew;

            PString = P.ToString();
            QString = Q.ToString();
            RString = R.ToString();

            //SerialReadScript.PHeightS = PString;
            //SerialReadScript.QHeightS = QString;
            //SerialReadScript.RHeightS = RString;
            //SerialReadScript.ErrorMessageText2S = errorMessage;

            if (_portWritePlat.IsOpen)
                SerialPlat = true;


            try
            {
                if (!_portWritePlat.IsOpen && ButtonScript.SerialWritePlat)
                {
                    _portWritePlat = new SerialPort("\\\\.\\" + ButtonScript.PortNameWritePlat, 9600);
                    _portWritePlat.Open();
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
                    ErrorMessageTextPlat.color = Color.red;
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
                ErrorMessageTextPlat.color = Color.red;
            }
        }

        void SetSerialDataStringPlat()
        {
            PHeightText.text = PString;
            QHeightText.text = QString;
            RHeightText.text = RString;
            ErrorMessageTextPlat.text = errorMessagePlat;
        }

        public void OnApplicationQuit()
        {
            Stop();
            pollingThreadWritePlat.Abort();
            SerialPlat = false;
        }
    }
}
