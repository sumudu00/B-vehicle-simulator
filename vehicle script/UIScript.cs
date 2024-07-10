using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class UIScript : MonoBehaviour {

    public bool SerialStateDrive = false;
    public Button SerialButtonDrive;
    public string PortNameDrive;
    public InputField PortInputFieldDrive;

    public bool SerialStateDrive2 = false;
    public Button SerialButtonDrive2;
    public string PortNameDrive2;
    public InputField PortInputFieldDrive2;

    public bool SerialWriteInd = false;
    public Button SerialButtWriteInd;
    public string PortNameWriteInd;
    public InputField PortInFldWriteInd;

    public bool SerialWritePlat = false;
    public Button SerialButtWritePlat;
    public string PortNameWritePlat;
    public InputField PortInFldWritePlat;

    public Text GearValText;
    public string GearVal;

    public Text SpeedValText;
    public string SpeedVal;

    public Text StartKeyText;
    public string StartKeyVal;

    public Text IgnitionText;
    public string IgnitionVal;

    public bool SetRangesMode = false;
    public Button RangesModeButton; // COMMENTED BY TKJ FOR TESTING

    // Use this for initialization
    void Start () {
		
	}
	
    public void SetSerialStateDrive()
    {
        if (SerialStateDrive)
            SerialStateDrive = false;
        else
            SerialStateDrive = true;
    }

    public void SetSerialStateDrive2()
    {
        if (SerialStateDrive2)
            SerialStateDrive2 = false;
        else
            SerialStateDrive2 = true;
    }

    public void SetSerialWriteInd()
    {
        if (SerialWriteInd)
            SerialWriteInd = false;
        else
            SerialWriteInd = true;
    }

    public void SetSerialWritePlat()
    {
        if (SerialWritePlat)
            SerialWritePlat = false;
        else
            SerialWritePlat = true;
    }

    public void SetDataRanges()
    {
        if (SetRangesMode)
            SetRangesMode = false;
        else
            SetRangesMode = true;
       // SetRangesMode = true;
    }

    public void StopSettingRanges()
    {
        SetRangesMode = false;
    }



    void Update()
    {
        //if (GearVal == null || GearVal == "0")
        //{
        //    GearValText.text = "N";
        //}
        //else if (GearVal == "6")
        //{
        //    GearValText.text = "R";
        //}
        //else
        //{
        //    GearValText.text = GearVal;
        //}



        //if (SpeedVal == null || SpeedVal == "0")
        //{
        //    SpeedValText.text = "0" + " kmph";
        //}
        //else
        //    SpeedValText.text = SpeedVal + " kmph";

        //if (StartKeyVal == "1")
        //{
        //    StartKeyText.text = "ON";
        //}
        //else
        //{
        //    StartKeyText.text = "OFF";
        //}

        //if (IgnitionVal == "1")
        //{
        //    IgnitionText.text = "ON";
        //}
        //else
        //{
        //    IgnitionText.text = "OFF";
        //}

        PortNameDrive = PortInputFieldDrive.text;
        ColorBlock ButtonColors = SerialButtonDrive.colors;

        PortNameDrive2 = PortInputFieldDrive2.text;
        ColorBlock ButtonColors2 = SerialButtonDrive2.colors;

        //PortNameWriteInd = PortInFldWriteInd.text;
        //ColorBlock ButtClrsWriteInd = SerialButtWriteInd.colors;

        //PortNameWritePlat = PortInFldWritePlat.text;
        //ColorBlock ButtClrsWritePlat = SerialButtWritePlat.colors;



        if (SerialStateDrive)
        {
            ButtonColors.highlightedColor = Color.red;
            SerialButtonDrive.colors = ButtonColors;
            SerialButtonDrive.GetComponentInChildren<Text>().text = "Driving Parameters Data OFF";
        }

        else
        {
            ButtonColors.highlightedColor = Color.green;
            SerialButtonDrive.colors = ButtonColors;
            //SerialButton.GetComponent<Button>().image.color = Color.green;
            SerialButtonDrive.GetComponentInChildren<Text>().text = "Driving Parameters Data ON";
        }

        if (SerialStateDrive2)
        {
            ButtonColors2.highlightedColor = Color.red;
            SerialButtonDrive2.colors = ButtonColors2;
            SerialButtonDrive2.GetComponentInChildren<Text>().text = "Driving Parameters Data OFF";
        }

        else
        {
            ButtonColors2.highlightedColor = Color.green;
            SerialButtonDrive2.colors = ButtonColors;
            //SerialButton.GetComponent<Button>().image.color = Color.green;
            SerialButtonDrive2.GetComponentInChildren<Text>().text = "Driving Parameters Data ON";
        }

        //if (SerialWriteInd)
        //{
        //    ButtClrsWriteInd.highlightedColor = Color.red;
        //    SerialButtWriteInd.colors = ButtClrsWriteInd;
        //    SerialButtWriteInd.GetComponentInChildren<Text>().text = "Dashboard Indicators Data OFF";
        //}

        //else
        //{
        //    ButtClrsWriteInd.highlightedColor = Color.green;
        //    SerialButtWriteInd.colors = ButtClrsWriteInd;
        //    //SerialButton.GetComponent<Button>().image.color = Color.green;
        //    SerialButtWriteInd.GetComponentInChildren<Text>().text = "Dashboard Indicators Data ON";
        //}

        //if (SerialWritePlat)
        //{
        //    ButtClrsWritePlat.highlightedColor = Color.red;
        //    SerialButtWritePlat.colors = ButtClrsWritePlat;
        //    SerialButtWritePlat.GetComponentInChildren<Text>().text = "Motion Platform Data OFF";
        //}

        //else
        //{
        //    ButtClrsWritePlat.highlightedColor = Color.green;
        //    SerialButtWritePlat.colors = ButtClrsWritePlat;
        //    //SerialButton.GetComponent<Button>().image.color = Color.green;
        //    SerialButtWritePlat.GetComponentInChildren<Text>().text = "Motion Platorm Data ON";
        //}
        ColorBlock RangesModeButtonColor = RangesModeButton.colors;  // COMMENTED BY TKJ FOR TESTING
        if (SetRangesMode)
        {
            RangesModeButtonColor.highlightedColor = Color.red;
            RangesModeButton.colors = RangesModeButtonColor;
            RangesModeButton.GetComponentInChildren<Text>().text = "Get Ranges Mode OFF";
        }

        else
        {
            RangesModeButtonColor.highlightedColor = Color.green;
            RangesModeButton.colors = RangesModeButtonColor;
            RangesModeButton.GetComponentInChildren<Text>().text = "Get Ranges Mode ON";
        }

    }

}
