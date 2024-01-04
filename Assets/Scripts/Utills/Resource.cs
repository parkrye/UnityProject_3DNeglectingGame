using UnityEngine;

public static class Resource
{
    public static T Load<T>(string path) where T : Object
    {
        string key = $"{typeof(T)}.{path}";

        return Resources.Load<T>(key) as T;
    }

    public static T Instantiate<T>(string path, Vector3 position, Quaternion rotation, Transform parent) where T : Object
    {
        var original = Resources.Load<T>(path);
        return Object.Instantiate(original, position, rotation, parent);
    }
}
