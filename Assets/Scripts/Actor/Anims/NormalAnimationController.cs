using UnityEngine;

public class NormalAnimationController : MonoBehaviour
{
    private Animator _animator;

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
}
