using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : Action 
{
    public override bool Initialize(Actor caster, Vector3 targetPosition, Actor targetActor)
    {
        caster.SetAttackTarget(targetActor);
        return true;
    }

    public override bool IsComplete(Actor caster, Vector3 targetPosition, Actor targetActor)
    {
        //TODO: check if caster can still reach target position, last known position or something
        return targetActor.IsDead();
    }
}
