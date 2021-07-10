using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiveState : EnemyState<Hive>
{
      new protected static T Create<T>(Hive target) where T : HiveState
    {
        var state = EnemyState<Hive>.Create<T>(target);
        return state;
    }
}