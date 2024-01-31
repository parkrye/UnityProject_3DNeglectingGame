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

    public void OpenView(View view)
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

    public void OpenDialog(Dialog dialog)
    {
        if (_dialogStack.Count > 0)
        {
            var peek = _dialogStack.Peek();
            peek.gameObject.SetActive(false);
            peek.CloseDialog();
            if (peek.Equals(dialog))
            {
                _dialogStack.Pop();
                return;
            }
        }

        _dialogStack.Push(dialog);
        dialog.gameObject.SetActive(true);
        dialog.OpenDialog();
    }

    public void CloseCurrentDialog()
    {
        if (_dialogStack.Count == 0)
            return;

        _dialogStack.Pop().gameObject.SetActive(false);
    }

    public void CloseAllDialog()
    {
        while(_dialogStack.Count > 0 )
        {
            _dialogStack.Peek().gameObject.SetActive(false);
            _dialogStack.Peek().CloseDialog();
            _dialogStack.Pop();
        }
    }
}
