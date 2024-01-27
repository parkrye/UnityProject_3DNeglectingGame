public class View : UI
{
    protected override void Init()
    {
        if(Global.UI.GetCurrentView() != this)
        {
            gameObject.SetActive(false);
        }
    }
}
