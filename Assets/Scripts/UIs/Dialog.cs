public class Dialog : UI
{
    protected override void Init()
    {
        if(Global.UI.GetCurrentDialog() != this)
        {
            gameObject.SetActive(false);
        }
    }
}
