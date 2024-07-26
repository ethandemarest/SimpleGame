using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public GameObject playButton;

    UIControls uiControls;
    public bool usingController;


    private void Start()
    {
        uiControls = new UIControls();
        uiControls.PauseMenu.Enable();

    }
    private void Update()
    {
        //TOGGLES MOUSE AND CONTROLLER INPUT
        if (uiControls.PauseMenu.Mouse.triggered)
        {
            Debug.Log("using mouse");
            usingController = false;
            EventSystem.current.SetSelectedGameObject(null);

        }
        if (uiControls.PauseMenu.Navigate.triggered && !usingController)
        {
            usingController = true;
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(playButton);
        }
    }
}