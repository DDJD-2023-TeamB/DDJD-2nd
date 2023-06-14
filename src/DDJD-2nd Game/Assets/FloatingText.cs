using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    private Vector3 _offset;
    private Transform _lookAt;
    private Camera _mainCam;
    private float _visibilityThreshold = 50f;

    private float _fadeDistance = 5f;

    // Start is called before the first frame update
    void Start()
    {
        _lookAt = gameObject.transform.parent.transform.parent;
        _mainCam = Camera.main;
        _offset = new Vector3(0, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = _mainCam.WorldToScreenPoint(_lookAt.position + _offset);

        if (transform.position != pos)
        {
            float distance = Vector3.Distance(_mainCam.transform.position, _lookAt.position);
            float scaleFactor = 1f / distance * 10;

            transform.position = pos;
            transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
            float alpha = Mathf.Clamp01((distance - _visibilityThreshold) / _fadeDistance);

            Color textColor = GetComponent<TextMeshProUGUI>().color;
            textColor.a = 1f - alpha;
            GetComponent<TextMeshProUGUI>().color = textColor;
        }
    }
}
