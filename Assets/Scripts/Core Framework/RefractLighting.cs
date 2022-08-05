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
        if (k < 0f)
            return Vector3.zero;

        float a;

        if (inputDotNormal > 0.0f)
        {
            a = refractRatio * inputDotNormal - Mathf.Sqrt(k);
        }
        else
        {
            a = refractRatio * inputDotNormal + Mathf.Sqrt(k);
        }

        Vector3 refract = input * refractRatio - normal * a;
        return refract;
    }
}