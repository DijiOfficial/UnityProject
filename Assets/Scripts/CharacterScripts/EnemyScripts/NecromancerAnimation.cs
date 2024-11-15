using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromancerAnimation : BaseAnimationController
{
    public void HandleSummoning()
    {
        if (_animator == null) return;

        _animator.SetTrigger("Cast Spell");
    }
    public bool IsAttacking()
    {
        if (_animator == null) return false;

        return (_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") || _animator.GetCurrentAnimatorStateInfo(0).IsName("Cast Spell"));
    }

    public override void HandleHit()
    {
        if (_animator == null) return;

        // Get the current animator state info
        AnimatorStateInfo currentState = _animator.GetCurrentAnimatorStateInfo(0);

        // Check if the current animation is "Attack1h1"
        if (currentState.IsName("Attack")) return;

        // Set the trigger for the "Hit1" animation
        _animator.SetTrigger("Hit");
    }
}
