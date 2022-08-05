using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PlayerPickUp : MonoBehaviour
{
    private GameObject targetObject;

    private Color targetColor;

    private bool isTarget = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Refract" && other.tag != "Reflect")
            return;

        targetObject = other.gameObject;
        targetColor = targetObject.GetComponent<Renderer>().material.color;

        Color curColor = new Color(0f, 0f, 0f, 0f);

        if (other.tag == "Reflect")
            curColor.a = targetColor.a;

        targetObject.GetComponent<Renderer>().material.color = curColor;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Refract" && other.tag != "Reflect")
            return;

        other.gameObject.GetComponent<Renderer>().material.color = targetColor;
        targetObject = null;
    }

    public void PickUp()
    {
        if (targetObject != null && isTarget == false)
        {
            targetObject.transform.SetParent(transform);
            isTarget = true;
            return;
        }
        if (targetObject != null && isTarget == true)
        {
            targetObject.transform.SetParent(null);
            isTarget = false;
            return;
        }
    }
}