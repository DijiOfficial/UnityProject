using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampireAnimation : BaseAnimationController
{
    public void HandleAttackRanged()
    {
        if (_animator == null) return;
        _animator.SetTrigger("AttackFar");
    }
    public void HandleAttackClose(bool isGrabAttack)
    {
        if (_animator == null) return;

        if (isGrabAttack)
            _animator.SetTrigger("Grab");
        else
            _animator.SetTrigger("AttackClose");
    }
}
