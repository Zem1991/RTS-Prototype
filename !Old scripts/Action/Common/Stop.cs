using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stop : Action 
{
    public override void InitializeActionExecution(Actor caster, Vector3 targetPosition, Actor targetActor)
    {
        base.InitializeActionExecution(caster, targetPosition, targetActor);
        caster.UpdateDestinationPosition(caster.transform.position, null);
    }
}
