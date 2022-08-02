using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResourcesManager : Singleton<ResourcesManager>
{
    public string UIPrefabPath = "Prefabs/UI/";

    public GameObject LoadUIPrefab(string uiName)
    {
        return Resources.Load<GameObject>(UIPrefabPath + uiName);
    }
}