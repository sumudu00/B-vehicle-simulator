using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EVP
{
    public class WarningUIScript : MonoBehaviour
    {
        private SerialWrite BTRPannelScript;

        //Warnings buttons
        public Button BatteryProbButtn;
        public Image BatteryProbImg;
        public Button EngineOilButtn;
        public Image EngineOilImg;
        public Button OilFilterButtn;
        public Image OilFilterImg;
        public Button EngineHeatButtn;
        public Image EngineHeatImg;

        //Other Indicator images
        public Image HeadImg;
        public Image HandBrakeImg;
        public Image LeftSigImg;
        public Image RightSigImg;
        public Image ParkingImg;

        //Warnings from instructor
        public bool BatteryProb = false;
        public bool EngineOil = false;
        public bool OilFilter = false;
        public bool EngineHeat = false;

        //Button Colors
        public Color NotPressNormal = new Color(198, 248, 139);
        public Color NotPressHigh = new Color(220, 243, 194);
        public Color PressNormal = new Color(246, 122, 18);
        public Color PressHigh = new Color(243, 181, 128);
        public Color RedBulbOff = new Color(171, 116, 116);
        public Color GreenBulbOff = new Color(128, 166, 128);

        public GameObject MyTank;

        // Start is called before the first frame update
        void Start()
        {
            //BTRtank = GameObject.FindGameObjectWithTag("Tank");
            BTRPannelScript = MyTank.GetComponent<SerialWrite>();
        }

        //public void GetBatteryProb()
        //{
        //    BatteryProb = !BatteryProb;
        //    SetButtonColor(BatteryProb, BatteryProbButtn);
        //    BTRPannelScript.BatteryON = BatteryProb;
        //}

        //public void GetEngineOil()
        //{
        //    EngineOil = !EngineOil;
        //    SetButtonColor(EngineOil, EngineOilButtn);
        //    BTRPannelScript.EngineOilON = EngineOil;
        //}

        //public void GetOilFilter()
        //{
        //    OilFilter = !OilFilter;
        //    SetButtonColor(OilFilter, OilFilterButtn);
        //    BTRPannelScript.OilFilterON = OilFilter;
        //}

        //public void GetEngineHeat()
        //{
        //    EngineHeat = !EngineHeat;
        //    SetButtonColor(EngineHeat, EngineHeatButtn);
        //    BTRPannelScript.EngineHeatON = EngineHeat;
        //}

        public void SetButtonColor(bool press, Button Button)
        {
            ColorBlock ButtonColor = Button.colors;
            if (!press)
            {
                ButtonColor.normalColor = NotPressNormal;
                ButtonColor.highlightedColor = NotPressHigh;
                Button.colors = ButtonColor;
            }
            else
            {
                ButtonColor.normalColor = PressNormal;
                ButtonColor.highlightedColor = PressHigh;
                Button.colors = ButtonColor;
            }
        }

        // Update is called once per frame
        void Update()
        {
            //BatteryProbImg.color = (BTRPannelScript.Battery == 1) ? Color.red : RedBulbOff;

            //EngineOilImg.color = (BTRPannelScript.EngineOil == 1) ? Color.red : RedBulbOff;

            //OilFilterImg.color = (BTRPannelScript.OilFilter == 1) ? Color.red : RedBulbOff;

            //EngineHeatImg.color = (BTRPannelScript.EngineHeat == 1) ? Color.red : RedBulbOff;

            //HeadImg.color = (BTRPannelScript.HeadLight == 1) ? Color.green : GreenBulbOff;

            //HandBrakeImg.color = (BTRPannelScript.HandBrake == 1) ? Color.red : RedBulbOff;

            //LeftSigImg.color = (BTRPannelScript.LeftSignal == 1) ? Color.green : GreenBulbOff;

            //RightSigImg.color = (BTRPannelScript.RightSignal == 1) ? Color.green : GreenBulbOff;

            //ParkingImg.color = (BTRPannelScript.Parking == 1) ? Color.green : GreenBulbOff;
        }

        
    }
}
