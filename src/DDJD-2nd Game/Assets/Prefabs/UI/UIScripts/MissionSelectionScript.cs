using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class MissionSelectionScript : MonoBehaviour, IPointerClickHandler
{
    public GameObject missionsUI;
    public Mission mission;
    [SerializeField] private GameObject newIndicator;
    [SerializeField] private TextMeshProUGUI missionTitle;
    private RectTransform rectTransform;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (missionsUI == null)
        {
            Debug.LogError("missionsUI is null!");
            return;
        }
        missionsUI.GetComponent<MissionsUIController>().setActiveMission(mission);
    }

    // Start is called before the first frame update
    void Start()
    {
        missionsUI = GameObject.Find("MissionsUI");
        rectTransform = newIndicator.GetComponent<RectTransform>();
        if(rectTransform == null)
        {
            Debug.Log("Rect transform is null!");
        }
    }

    public void setAsNew(bool isNew)
    {
        if (rectTransform == null)
        {
            rectTransform = newIndicator.GetComponent<RectTransform>();
        }
        if (isNew)
        {
            float textWidth = missionTitle.preferredWidth;
            rectTransform.anchoredPosition = new Vector3(textWidth,0,0);
            Debug.Log("Setting mission as active: " + rectTransform.anchoredPosition);
            newIndicator.SetActive(true);
        }
        else
        {
            newIndicator.SetActive(false);
        }
    }
}
