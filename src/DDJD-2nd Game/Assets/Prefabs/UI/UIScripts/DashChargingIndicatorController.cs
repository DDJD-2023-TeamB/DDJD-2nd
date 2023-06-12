using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashChargingIndicatorController : MonoBehaviour
{
    [SerializeField] private Element fireElement,windElement,electricityElement,earthElement;

    [SerializeField] private List<Sprite> frameList;
    [SerializeField] private Sprite windSprite;
    [SerializeField] private Sprite fireSprite;
    [SerializeField] private Sprite electricitySprite;
    [SerializeField] private Sprite earthSprite;

    public float chargingDuration = 3.0f;

    private int currentFrame = -1;
    public Image elementImage;
    private Image imageComponent;
    private DateTime startTime;

    private void Start()
    {
        imageComponent = GetComponent<Image>();
        startTime = DateTime.Now;
    }

    public void startAnimation()
    {
        gameObject.SetActive(true);
        startTime = System.DateTime.Now;
        currentFrame = 0;
        imageComponent.sprite = frameList[0];
    }

    public void Update()
    {

        float ellapsedMillisseconds = (float)((System.DateTime.Now - startTime).Milliseconds + (System.DateTime.Now - startTime).Seconds * 1000);

        if (ellapsedMillisseconds > chargingDuration * 1000f && currentFrame != -1)
        {
            currentFrame = -1;
            startTime = DateTime.MinValue;
            gameObject.SetActive(false);
        }
        else
        {
            int progress = (int)(ellapsedMillisseconds/(float)(chargingDuration*27.8));

            if (currentFrame != progress)
            {
                imageComponent.sprite = frameList[progress];
                currentFrame = progress;
            }
        }

        
    }

    public void changeElement(Element element)
    {
        if (element == windElement){
            elementImage.sprite = windSprite;
        }
        else if (element == earthElement)
        {
            elementImage.sprite = earthSprite;
        }
        else if (element == fireElement)
        {
            elementImage.sprite = fireSprite;
        }
        else if (element == electricityElement)
        {
            elementImage.sprite = electricitySprite;
        }
        else
        {
            Debug.LogError("invalid argument provided to changeElement in DashCharingIndicatorController.cs");
        }
    }
}