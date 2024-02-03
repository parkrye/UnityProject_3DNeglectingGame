using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class UI : MonoBehaviour
{
    protected readonly Dictionary<string, TMP_Text> texts = new Dictionary<string, TMP_Text>();
    protected readonly Dictionary<string, Button> buttons = new Dictionary<string, Button>();
    protected readonly Dictionary<string, Image> images = new Dictionary<string, Image>();
    protected readonly Dictionary<string, RectTransform> contents = new Dictionary<string, RectTransform>();
    protected readonly Dictionary<string, Template> templates = new Dictionary<string, Template>();

    private void Awake()
    {
        var templateChildren = GetComponentsInChildren<Template>();
        foreach (var child in templateChildren)
        {
            if (child.name.Contains("!"))
                continue;
            templates[child.name] = child;
        }

        var textChildren = GetComponentsInChildren<TMP_Text>();
        foreach(var child in textChildren)
        {
            if (child.name.Contains("!"))
                continue;
            texts[child.name] = child;
        }

        var buttonChildren = GetComponentsInChildren<Button>();
        foreach(var child in buttonChildren)
        {
            if (child.name.Contains("!"))
                continue;
            buttons[child.name] = child;
        }

        var imageChildren = GetComponentsInChildren<Image>();
        foreach(var child in imageChildren)
        {
            if (child.name.Contains("!"))
                continue;
            images[child.name] = child;
        }

        var scrollChildren = GetComponentsInChildren<ScrollRect>();
        foreach(var child in scrollChildren)
        {
            if (child.name.Contains("!"))
                continue;
            var content = child.GetComponent<RectTransform>();
            while(content.childCount > 0)
            {
                content = content.GetChild(0).GetComponent<RectTransform>();
            }
            contents[child.name] = content;
        }
    }

    public abstract void Init();

    public Template GetTemplate(string name)
    {
        if (templates.ContainsKey(name))
            return templates[name];
        return null;
    }

    public TMP_Text GetText(string name)
    {
        if(texts.ContainsKey(name))
            return texts[name];
        return null;
    }

    public Button GetButton(string name)
    {
        if(buttons.ContainsKey(name))
            return buttons[name];
        return null;
    }

    public Image GetImage(string name)
    {
        if(images.ContainsKey(name))
            return images[name];
        return null;
    }

    public RectTransform GetContent(string name)
    {
        if(contents.ContainsKey(name))
            return contents[name];
        return null;
    }
}
