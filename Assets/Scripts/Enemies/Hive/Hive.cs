using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hive : EnemyBase<Hive>
{
    public float cooldown;

    public List<Transform> spawnPoints;
    public GameObject flyPrefab;

    protected override void Start()
    {
        base.Start();
        if (!started)
        {
            state = HiveIdle.Create(this);
            started = true;
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        if (started) state = HiveIdle.Create(this);
    }

}