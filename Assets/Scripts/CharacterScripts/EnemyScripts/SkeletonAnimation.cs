using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SkeletonAnimation : BaseAnimationController
{
    public void HandleAttack()
    {
        if (_animator == null) return;

        _animator.SetTrigger("Attack1h1");
    }

    public void HandleHit()
    {
        if (_animator == null) return;

        // Get the current animator state info
        AnimatorStateInfo currentState = _animator.GetCurrentAnimatorStateInfo(0);

        // Check if the current animation is "Attack1h1"
        if (currentState.IsName("Attack1h1")) return;

        // Set the trigger for the "Hit1" animation
        _animator.SetTrigger("Hit1");
    }
}
