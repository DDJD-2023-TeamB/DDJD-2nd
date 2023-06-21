using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashChargingIndicatorController : MonoBehaviour
{
    [SerializeField]
    private Element fireElement,
        windElement,
        electricityElement,
        earthElement;

    [SerializeField]
    private List<Sprite> frameList;

    [SerializeField]
    private Sprite windSprite;

    [SerializeField]
    private Sprite fireSprite;

    [SerializeField]
    private Sprite electricitySprite;

    [SerializeField]
    private Sprite earthSprite;

    private float chargingDuration = 0f;

    [SerializeField]
    private DashSkill fireDash;

    [SerializeField]
    private DashSkill electricityDash;

    [SerializeField]
    private DashSkill earthDash;

    [SerializeField]
    private DashSkill windDash;

    private int currentFrame = -1;
    public Image elementImage;
    private Image imageComponent;
    private DateTime startTime;

    // ugly way to avoid wrong cooldown at the start of the game
    private bool firstDash = true;

    private void Start()
    {
        imageComponent = GetComponent<Image>();
        startTime = DateTime.Now;
    }

    public void startAnimation()
    {
        gameObject.SetActive(true);
        currentFrame = 0;
        imageComponent.sprite = frameList[0];

        float ellapsedMillisseconds = (float)(
            (System.DateTime.Now - startTime).Milliseconds
            + (System.DateTime.Now - startTime).Seconds * 1000
        );
        if (ellapsedMillisseconds > chargingDuration * 1000f)
        { // the counter shouldn't display regular dashes
            startTime = System.DateTime.Now;
        }
        firstDash = false;
    }

    public void Update()
    {
        float ellapsedMillisseconds = (float)(
            (System.DateTime.Now - startTime).Milliseconds
            + (System.DateTime.Now - startTime).Seconds * 1000
        );

        if (
            (ellapsedMillisseconds >= chargingDuration * 1000f && currentFrame != -1)
            || chargingDuration == 0f
            || firstDash
        )
        {
            currentFrame = -1;
            startTime = DateTime.MinValue;
            gameObject.SetActive(false);
        }
        else
        {
            int progress = (int)(
                ellapsedMillisseconds / (chargingDuration * 1000f) * frameList.Count
            );

            if (currentFrame != progress)
            {
                imageComponent.sprite = frameList[progress];
                currentFrame = progress;
            }
        }
    }

    public void changeElement(Element element)
    {
        if (element == windElement)
        {
            elementImage.sprite = windSprite;
            chargingDuration = windDash.DashSkillStats.Cooldown;
        }
        else if (element == earthElement)
        {
            elementImage.sprite = earthSprite;
            chargingDuration = earthDash.SkillStats.Cooldown;
        }
        else if (element == fireElement)
        {
            elementImage.sprite = fireSprite;
            chargingDuration = fireDash.SkillStats.Cooldown;
        }
        else if (element == electricityElement)
        {
            elementImage.sprite = electricitySprite;
            chargingDuration = electricityDash.SkillStats.Cooldown;
        }
        else
        {
            Debug.LogError(
                "invalid argument provided to changeElement in DashCharingIndicatorController.cs"
            );
        }
    }
}
