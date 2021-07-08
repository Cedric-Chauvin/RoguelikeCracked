using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomSliderAttribute : PropertyAttribute
{

    public readonly string max;

    public CustomSliderAttribute(string maxString)
    {
        max = maxString;
    }

}
