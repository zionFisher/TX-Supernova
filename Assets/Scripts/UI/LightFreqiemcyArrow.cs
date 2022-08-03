using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFreqiemcyArrow : MonoBehaviour
{
    public FrequencyInfo FI;

    private float range = 365;

    private void Update()
    {
        float frequency = FI.LBC.curFrequency - 385;
        float position = frequency / range * 660 - 660;
        RectTransform rt = GetComponent<RectTransform>();
        rt.localPosition = new Vector3(position, rt.localPosition.y, rt.localPosition.z);
    }
}