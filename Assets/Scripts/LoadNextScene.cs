using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextScene : MonoBehaviour
{
    public int sceneToLoad;
    public float animationTime;
    public AnimationCurve animCurve;
    bool hasLoaded;


    public void LoadGame()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    /*

    public IEnumerator FadeToBlack()
    {
        float timeElapsed = 0;
        while (timeElapsed < animationTime)
        {
            float t = timeElapsed / animationTime;
            t = animCurve.Evaluate(t);
            float opacity = Mathf.Lerp(0f, 1f, t); ;
            sp.color = new Color(0f, 0f, 0f, opacity);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        sp.color = new Color(0f, 0f, 0f, 1f);
        LoadGame();
    }
    */
}
