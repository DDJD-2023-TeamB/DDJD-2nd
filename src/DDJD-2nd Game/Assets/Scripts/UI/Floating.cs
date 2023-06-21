using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Floating : MonoBehaviour
{
    private Transform _mainCam;
    private Transform _lookAt;

    private float _gap = 0.1f;
    public float animationOffsetValue;

    private Vector3 _initialPosition;

    [SerializeField]
    private bool _useParent = true;

    // Start is called before the first frame update
    void Start()
    {
        _mainCam = Camera.main.transform;
        if (_useParent)
        {
            _lookAt = gameObject.transform.parent.transform.parent.transform;
            transform.position += new Vector3(
                0,
                GetLookAtHeightOffset() / 2 + GetObjectAtHeightOffset() + _gap,
                0
            );
            transform.localScale = new Vector3(
                transform.localScale.x / _lookAt.localScale.x,
                transform.localScale.y / _lookAt.localScale.y,
                transform.localScale.z / _lookAt.localScale.z
            );
        }

        _initialPosition = transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = _initialPosition + new Vector3(0, animationOffsetValue, 0);

        if (_useParent)
        {
            transform.LookAt(_mainCam.position);
        }
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

        return heightOffset * _lookAt.localScale.y;
    }

    private float GetObjectAtHeightOffset()
    {
        // Get the RectTransform component of the parent object
        RectTransform parentRectTransform = GetComponent<RectTransform>();
        // Get the preferred height of the text component
        float height = parentRectTransform.rect.size.y;

        return height * parentRectTransform.localScale.y;
    }
}
