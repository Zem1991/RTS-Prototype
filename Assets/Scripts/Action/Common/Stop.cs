using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stop : Action 
{
    public override bool Finalize(Actor caster, Vector3 targetPosition, Actor targetActor)
    {
        caster.ClearActionInstances();
        //TODO: stop attacking?
        caster.SetDestination(caster.transform.position, null);
        Debug.Log("Stop!");
        return true;
    }
}
