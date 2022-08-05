using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class LightDisk : MonoBehaviour
{
    public Color TargatColor = new Color(0f, 0f, 0f, 1f);

    public bool IsLighted;

    public int LowerFrequency = 385;

    public int HigherFrequency = 750;

    private Renderer Renderer;

    private Color previousColor;

    private bool clear = true;

    private void Start()
    {
        Renderer = GetComponent<Renderer>();
        previousColor = Renderer.material.color;
    }

    private void FixedUpdate()
    {
        if (clear == true && Renderer.material.color != previousColor)
        {
            Renderer.material.color = previousColor;
            IsLighted = false;
        }

        clear = true;
    }

    public void LightLightDisk(float frequency)
    {
        if (frequency > HigherFrequency || frequency < LowerFrequency)
            return;

        Renderer.material.color = TargatColor;
        IsLighted = true;
        clear = false;
    }
}