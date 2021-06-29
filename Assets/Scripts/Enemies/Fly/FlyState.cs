using UnityEngine;

public class FlyState : EnemyState<Fly>
{
    protected SpriteRenderer _renderer;
    protected Animator _animator;

    protected static new T Create<T>(Fly target) where T : FlyState
    {
        var state = EnemyState<Fly>.Create<T>(target);
        state._renderer = target.GetComponent<SpriteRenderer>();
        state._animator = target.GetComponent<Animator>();
        return state;
    }
    
}