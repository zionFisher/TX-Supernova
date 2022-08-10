using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Utility
{
    public static IEnumerator WaitForSeconds(float seconds) { yield return new WaitForSeconds(seconds); yield break; }

    public static IEnumerator InvokeAfterSeconds(float seconds, Action method) { yield return new WaitForSeconds(seconds); method(); yield break; }

    public static IEnumerator InvokeBeforeAndAfterSecondes(float seconds, Action before, Action after) { before(); yield return new WaitForSeconds(seconds); after(); yield break; }

    public static void CheckUnassignedVar<T>(T param) { if (param == null) Debug.LogError("Check if there are any unassigned variables."); }

    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}