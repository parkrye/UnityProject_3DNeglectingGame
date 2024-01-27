using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    private View _currentView;
    private Stack<Dialog> _dialogStack = new Stack<Dialog>();

    public View GetCurrentView()
    {
        return _currentView;
    }

    public Dialog GetCurrentDialog()
    {
        _dialogStack.TryPeek(out var current);
        return current;
    }

    public void OpenView<T>(T view) where T : View
    {
        CloseCurrentView();

        _currentView = view;
        _currentView.gameObject.SetActive(true);
    }

    public void CloseCurrentView()
    {
        if (_currentView == null)
            return;

        _currentView.gameObject.SetActive(false);
        _currentView = null;
    }

    public void OpenDialog<T>() where T : Dialog
    {
        if (_dialogStack.Count > 0)
            _dialogStack.Peek().gameObject.SetActive(false);

        var dialog = Object.FindFirstObjectByType<T>();
        if(_dialogStack.Peek().Equals(dialog) == false)
        {
            _dialogStack.Push(dialog);
            dialog.gameObject.SetActive(true);
        }
        else
        {
            _dialogStack.Peek().gameObject.SetActive(false);
        }
    }

    public void CloseCurrentDialog()
    {
        if (_dialogStack.Count == 0)
            return;

        _dialogStack.Peek().gameObject.SetActive(false);
        _dialogStack.Pop();
    }

    public void CloseAllDialog()
    {
        while(_dialogStack.Count > 0 )
        {
            _dialogStack.Peek().gameObject.SetActive(false);
            _dialogStack.Pop();
        }
    }
}
