using UnityEngine;
using UnityEngine.Events;

public class NormalAnimationController : MonoBehaviour
{
    private Animator _animator;

    public UnityEvent AttackEndEvent = new UnityEvent();
    public UnityEvent HitEndEvent = new UnityEvent();
    public UnityEvent DieEndEvent = new UnityEvent();
    public UnityEvent RecoveryEndEvent = new UnityEvent();
    public UnityEvent DizzyEndEvent = new UnityEvent();

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    
    public bool PlayMoveAnimation()
    {
        try
        {
            _animator.SetBool("OnMove", true);
            return true;
        }
        catch
        {
            return false;
        }
    }
    
    public bool StopMoveAnimation()
    {
        try
        {
            _animator.SetBool("OnMove", false);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool PlayAttackAnimation()
    {
        try
        {
            _animator.SetTrigger("OnAttack");
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool PlayHitAnimation()
    {
        try
        {
            _animator.SetTrigger("OnHit");
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool PlayDieAnimation()
    {
        try
        {
            _animator.SetTrigger("OnDie");
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool PlayRecoveryAnimation()
    {
        try
        {
            _animator.SetTrigger("OnRecovery");
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool PlayDizzyAnimation()
    {
        try
        {
            _animator.SetTrigger("OnDizzy");
            return true;
        }
        catch
        {
            return false;
        }
    }

    public void OnAttackEndEvent()
    {
        AttackEndEvent?.Invoke();
    }

    public void OnHitEndEvent()
    {
        HitEndEvent?.Invoke();
    }

    public void OnDieEndEvent()
    {
        DieEndEvent?.Invoke();
    }
    
    public void OnRecoveryEndEvent()
    {
        RecoveryEndEvent?.Invoke();
    }

    public void OnDizzyEndEvent()
    {
        DizzyEndEvent?.Invoke();
    }
}
