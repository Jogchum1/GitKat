using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject menu;
    public bool active = false;

    private void Start()
    {
        menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Pressed");
            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
            }
            ToggleMenu(!active);
        } 
    }

    private void ToggleMenu(bool state)
    {
        active = state;
        menu.SetActive(state);
        if (state)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void Resume()
    {
        ToggleMenu(!active);
    }

    public void Options()
    {
        Debug.Log("HIER OPTIES?");
    }

    public void Quit()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }

}
