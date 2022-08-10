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

        if (other.GetComponent<PickUpable>().Pickable == false)
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

        if (isTarget == true)
            return;

        if (targetObject != null)
        {
            targetObject.GetComponent<Renderer>().material.color = targetColor;
            targetObject = null;
        }
    }

    public void PickUp()
    {
        if (targetObject != null && isTarget == false)
        {
            isTarget = true;
            return;
        }
        if (targetObject != null && isTarget == true)
        {
            isTarget = false;
            targetObject.gameObject.GetComponent<Renderer>().material.color = targetColor;
            targetObject = null;
            return;
        }
    }

    private void Update()
    {
        if (isTarget)
        {
            int floorMask = LayerMask.NameToLayer("Floor");
            LayerMask mask = (1 << floorMask);
            Ray detect = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(detect, out RaycastHit detectHit, 1000, mask))
            {
                Vector3 detectPosition = detectHit.point;
                targetObject.transform.position = new Vector3(detectPosition.x, targetObject.transform.position.y, detectPosition.z);
            }
        }
    }
}