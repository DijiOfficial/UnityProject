using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SkeletonAnimation : BaseAnimationController
{
    public override void HandleAttack()
    {
        if (_animator == null) return;

        _animator.SetTrigger("Attack1h1");
    }

    public void HandleArcherAttack()
    {
        if (!_animator) return;

        _animator.SetTrigger("BowAttack");
    }

    public bool IsAttacking()
    {
        if (_animator == null) return false;

        return (_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1h1") || _animator.GetCurrentAnimatorStateInfo(0).IsName("BowAttack"));
    }

    public override void HandleHit()
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
