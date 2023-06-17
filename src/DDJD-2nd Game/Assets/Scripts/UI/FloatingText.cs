using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    public float animationOffsetValue;

    private Vector3 _offset;
    private Transform _lookAt;
    private Camera _mainCam;
    private TextMeshProUGUI _text;

    private float _heightFactor = 0.6f;
    private float _visibilityThreshold = 50f;

    private float _fadeDistance = 5f;

    // Start is called before the first frame update
    void Start()
    {
        if(gameObject.transform.parent.transform.parent != null)
        {
            _lookAt = gameObject.transform.parent.transform.parent.transform;
            _mainCam = Camera.main;
            _text = GetComponent<TextMeshProUGUI>();
            _offset = new Vector3(0, GetLookAtHeightOffset() * _heightFactor , 0);
        }   
    }

    // Update is called once per frame
    void Update()
    {
        if (_lookAt != null)
        {
            Vector3 pos = _mainCam.WorldToScreenPoint(_lookAt.position + _offset + new Vector3(0,animationOffsetValue, 0));

            if (transform.position != pos)
            {
                float distance = Vector3.Distance(_mainCam.transform.position, _lookAt.position);
                float scaleFactor = 1f / distance * 10;

                transform.position = pos;
                transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
                float alpha = Mathf.Clamp01((distance - _visibilityThreshold) / _fadeDistance);

                Color textColor = _text.color;
                textColor.a = 1f - alpha;
                _text.color = textColor;
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
