using UnityEngine;

public class HiveSpawning : HiveState
{

    public static HiveSpawning Create(Hive target)
    {
        HiveSpawning state = HiveState.Create<HiveSpawning>(target);
        return state;
    }

    public override void StateStart()
    {
        base.StateStart();
    }

    public override void StateUpdate()
    {
        foreach (var pos in target.spawnPoints)
        {
            Instantiate(target.flyPrefab, pos.position, Quaternion.identity);
        }

        SetState(HiveIdle.Create(target));
    }
}