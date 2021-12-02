using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EuphoryBarComponent : MonoBehaviour

{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetEuphory(int euphory)
    {
        slider.value = euphory;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

   /* public void SetMinEuphory(int euphory)
    {
        slider.minValue = euphory;
        slider.value = euphory;
        fill.color = gradient.Evaluate(1f);
    }
   */

    public void SetMaxEuphory(int euphory)
    {
        slider.maxValue = euphory;
        slider.value = euphory;
        fill.color = gradient.Evaluate(1f);
    }
}
