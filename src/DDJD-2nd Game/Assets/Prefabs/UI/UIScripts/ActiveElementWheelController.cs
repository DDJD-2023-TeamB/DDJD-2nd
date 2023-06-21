using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ActiveElementWheelController : MonoBehaviour
{
    [SerializeField]
    private GameObject edgeObject;

    [SerializeField]
    private Element fireElement;

    [SerializeField]
    private Element earthElement;

    [SerializeField]
    private Element windElement;

    [SerializeField]
    private Element electricityElement;

    [SerializeField]
    private GameObject fireElementButtonObject;

    [SerializeField]
    private GameObject earthElementButtonObject;

    [SerializeField]
    private GameObject windElementButtonObject;

    [SerializeField]
    private GameObject electricityElementButtonObject;

    [SerializeField]
    private GameObject activeElementDescriptionObject;

    [SerializeField]
    private ActiveElementButton fireElementButton;

    [SerializeField]
    private ActiveElementButton earthElementButton;

    [SerializeField]
    private ActiveElementButton windElementButton;

    [SerializeField]
    private ActiveElementButton electricityElementButton;
    private float slotSize;
    private Player playerController;

    [SerializeField]
    private TextMeshProUGUI activeElementDescription;

    void Start()
    {
        slotSize = edgeObject.transform.position.x - transform.position.x;
        playerController = GameObject.Find("Player").GetComponent<Player>();
        //activeElementDescription = activeElementDescriptionObject.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        Vector2 mouseVec = Input.mousePosition - transform.position;
        if (mouseVec.magnitude < slotSize)
        {
            float angle = Vector2.Angle(mouseVec, transform.right);
            Debug.DrawRay(transform.position, mouseVec, Color.yellow);
            if (angle > -45 && angle < 45 && !earthElementButton.IsLocked)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    playerController.UpdateElement(earthElement);
                    setSprites("Earth");
                }
                activeElementDescription.text = "Earth";
            }
            if (angle < -135 || angle > 135 && !electricityElementButton.IsLocked)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    playerController.UpdateElement(electricityElement);
                    setSprites("Electricity");
                }
                activeElementDescription.text = "Electricity";
            }
            if (angle > 45 && angle < 135)
            {
                if (mouseVec.y > 0 && !fireElementButton.IsLocked)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        playerController.UpdateElement(fireElement);
                        setSprites("Fire");
                    }
                    activeElementDescription.text = "Fire";
                }
                else if (!windElementButton.IsLocked)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        playerController.UpdateElement(windElement);
                        setSprites("Wind");
                    }
                    activeElementDescription.text = "Wind";
                }
            }
        }
        else
        {
            activeElementDescription.text = "";
        }
    }

    void setSprites(string element)
    {
        fireElementButton.setActive(false);
        earthElementButton.setActive(false);
        windElementButton.setActive(false);
        electricityElementButton.setActive(false);
        if (element == "Fire")
        {
            fireElementButton.setActive(true);
        }
        else if (element == "Earth")
        {
            earthElementButton.setActive(true);
        }
        else if (element == "Wind")
        {
            windElementButton.setActive(true);
        }
        else if (element == "Electricity")
        {
            electricityElementButton.setActive(true);
        }
    }

    public void SetUnlockedElements(List<Element> elements)
    {
        fireElementButton.SetLocked(!elements.Contains(fireElement));
        earthElementButton.SetLocked(!elements.Contains(earthElement));
        windElementButton.SetLocked(!elements.Contains(windElement));
        electricityElementButton.SetLocked(!elements.Contains(electricityElement));
    }
}
