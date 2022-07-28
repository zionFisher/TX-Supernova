using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RefractLighting
{
    /// <summary>
    /// Calculate refract light.
    /// </summary>
    /// <returns>Refracted Light</returns>
    public static Vector3 Refract(Vector3 input, Vector3 normal, float refractRatio)
    {
        input = input.normalized;
        normal = normal.normalized;

        float inputDotNormal = Vector3.Dot(input, normal);
        float k = 1.0f - refractRatio * refractRatio * (1.0f - inputDotNormal * inputDotNormal);
        Vector3 T = refractRatio * input - (refractRatio * inputDotNormal + Mathf.Sqrt(k)) * normal;

        if (k > 0)
            return T;
        else
            return Vector3.zero;
    }
}