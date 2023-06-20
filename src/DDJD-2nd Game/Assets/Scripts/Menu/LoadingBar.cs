using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{

    [SerializeField]
	private Slider slider;
    [SerializeField]
	private Gradient gradient;
    [SerializeField]
	private Image bar;

	public void SetMaxValue(int value)
	{
		slider.maxValue = value;
		slider.value = value;

		bar.color = gradient.Evaluate(1f);
	}

    public void SetValue(int value)
	{
		slider.value = value;

		bar.color = gradient.Evaluate(slider.normalizedValue);
	}

}
