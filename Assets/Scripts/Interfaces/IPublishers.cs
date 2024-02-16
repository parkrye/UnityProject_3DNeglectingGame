public interface IFloatPublisher
{
    public void AddSubscriber(IFloatSubscriber subscriber);

    public void RemoveSubscriber(IFloatSubscriber subscriber);
}


public interface IFloatSubscriber
{
    public float Modifiy(float value);
}