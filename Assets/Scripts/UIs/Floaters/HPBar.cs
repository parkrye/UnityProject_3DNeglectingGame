public class HPBar : Floater
{    
    public void ModifyBar(float value)
    {
        images.TryGetValue("HPBar", out var image);
        if (image == null)
            return;

        image.fillAmount = value;
        if(value == 1f)
            image.enabled = false;
        else
            image.enabled = true;
    }
}
