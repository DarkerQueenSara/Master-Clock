using UnityEngine;

public abstract class EnemyState : MonoBehaviour
{
    public bool Initialized { get; protected set; }

    public virtual void StateStart()
    {
        Initialized = true;
    }

    public abstract void StateUpdate();

    public abstract void StateFixedUpdate();
    public abstract void OnGetHit();
}

public abstract class EnemyState<EnemyType> : EnemyState where EnemyType : EnemyBase<EnemyType>
{
    protected EnemyType target;

    protected static T Create<T>(EnemyType target) where T : EnemyState<EnemyType>
    {
        T state = target.gameObject.AddComponent<T>();
        state.target = target;
        return state;
    }

    protected void SetState(EnemyState<EnemyType> state)
    {
        target.SetState(state);
        Destroy(this);
    }
}