using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EscPanelScript : MonoBehaviour
{
    public GameObject EscMenuPanel;

    // Start is called before the first frame update
    void Start()
    {
        EscMenuPanel.SetActive(false);
    }

    public void EscMenuExit()
    {
        Application.Quit();
    }

    public void EscMenuResume()
    {
        EscMenuPanel.SetActive(false);
    }
    
    public void EscMenuMenu(string sceneName)
    {
        EscMenuPanel.SetActive(false);
        SceneManager.LoadScene(sceneName);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EscMenuPanel.SetActive(true);
        }
    }
}
