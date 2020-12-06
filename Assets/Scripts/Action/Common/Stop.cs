using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stop : Action 
{
    public override bool Initialize(Actor caster, Vector3 targetPosition, Actor targetActor)
    {
        caster.ClearActionCommands();
        //TODO: stop attacking?
        return true;
    }
}
