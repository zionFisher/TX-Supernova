using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectObject : MonoBehaviour
{
    public float ReflectFactor
    {
        get => _reflectFactor;
        set
        {
            if (value == _reflectFactor)
                return;

            _reflectFactor = value;
        }
    }

    [SerializeField] private float _reflectFactor;
}