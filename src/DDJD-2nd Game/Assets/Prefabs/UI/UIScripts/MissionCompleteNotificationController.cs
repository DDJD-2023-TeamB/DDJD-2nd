using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MissionCompleteNotificationController : MonoBehaviour
{
    private DateTime startTime;
    private bool animationActive = false;

    [SerializeField]
    private float entryExitDuration = 0.5f;

    [SerializeField]
    private float displayDuration = 4f;

    [SerializeField]
    private TextMeshProUGUI missionTitleComponent;

    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void StartAnimation(string missionTitle)
    {
        missionTitleComponent.text = missionTitle;
        gameObject.SetActive(true);
        rectTransform.anchoredPosition = new Vector3(-620, -250, 0);
        startTime = DateTime.Now;
        animationActive = true;
    }

    public void EndAnimation()
    {
        gameObject.SetActive(false);
        rectTransform.anchoredPosition = new Vector3(-620, -250, 0);
        animationActive = false;
    }

    private void Update()
    {
        if (animationActive)
        {
            TimeSpan delta = DateTime.Now - startTime;
            int deltaMillis = delta.Milliseconds + delta.Seconds * 1000;
            if (deltaMillis < entryExitDuration * 1000f)
            {
                rectTransform.anchoredPosition = new Vector3(
                    -620 * (1 - (float)deltaMillis / (entryExitDuration * 1000)),
                    -250,
                    0
                );
            }
            else if (deltaMillis < (entryExitDuration + displayDuration) * 1000)
            {
                rectTransform.anchoredPosition = new Vector3(0, -250, 0);
            }
            else if (deltaMillis < (displayDuration + 2 * entryExitDuration) * 1000)
            {
                rectTransform.anchoredPosition = new Vector3(
                    -620
                        * (
                            (float)(deltaMillis - (entryExitDuration + displayDuration) * 1000)
                            / (entryExitDuration * 1000)
                        ),
                    -250,
                    0
                );
            }
            else
            {
                EndAnimation();
            }
        }
    }
}
