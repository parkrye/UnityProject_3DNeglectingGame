using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class RuntimeInitializer
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Init()
    {
        var contentsDirectory = new DirectoryInfo("Assets/Contents/Datas");
        foreach (var file in contentsDirectory.GetFiles("*.csv"))
        {
            Debug.Log(file.FullName);
            var stream = file.OpenRead();
            var reader = new StreamReader(stream);
            while (reader.EndOfStream == false)
            {
                Debug.Log(reader.ReadLine());
            }
        }
    }
}
