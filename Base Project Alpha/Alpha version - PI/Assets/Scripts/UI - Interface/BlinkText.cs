using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkText : MonoBehaviour {

    public CanvasGroup UIText;
    public float animationDuration = 0.75f;

    public void fadeIn()
    {
        StartCoroutine(Fadetext(UIText, UIText.alpha, 1, animationDuration,1));
    }

    public void fadeOut()
    {
        StartCoroutine(Fadetext(UIText, UIText.alpha, 0, animationDuration,0));
    }

    public IEnumerator Fadetext(CanvasGroup UIText, float start, float end, float lerpTime,int state)
    {
        float startTime = Time.time;
        float currentAnimTime = Time.time - startTime;
        float progress = currentAnimTime / lerpTime;


        while (true)
        {
            currentAnimTime = Time.time - startTime;
            progress = currentAnimTime / lerpTime;

            UIText.alpha = Mathf.Lerp(start, end, progress);

            if (progress >= 1)
                break;

            yield return new WaitForEndOfFrame();
        }

        if(state == 0)
        {
            fadeIn();
        }
        else
        {
            fadeOut();
        }
    }

    void Start()
    {
        fadeOut();    
    }

}
