using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveElementButton : MonoBehaviour
{
    [SerializeField]
    private Sprite activeSprite;

    [SerializeField]
    private Sprite inactiveSprite;

    [SerializeField]
    private GameObject _lockedImage;
    Image buttonImage;

    private bool _isLocked = false;

    private void Start()
    {
        buttonImage = this.GetComponent<Image>();
    }

    public void setActive(bool active)
    {
        if (active)
        {
            buttonImage.sprite = activeSprite;
        }
        else
        {
            buttonImage.sprite = inactiveSprite;
        }
    }

    public void SetLocked(bool locked)
    {
        _lockedImage.SetActive(locked);
        _isLocked = locked;
    }

    public bool IsLocked
    {
        get { return _isLocked; }
    }
}
