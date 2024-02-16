public interface IDamagePublisher
{
    public void AddSubscriber(IDamageSubscriber subscriber);

    public void RemoveSubscriber(IDamageSubscriber subscriber);

    public float Publish(float origin);
}


public interface IDamageSubscriber
{
    public float Modifiy(float value);
}