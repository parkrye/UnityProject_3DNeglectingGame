using System.IO;
using UnityEngine;

public class RuntimeInitializer
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Init()
    {
        var contentsDirectory = new DirectoryInfo("Assets/Contents/Datas");
        foreach(var file in contentsDirectory.GetFiles("*.txt"))
        {
            var stream = file.OpenRead();
            var reader = new StreamReader(stream);
            while(reader.EndOfStream == false)
            {

            }
        }
    }
}
