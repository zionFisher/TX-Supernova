using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectTime : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerManager.Instance.MaxTime = 3;
        }
    }
}