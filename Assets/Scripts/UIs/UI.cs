using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class UI : MonoBehaviour
{
    protected readonly Dictionary<string, TMP_Text> texts;
    protected readonly Dictionary<string, Button> buttons;
    protected readonly Dictionary<string, Image> images;

    private async void Awake()
    {
        var textChildren = GetComponentsInChildren<TMP_Text>();
        foreach(var child in textChildren)
        {
            texts[child.name] = child;
        }

        var buttonChildren = GetComponentsInChildren<Button>();
        foreach(var child in buttonChildren)
        {
            buttons[child.name] = child;
        }

        var imageChildren = GetComponentsInChildren<Image>();
        foreach(var child in imageChildren)
        {
            images[child.name] = child;
        }

        await UniTask.WaitForEndOfFrame();
        Init();
    }

    protected abstract void Init();
}
