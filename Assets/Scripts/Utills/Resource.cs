using UnityEngine;

public static class Resource
{
    public static T Load<T>(string path) where T : Object
    {
        return Resources.Load<T>(path);
    }

    public static T Instantiate<T>(string path, Vector3 position, Quaternion rotation, Transform parent) where T : Object
    {
        var original = Load<T>(path);
        return Object.Instantiate(original, position, rotation, parent);
    }
}
