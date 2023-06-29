using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingCanvas : MonoBehaviour
{
    [SerializeField]
    private GameObject _worldCanvas;

    private Floating _worldFloatingIcon;

    [SerializeField]
    private bool _hasMapCanvas = false;

    [SerializeField]
    private GameObject _mapCanvas;

    private Animator _worldCanvasAnimator;
    private Animator _mapCanvasAnimator;

    private GameObject _parent;

    public void Start()
    {
        _worldCanvasAnimator = _worldCanvas.transform.GetChild(0).GetComponent<Animator>();
        _worldFloatingIcon = _worldCanvas.transform.GetChild(0).GetComponent<Floating>();
        if (_hasMapCanvas)
            _mapCanvasAnimator = _mapCanvas.transform.GetChild(0).GetComponent<Animator>();
        _worldFloatingIcon.SetParent(_parent);
    }

    public void SetParent(GameObject obj)
    {
        _parent = obj;
    }

    public void StartAnimation() { }

    public void StopAnimation()
    {
        _worldCanvasAnimator.SetTrigger("Stop");
        if (_hasMapCanvas)
            _mapCanvasAnimator.SetTrigger("Stop");
    }

    public GameObject WorldCanvas
    {
        get { return _worldCanvas; }
    }

    public GameObject MapCanvas
    {
        get { return _mapCanvas; }
    }

    public bool HasMapCanvas
    {
        get { return _hasMapCanvas; }
    }
}
