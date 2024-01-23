using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class UI : MonoBehaviour
{
    protected readonly Dictionary<string, TMP_Text> texts = new Dictionary<string, TMP_Text>();
    protected readonly Dictionary<string, Button> buttons = new Dictionary<string, Button>();
    protected readonly Dictionary<string, Image> images = new Dictionary<string, Image>();

    private void Awake()
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

        Init();
    }

    protected abstract void Init();
}
