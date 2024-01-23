using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Pool
{
    private Dictionary<string, ObjectPool<GameObject>> poolDict = new Dictionary<string, ObjectPool<GameObject>>();
    private Transform poolContainer;

    public T Get<T>(T original, Vector3 position, Quaternion rotation, Transform parent) where T : Object
    {
        if(original is GameObject)
        {
            GameObject prefab = original as GameObject;
            string key = prefab.name;

            if (poolDict.ContainsKey(key) == false)
                CreatePool(key, prefab);

            GameObject obj = poolDict[key].Get();
            obj.transform.parent = parent;
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            return obj as T;
        }
        else
        {
            Component componenet = original as Component;
            string key = componenet.gameObject.name;

            if (poolDict.ContainsKey(key) == false)
                CreatePool(key, componenet.gameObject);

            GameObject obj = poolDict[key].Get();
            obj.transform.parent = parent;
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            return obj.GetComponent<T>();
        }
    }

    public bool Release<T>(T instance) where T : Object
    {
        if (instance is GameObject)
        {
            GameObject obj = instance as GameObject;
            string key = obj.name;

            if (poolDict.ContainsKey(key) == false)
                return false;

            try
            {
                poolDict[key].Release(obj);
            }
            catch
            {
                return false;
            }
        }
        else
        {
            Component component = instance as Component;
            string key = component.gameObject.name;

            if (poolDict.ContainsKey(key) == false)
                return false;

            try
            {
                poolDict[key].Release(component.gameObject);
            }
            catch
            {
                return false;
            }
        }

        return true;
    }

    private void CreatePool(string key, GameObject prefab)
    {
        GameObject root = new GameObject();
        root.gameObject.name = $"{key}Container";
        root.transform.parent = poolContainer;

        ObjectPool<GameObject> pool = new ObjectPool<GameObject>(
            createFunc: () =>
            {
                GameObject obj = Resource.Instantiate<GameObject>(prefab, Vector3.zero, Quaternion.identity, null);
                obj.gameObject.name = key;
                return obj;
            },
            actionOnGet: (GameObject obj) =>
            {
                obj.gameObject.SetActive(true);
            },
            actionOnRelease: (GameObject obj) =>
            {
                obj.gameObject.SetActive(false);
            },
            actionOnDestroy: (GameObject obj) =>
            {

            }
            );
        poolDict.Add(key, pool);
    }

    public void Reset()
    {
        poolDict.Clear();
    }
}
