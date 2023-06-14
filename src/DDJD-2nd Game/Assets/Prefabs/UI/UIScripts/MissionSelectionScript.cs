using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MissionSelectionScript : MonoBehaviour, IPointerClickHandler
{
    public GameObject missionsUI;
    public Mission mission;

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
    }

    // Update is called once per frame
    void Update() { }
}
