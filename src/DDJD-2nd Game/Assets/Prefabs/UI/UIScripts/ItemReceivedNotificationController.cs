using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemReceivedNotificationController : MonoBehaviour
{
    private DateTime startTime;
    private bool animationActive = false;

    [SerializeField]
    private float entryExitDuration = 0.5f;

    [SerializeField]
    private float displayDuration = 4f;

    [SerializeField]
    private TextMeshProUGUI itemNameComponent;

    private Animator _animator;

    private ItemObject _item;

    private RectTransform rectTransform;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void StartAnimation()
    {
        gameObject.SetActive(true);
        _animator.SetTrigger("Start");
        startTime = DateTime.Now;
        animationActive = true;
    }

    public void EndAnimation()
    {
        _animator.SetTrigger("Stop");
        StartCoroutine(WaitAndDisable());

        animationActive = false;
    }

    private IEnumerator WaitAndDisable()
    {
        yield return new WaitForSeconds(1.0f);
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (animationActive)
        {
            TimeSpan delta = DateTime.Now - startTime;
            int deltaMillis = delta.Milliseconds + delta.Seconds * 1000;
            if (deltaMillis < entryExitDuration * 1000f) { }
            else if (deltaMillis < (entryExitDuration + displayDuration) * 1000) { }
            else if (deltaMillis < (displayDuration + 2 * entryExitDuration) * 1000) { }
            else
            {
                EndAnimation();
            }
        }
    }

    public void SetItems(List<ItemStack> items)
    {
        string itemNames = "";
        foreach (ItemStack item in items)
        {
            itemNames += item.item.Name + ", ";
        }
        itemNameComponent.text = itemNames;
    }
}
