using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBarController : MonoBehaviour
{

    public GameObject outlineImageObject;
    public GameObject fillImageObject;

    public Sprite fireOutline;
    public Sprite waterOutline;
    public Sprite airOutline;
    public Sprite earthOutline;
    public Sprite electricityOutline;
    public Sprite fireFill;
    public Sprite waterFill;
    public Sprite airFill;
    public Sprite earthFill;
    public Sprite electricityFill;

    public string currentType = "water";

    public void changeStaminaBarType(string type)
    {
        currentType = type;
        switch (type)
        {
            case "fire":
                outlineImageObject.GetComponent<Image>().sprite = fireOutline;
                fillImageObject.GetComponent<Image>().sprite = fireFill;
                break;
            case "water":
                outlineImageObject.GetComponent<Image>().sprite = waterOutline;
                fillImageObject.GetComponent<Image>().sprite = waterFill;
                break;
            case "air":
                outlineImageObject.GetComponent<Image>().sprite = airOutline;
                fillImageObject.GetComponent<Image>().sprite = airFill;
                break;
            case "earth":
                outlineImageObject.GetComponent<Image>().sprite = earthOutline;
                fillImageObject.GetComponent<Image>().sprite = earthFill;
                break;
            case "electricity":
                outlineImageObject.GetComponent<Image>().sprite = electricityOutline;
                fillImageObject.GetComponent<Image>().sprite = electricityFill;
                break;
        }
    }

    public void setStaminaBarProgress(float value)
    {
        fillImageObject.GetComponent<Image>().fillAmount = value;
    }

    public float getStaminaBarProgress()
    {
        return fillImageObject.GetComponent<Image>().fillAmount;
    }
}
