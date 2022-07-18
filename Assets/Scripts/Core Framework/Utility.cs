using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
    public static void CheckUnassignedVar<T>(T param) { if (param == null) Debug.LogError("Check if there are any unassigned variables."); }
}