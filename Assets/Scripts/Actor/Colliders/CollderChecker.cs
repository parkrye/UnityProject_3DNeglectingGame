using UnityEngine;
using UnityEngine.Events;

public enum PhysicEventType
{
    Enter,
    Stay,
    Exit,
}
public class CollderChecker : MonoBehaviour
{
    private UnityEvent<Collider> _triggerEnter = new UnityEvent<Collider>(), 
        _triggerStay = new UnityEvent<Collider>(), 
        _triggerExit = new UnityEvent<Collider>();
    private UnityEvent<Collision> _collisionEnter = new UnityEvent<Collision> (),
        _collisionStay = new UnityEvent<Collision>(),
        _collisionExit = new UnityEvent<Collision>();

    public void AddTriggerEvent(PhysicEventType eventType, UnityAction<Collider> action)
    {
        switch(eventType)
        {
            case PhysicEventType.Enter:
                _triggerEnter.AddListener(action);
                break;
            case PhysicEventType.Stay:
                _triggerStay.AddListener(action);
                break;
            case PhysicEventType.Exit:
                _triggerExit.AddListener(action);
                break;
        }
    }

    public void AddCollisionEvent(PhysicEventType eventType, UnityAction<Collision> action)
    {
        switch(eventType)
        {
            case PhysicEventType.Enter:
                _collisionEnter.AddListener(action);
                break;
            case PhysicEventType.Stay:
                _collisionStay.AddListener(action);
                break;
            case PhysicEventType.Exit:
                _collisionExit.AddListener(action);
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _triggerEnter?.Invoke(other);
    }

    private void OnTriggerStay(Collider other)
    {
        _triggerStay?.Invoke(other);
    }

    private void OnTriggerExit(Collider other)
    {
        _triggerExit?.Invoke(other);
    }

    private void OnCollisionEnter(Collision collision)
    {
        _collisionEnter?.Invoke(collision);
    }

    private void OnCollisionStay(Collision collision)
    {
        _collisionStay?.Invoke(collision);
    }

    private void OnCollisionExit(Collision collision)
    {
        _collisionExit?.Invoke(collision);
    }
}
