using UnityEngine;

public class MainView : View
{
    private LevelUpDialog _levelUpDialog;

    protected override void Init()
    {
        base.Init();

        var canvasList = FindObjectsOfType<Canvas>();
        foreach(var canvas in canvasList)
        {
            var levelUpDialog = canvas.transform.Find("LevelUpDialog");
            if(levelUpDialog != null)
            {
                _levelUpDialog = levelUpDialog.GetComponent<LevelUpDialog>();
            }
        }

        GetButton("LevelUpButton").onClick.AddListener(OnLevelUpButtonClick);
    }

    private void OnLevelUpButtonClick()
    {
        Global.UI.OpenDialog(_levelUpDialog);
    }

    private void OnButton1Click()
    {

    }

    private void OnButton2Click()
    {

    }

    private void OnButton3Click()
    {

    }

    private void OnButton4Click()
    {

    }
}
