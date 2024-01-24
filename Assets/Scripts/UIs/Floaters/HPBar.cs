public class HPBar : Floater
{    
    public void ModifyBar(float value, bool hideOnFull)
    {
        images.TryGetValue("HPBar", out var image);
        if (image == null)
            return;

        image.fillAmount = value;
        if(hideOnFull && value == 1f)
            image.enabled = false;
        else
            image.enabled = true;
    }
}
