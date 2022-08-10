using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefractObject : MonoBehaviour
{
    public float RefractFactor
    {
        get => _refractFactor;
        set
        {
            if (value == _refractFactor)
                return;

            _refractFactor = value;
        }
    }

    [SerializeField] private float _refractFactor;
}