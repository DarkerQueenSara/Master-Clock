using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiveState : EnemyState<Hive>
{
    protected SpriteRenderer renderer;
    protected Animator animator;

    protected static new T Create<T>(Hive target) where T : HiveState
    {
        var state = EnemyState<Hive>.Create<T>(target);
        state.renderer = target.GetComponent<SpriteRenderer>();
        state.animator = target.GetComponent<Animator>();
        return state;
    }
}