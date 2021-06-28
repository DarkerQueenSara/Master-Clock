using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlerState : EnemyState<Crawler>
{
    new protected SpriteRenderer _renderer;
    protected Animator _animator;

    protected static new T Create<T>(Crawler target) where T : CrawlerState
    {
        var state = EnemyState<Crawler>.Create<T>(target);
        state._renderer = target.GetComponent<SpriteRenderer>();
        state._animator = target.GetComponent<Animator>();
        return state;
    }
}