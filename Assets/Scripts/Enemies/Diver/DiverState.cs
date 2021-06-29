using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiverState : EnemyState<Diver>
{
    protected SpriteRenderer _renderer;
    protected Animator _animator;

    protected static new T Create<T>(Diver target) where T : DiverState
    {
        var state = EnemyState<Diver>.Create<T>(target);
        state._renderer = target.GetComponent<SpriteRenderer>();
        state._animator = target.GetComponent<Animator>();
        return state;
    }
}