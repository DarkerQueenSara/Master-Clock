using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackActive : StateMachineBehaviour
{
    private PlayerAnimatorInterface animatorInterface = null;
    public int boxIndex;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if (animatorInterface == null)
            animatorInterface = animator.GetComponent<PlayerAnimatorInterface>();

        animatorInterface.ActivateHitbox(boxIndex);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        animatorInterface.DeactivateHitbox(boxIndex);
    }
}