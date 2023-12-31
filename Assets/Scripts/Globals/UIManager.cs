using System.Collections.Generic;

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
        return _dialogStack.Peek();
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

    public void OpenDialog<T>(T dialog) where T : Dialog
    {
        if (_dialogStack.Count > 0)
            _dialogStack.Peek().gameObject.SetActive(false);
        
        _dialogStack.Push(dialog);
        dialog.gameObject.SetActive(true);
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
