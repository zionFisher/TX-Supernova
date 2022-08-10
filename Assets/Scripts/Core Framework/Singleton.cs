using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance != null)
                return _instance;

            string objectName = typeof(T).Name;
            GameObject gObject = GameObject.Find(objectName);

            if (gObject != null)
                _instance = gObject.GetComponent<T>();
            else
                Debug.LogError($"No such singleton class is found in the scene: {objectName}");

            return _instance;
        }
        private set => _instance = value;
    }

    protected virtual void OnDestroy()
    {
        if (_instance != null)
            _instance = null;
    }
}