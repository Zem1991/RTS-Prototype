using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Action : MonoBehaviour
{
    public virtual bool Initialize(Actor caster, Vector3 targetPosition, Actor targetActor)
    {
        //This is called at the same frame an action is started.
        //Use this method to deduct action costs or to create its initial effects.
        //When TRUE is returned, the action is considered to be successfully initialized.
        return true;
    }

    public virtual bool Execute(Actor caster, Vector3 targetPosition, Actor targetActor)
    {
        //This is called right after the action is started.
        //Use this method for repeating effects.
        //When TRUE is returned, the action is considered to have its execution working as intended.
        return true;
    }

    public virtual bool IsComplete(Actor caster, Vector3 targetPosition, Actor targetActor)
    {
        //This is called right after the Execute method.
        //When TRUE is returned, the action will be considered completed.
        return true;
    }

    public virtual bool Finalize(Actor caster, Vector3 targetPosition, Actor targetActor)
    {
        //This is called right after the action is marked as complete.
        //Use this method to create its final effects.
        //When TRUE is returned, the action is considered to be successfully finalized.
        return true;
    }
}
