using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : Action 
{
    public override bool Initialize(Actor caster, Vector3 targetPosition, Actor targetActor)
    {
        caster.SetDestination(targetPosition, targetActor);
        return true;
    }

    public override bool IsComplete(Actor caster, Vector3 targetPosition, Actor targetActor)
    {
        return caster.CheckDestinationReached();
    }
}
