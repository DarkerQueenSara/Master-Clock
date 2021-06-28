using Extensions;
using UnityEngine;

public class DiverDiving : EnemyState<Diver>
{
    private bool _hit = false;
    private float _diveCoordinates;

    public static DiverDiving Create(Diver target)
    {
        DiverDiving state = DiverState.Create<DiverDiving>(target);
        return state;
    }

    // Start is called before the first frame update
    public override void StateStart()
    {
        base.StateStart();
        _hit = false;
        _diveCoordinates = PlayerEntity.instance.transform.position.x;
    }

    public override void StateFixedUpdate()
    {
        if (!_hit)
        {
            var currentPos = target.rb.position;
            target.rb.MovePosition(new Vector2(_diveCoordinates - currentPos.x, -1).normalized * target.diveSpeed *
                Time.fixedDeltaTime + currentPos);
        }
        else
        {
            SetState(DiverRecovering.Create(target));
        }
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (target.hittables.HasLayer(col.gameObject.layer))
        {
            _hit = true;
        }
    }
}