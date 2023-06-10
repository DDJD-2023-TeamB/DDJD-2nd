using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ActiveElementWheelController : MonoBehaviour
{
    [SerializeField] private GameObject edgeObject;
    [SerializeField] private Element fireElement;
    [SerializeField] private Element earthElement;
    [SerializeField] private Element windElement;
    [SerializeField] private Element electricityElement;

    [SerializeField] private GameObject fireElementButtonObject;
    [SerializeField] private GameObject earthElementButtonObject;
    [SerializeField] private GameObject windElementButtonObject;
    [SerializeField] private GameObject electricityElementButtonObject;

    private ActiveElementButton fireElementButton;
    private ActiveElementButton earthElementButton;
    private ActiveElementButton windElementButton;
    private ActiveElementButton electricityElementButton;
    private float slotSize;
    private Player playerController;
    void Start()
    {
        slotSize = edgeObject.transform.position.x - transform.position.x;
        playerController = GameObject.Find("Player").GetComponent<Player>();
        fireElementButton = fireElementButtonObject.GetComponent<ActiveElementButton>();
        earthElementButton = earthElementButtonObject.GetComponent<ActiveElementButton>();
        windElementButton = windElementButtonObject.GetComponent<ActiveElementButton>();
        electricityElementButton = electricityElementButtonObject.GetComponent<ActiveElementButton>();
    }
    void Update()
    {
        Vector2 mouseVec = Input.mousePosition - transform.position;
        if (mouseVec.magnitude < slotSize)
        {
            float angle = Vector2.Angle(mouseVec, transform.right);
            Debug.DrawRay(transform.position, mouseVec, Color.yellow);
            if (Input.GetMouseButtonDown(0))
            {
                if (angle > -45 && angle < 45)
                {
                    playerController.UpdateElement(windElement);
                    setSprites("wind");
                }
                if (angle < -135 || angle > 135)
                {
                    playerController.UpdateElement(electricityElement);
                    setSprites("electricity");
                }
                if (angle > 45 && angle < 135)
                {
                    if (mouseVec.y > 0)
                    {
                        playerController.UpdateElement(fireElement);
                        setSprites("fire");
                    }
                    else
                    {
                        playerController.UpdateElement(earthElement);
                        setSprites("earth");
                    }
                }
            }
        }
    }

    void setSprites(string element)
    {
        fireElementButton.setActive(false);
        earthElementButton.setActive(false);
        windElementButton.setActive(false);
        electricityElementButton.setActive(false);
        if (element == "fire")
        {
            fireElementButton.setActive(true);
        }else if (element == "earth")
        {
            earthElementButton.setActive(true);
        }
        else if (element == "wind")
        {
            windElementButton.setActive(true);
        }
        else if (element == "electricity")
        {
            electricityElementButton.setActive(true);
        }
    }
}
