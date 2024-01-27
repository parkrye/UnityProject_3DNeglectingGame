public class MainView : View
{
    protected override void Init()
    {
        base.Init();

        GetButton("LevelUpButton").onClick.AddListener(OnLevelUpButtonClick);
    }

    private void OnLevelUpButtonClick()
    {
        Global.UI.OpenDialog<LevelUpDialog>();
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
