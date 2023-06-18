using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Floating : MonoBehaviour
{
    public float animationOffsetValue;

    private Vector3 _offset;
    private Transform _lookAt;
    private Camera _mainCam;
    private Image _image;

    private float _initialScale;

    private float _halfHeight;
    private RectTransform _rectTransform;
    private float _gap = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        _lookAt = gameObject.transform.parent.transform.parent.transform;
        _mainCam = Camera.main;
        _image = GetComponent<Image>();
        _offset = new Vector3(0, GetLookAtHeightOffset() / 2 + _gap, 0);
        _rectTransform = _image.GetComponent<RectTransform>();
        _initialScale = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (_lookAt != null)
        {
            float _imageScale = transform.localScale.x;
            _halfHeight = _rectTransform.rect.size.y * _imageScale;

            animationOffsetValue = 0;
            Vector3 pos =
                _mainCam.WorldToScreenPoint(
                    _lookAt.position + _offset + new Vector3(0, animationOffsetValue, 0)
                ) + new Vector3(0, _halfHeight, 0);

            if (transform.position != pos)
            {
                float distance = Vector3.Distance(_mainCam.transform.position, _lookAt.position);
                float scaleFactor = ((5f / distance) * _initialScale);

                transform.position = pos;
                transform.localScale = new Vector3(scaleFactor, scaleFactor, 0);
            }
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

        return heightOffset;
    }
}
