using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutTitleScreen : MonoBehaviour
{

    public GameObject uiTitleScreengo;
    public CanvasGroup uiTitleScreen;


    public float animationDuration = 0.25f;

    public void fadeOut()
    {
        StartCoroutine(FadeTitle(uiTitleScreen, uiTitleScreen.alpha, 0, animationDuration));
    }

    public IEnumerator FadeTitle(CanvasGroup uiText, float start, float end, float lerpTime)
    {
        float startTime = Time.time;
        float currentAnimTime = Time.time - startTime;
        float progress = currentAnimTime / lerpTime;


        while (true)
        {
            currentAnimTime = Time.time - startTime;
            progress = currentAnimTime / lerpTime;

            uiText.alpha = Mathf.Lerp(start, end, progress);

            if (progress >= 1)
                break;

            yield return new WaitForEndOfFrame();
        }

        uiTitleScreengo.SetActive(false);
        
    }
}
