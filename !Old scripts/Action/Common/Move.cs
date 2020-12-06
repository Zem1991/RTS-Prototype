using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : Action 
{
    public override void InitializeActionExecution(Actor caster, Vector3 targetPosition, Actor targetActor)
    {
        base.InitializeActionExecution(caster, targetPosition, targetActor);
        caster.UpdateDestinationPosition(targetPosition, targetActor);
    }

    public override bool CheckActionComplete(Actor caster, Vector3 targetPosition, Actor targetActor)
    {
        if (base.CheckActionComplete(caster, targetPosition, targetActor))
        {
            return caster.CheckDestinationReached(targetPosition);
            //Vector3 position, destination;
            //position = caster.transform.position;
            //position.y = 0;     //We do this because of Unity's NavMesh
            //destination = caster.destinationPos;
            //destination.y = 0;  //We do this because of Unity's NavMesh
            //if (position == destination)
            //{
            //    return true;
            //}
        }
        return false;
    }
}
