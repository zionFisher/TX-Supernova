using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class LightDisk : MonoBehaviour
{
    public Color TargatColor = new Color(0f, 0f, 0f, 1f);

    private Renderer Renderer;

    private Color previousColor;

    private bool clear = true;

    private void Start()
    {
        Renderer = GetComponent<Renderer>();
        previousColor = Renderer.material.color;
    }

    private void LateUpdate()
    {
        if (clear == true && Renderer.material.color != previousColor)
            Renderer.material.color = previousColor;

        clear = true;
    }

    public void LightLightDisk()
    {
        Renderer.material.color = TargatColor;
        clear = false;
    }
}