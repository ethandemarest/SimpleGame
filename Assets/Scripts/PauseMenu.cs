using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    SoundManagerMusic music;
    public GameObject buttonCont;
    public GameObject buttonQuit;
    public GameObject pauseFade;
    public GameObject pausePanel;
    public bool GameIsPaused = false;
    public float animationTime;
    public AnimationCurve animCurve;
    public float opacity;
    float offscreenAmount = 200;

    bool usingController;

    UIControls uiControls;

    void Start()
    {
        music = GameObject.Find("Music Manager").GetComponent<SoundManagerMusic>();
        pauseFade.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0);
        pausePanel.GetComponent<RectTransform>().anchoredPosition = Vector2.down * offscreenAmount;
        uiControls = new UIControls();
        uiControls.PauseMenu.Enable();
    }
    private void Update()
    {
        if (uiControls.PauseMenu.Mute.triggered)
        {
            music.ToggleMute();
        }

        //TOGGLES MOUSE AND CONTROLLER INPUT
        if (uiControls.PauseMenu.Mouse.triggered)
        {
            usingController = false;
            EventSystem.current.SetSelectedGameObject(null);

        }
        if (uiControls.PauseMenu.Navigate.triggered && !usingController)
        {
            usingController = true;
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(buttonCont);
        }
    }
        
    public void Resume()
    {
        StopAllCoroutines();

        GameIsPaused = false;


        Time.timeScale = 1f;
        StartCoroutine(LerpOpacity(.5f, 0f));
        pausePanel.GetComponent<RectTransform>().anchoredPosition = Vector2.down * offscreenAmount;
        gameObject.SetActive(false);
    }
    public void Pause()
    {
        StopAllCoroutines();
        StartCoroutine(LerpPosUp(pausePanel.GetComponent<RectTransform>().anchoredPosition, Vector3.zero));
        GameIsPaused = true;

        Time.timeScale = 0f;
        
        
        ///SETS FIRST SELECTED BUTTON
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(buttonCont);
    }

    public void QuitGame()
    {
        Debug.Log("The Game was quit");
        Application.Quit();
    }

    IEnumerator LerpOpacity(float start, float end)
    {
        float timeElapsed = 0;
        while (timeElapsed < animationTime)
        {
            float t = timeElapsed / animationTime;
            t = animCurve.Evaluate(t);
            opacity = Mathf.Lerp(start, end, t);
            timeElapsed += Time.unscaledDeltaTime;
            yield return null;
            pauseFade.GetComponent<Image>().color = new Color(1f, 1f, 1f, opacity);
        }
        opacity = end;
    }
    IEnumerator LerpPosUp(Vector3 start, Vector3 end)
    {
        buttonCont.SetActive(true);
        buttonQuit.SetActive(true);
        float timeElapsed = 0;
        while (timeElapsed < animationTime)
        {
            float t = timeElapsed / animationTime;
            t = animCurve.Evaluate(t);
            pausePanel.GetComponent<RectTransform>().anchoredPosition = Vector3.Lerp(start, end, t);
            timeElapsed += Time.unscaledDeltaTime;
            yield return null;
        }
        pausePanel.GetComponent<RectTransform>().anchoredPosition = end;
    }
}
