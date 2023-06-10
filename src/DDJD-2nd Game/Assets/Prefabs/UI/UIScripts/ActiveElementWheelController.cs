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
    private float slotSize;
    private Player playerController;
    void Start()
    {
        slotSize = edgeObject.transform.position.x - transform.position.x;
        playerController = GameObject.Find("Player").GetComponent<Player>();
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
                }
                if (angle < -135 || angle > 135)
                {
                    playerController.UpdateElement(electricityElement);
                }
                if (angle > 45 && angle < 135)
                {
                    if (mouseVec.y > 0)
                    {
                        playerController.UpdateElement(fireElement);
                    }
                    else
                    {
                        playerController.UpdateElement(earthElement);
                    }
                }
            }
        }
    }
}
