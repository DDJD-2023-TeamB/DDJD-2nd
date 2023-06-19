using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Floating : MonoBehaviour
{
    private Transform _mainCam;
    private Transform _lookAt;
    public float animationOffsetValue;

    private Vector3 _initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        _mainCam = Camera.main.transform;
        _lookAt = gameObject.transform.parent.transform.parent.transform;
        transform.position += new Vector3(
            0,
            GetLookAtHeightOffset() / 2 + GetObjectAtHeightOffset() / 2,
            0
        );
        _initialPosition = transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = _initialPosition + new Vector3(0, animationOffsetValue, 0);
        transform.LookAt(_mainCam.position);
    }

    // Calculate the height offset based on the bounds of the _lookAt GameObject
    private float GetLookAtHeightOffset()
    {
        float heightOffset = 0f;

        // Check if the _lookAt GameObject has a Renderer component
        Renderer renderer = _lookAt.GetComponent<Renderer>();
        if (renderer != null)
        {
            heightOffset = renderer.bounds.size.y;
        }
        else
        {
            Collider collider = _lookAt.GetComponent<Collider>();
            if (collider != null)
            {
                heightOffset = collider.bounds.size.y;
            }
        }

        return heightOffset;
    }

    private float GetObjectAtHeightOffset()
    {
        // Get the RectTransform component of the parent object
        RectTransform parentRectTransform = GetComponent<RectTransform>();

        // Get the preferred height of the text component
        float preferredHeight = LayoutUtility.GetPreferredHeight(parentRectTransform);

        return preferredHeight * parentRectTransform.localScale.y;
    }
}
