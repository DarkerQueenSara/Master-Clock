using UnityEngine;

public class DiverState : EnemyState<Diver>
{
    protected Animator animator;
    protected AudioManager audioManager;
    
    protected static new T Create<T>(Diver target) where T : DiverState
    {
        var state = EnemyState<Diver>.Create<T>(target);
        state.animator = target.GetComponentInChildren<Animator>();
        state.audioManager = target.GetComponent<AudioManager>();
        return state;
    }
}