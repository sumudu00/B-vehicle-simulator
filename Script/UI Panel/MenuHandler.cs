using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class MenuHandler : MonoBehaviour
{
    public GameObject ButtonsPanel;
    public GameObject SettingsPanel;

    public GameObject ButtonsPanelDev;
    public GameObject ButtonsPanelUsr;

    //public GameObject SettingsPanelDev;
    public bool User = false;
    public bool Technician = false;
    public bool Developer = false;

    public AudioMixer audioMixer;

    public Dropdown resolutionDropdown;
    Resolution[] resolutions;


    //bool HighwayTerrainOn;
    //bool UrbanTerrainOn;

    bool UrbanTerrainBackButton;

    bool UserModeButton = false;

    //public GameObject HighwayTerrain;
    //public GameObject UrbanTerrain;

    public GameObject UIPanelFirstPanel;
    public GameObject UIPanelSecondPanel;

    bool TerrainIsSelected;
    bool DriverLevelIsSelected;
    bool GearModeIsSelected;

    public GameObject StartButtonError; //activate when the user does not select the terrain or driver level or the gear mode
    public GameObject StartButton; //activate when the user select the terrain or driver level or the gear mode properly




    public static bool HighwayTerrainOn;
    public static bool UrbanTerrainOn;

    public static bool UrbanTerrainBeginnerOn;
    public static bool UrbanTerrainIntermediateOn;

    public static bool HighwayTerrainBeginnerOn;
    public static bool HighwayTerrainIntermediateOn;

    //public static bool AutoGearOn;
    //public static bool ManualGearOn;


    void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();


        //UrbanTerrainBackButton = false;
        //UserModeButton = false;

        //UrbanTerrainSelected();
        //HighwayTerrainSelected();

        UrbanBackButtonPressed();





    }

    public void SetResolition(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

    }
    // Recognize the Mode
    public void UserMode()
    {
        User = true;
        Technician = false;
        Developer = false;
        //ButtonsPanelUsr.SetActive(true);
    }

    public void TechMode()
    {
        User = false;
        Technician = true;
        Developer = false;
    }

    public void DevMode()
    {
        User = false;
        Technician = false;
        Developer = true;
    }

    // Technician
    public void SPanelAppear()
    {
        ButtonsPanel.SetActive(false);
        SettingsPanel.SetActive(true);
    }

    public void Save()
    {
        SettingsPanel.SetActive(false);
        if (Technician)
        {
            ButtonsPanel.SetActive(true);
        }
        else if (Developer)
        {
            ButtonsPanelDev.SetActive(true);
        }
    }
    // Developer
    public void SPanelAppearDev()
    {
        ButtonsPanelDev.SetActive(false);
        SettingsPanel.SetActive(true);
    }

    public void SetVol(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }

    public void Quality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void setFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();

    }

    private void Awake()
    {

        //TerrainSelection();
 
    }

    public void Update()
    {
        //BackButtonUrbanTerrain();
        //BacktoUIPanelFromUrbanTerrain();
        //StartButtonOperation();
        
        //StartButtonOperation();
    }


    //public void HighwayTerrainSelected()
    //{

    //    HighwayTerrainOn = true;
    //    UrbanTerrainOn = false;
    //    Debug.Log("Highway Terrain chosen");
    //}

    //public void UrbanTerrainSelected()
    //{

    //    HighwayTerrainOn = false;
    //    UrbanTerrainOn = true;
    //    Debug.Log("Urban Terrain chosen");
    //}

    
    //public void TerrainSelection()
    //{
    //    if (HighwayTerrainOn == true)
    //    {
    //        UrbanTerrain.SetActive(false);
    //        HighwayTerrain.SetActive(true);
    //        Debug.Log("Highway Terrain Activated");
    //    }
    //    else if (UrbanTerrainOn == true)
    //    {
    //        HighwayTerrain.SetActive(false);
    //        UrbanTerrain.SetActive(true);
    //        Debug.Log("Urban Terrain Activated");
    //    }
       

    //}

    //check wheather the terrain is selected or not in the UI Panel
    public void TerrainSelected()
    {
        TerrainIsSelected = true;
        Debug.Log("Terrain Selected");

    }

    //check wheather the driver level is selected or not in the UI Panel
    public void DriverLevelSelected()
    {
        DriverLevelIsSelected = true;
        Debug.Log("Driver Level Selected");
    }

    //check wheather the gear mode is selected or not in the UI Panel
    public void GearModeSelected()
    {
        GearModeIsSelected = true;
        Debug.Log("Gear Mode Selected");
    }


    public void StartButtonOperation()
    {
        if (TerrainIsSelected == true && DriverLevelIsSelected == true && GearModeIsSelected == true)
        {
            StartButtonError.SetActive(false);
            StartButton.SetActive(true);
            Debug.Log("Terrain can Start Successfully");

        }

        else
        {
            StartButton.SetActive(false);
            StartButtonError.SetActive(true);
            Debug.Log("Please select the Terrain, Driver Level and Gear Mode");
        }
    }







    public void HighwayTerrainSelected()
    {

        HighwayTerrainOn = true;
        UrbanTerrainOn = false;
        Debug.Log("Highway Terrain chosen ");
    }

    public void UrbanTerrainSelected()
    {

        HighwayTerrainOn = false;
        UrbanTerrainOn = true;
        Debug.Log("Urban Terrain chosen ");
    }



    public void UrbanBackButtonPressed()
    {
        if (TerrainSelector.UrbanBackButton == true)
        {
            UIPanelFirstPanel.SetActive(false);
            UIPanelSecondPanel.SetActive(true);
        }

    }


    public void UrbanTerrainBeginnerSelected()
    {

        UrbanTerrainBeginnerOn = true;
        UrbanTerrainIntermediateOn = false;
        Debug.Log("Urban Terrain Beginner Level Chosen ");
    }


    public void UrbanTerrainIntermediateSelected()
    {

        UrbanTerrainIntermediateOn = true;
        UrbanTerrainBeginnerOn = false;
        Debug.Log("Urban Terrain Intermediate Level Chosen ");
    }


    public void HighwayTerrainBeginnerSelected()
    {

        HighwayTerrainBeginnerOn = true;
        HighwayTerrainIntermediateOn = false;
        Debug.Log("Highway Terrain Beginner Level Chosen ");
    }


    public void HighwayTerrainIntermediateSelected()
    {

        HighwayTerrainIntermediateOn = true;
        HighwayTerrainBeginnerOn = false;
        Debug.Log("Highway Terrain Intermediate Level Chosen ");
    }



    //connected to the autogear button of the UI Panel 
    public void AutoGearSelected()
    {
        //AutoGearOn = true;
        //ManualGearOn = false;
        EVP.FourWheelGearInput.autoGear = true;
        Debug.Log("AutoGear Mode Chosen ");
    }

    //connected to the manualgear button of the UI Panel 
    public void ManualGearSelected()
    {
        //ManualGearOn = true;
        //AutoGearOn = false;
        EVP.FourWheelGearInput.autoGear = false;
        Debug.Log("ManualGear Mode Chosen ");
    }







    //public void TerrainSelection()
    //{
    //    if (HighwayTerrainOn == true && UrbanTerrainOn == false)
    //    {
    //        HighwayTerrain.SetActive(true);
    //        UrbanTerrain.SetActive(false);
    //        Debug.Log("Highway Terrain Activated");
    //    }
    //    else if (UrbanTerrainOn == true && HighwayTerrainOn == false)
    //    {

    //        UrbanTerrain.SetActive(true);
    //        HighwayTerrain.SetActive(false);
    //        Debug.Log("Urban Terrain Activated");
    //    }


    //}









    //public void BackButtonUrbanTerrain()
    //{
    //    UrbanTerrainBackButton = true;

    //}

    //public void UserModeButtonPressed()
    //{
    //    UserModeButton = true;

    //}

    //public void BacktoUIPanelFromUrbanTerrain()
    //{
    //    if ((UrbanTerrainBackButton == true) || (UserModeButton == true))
    //    {
    //        UIPanelFirstPanel.SetActive(false);
    //        UIPanelSecondPanel.SetActive(true);

    //    }
    //    else
    //    {
    //        UIPanelFirstPanel.SetActive(true);
    //        UIPanelSecondPanel.SetActive(false);

    //    }
    //}





}
