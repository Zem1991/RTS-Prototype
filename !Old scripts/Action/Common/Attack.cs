using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : Action 
{
    public override void InitializeActionExecution(Actor caster, Vector3 targetPosition, Actor targetActor)
    {
        base.InitializeActionExecution(caster, targetPosition, targetActor);
        caster.SetAttackPos(targetPosition);
        caster.SetAttackObj(targetActor);
        caster.UpdateDestinationPosition(targetPosition, targetActor);
    }

    public override void ExecuteAction(Actor caster, Vector3 targetPosition, Actor targetActor)
    {
        base.ExecuteAction(caster, targetPosition, targetActor);
        /*  WE HAVE TWO WAYS TO DEAL WITH THIS ACTION, DEPENDING IF AN TARGET OBJECT WAS GIVEN.
        *  1. TargetObj was given. Then we only have to attack it.
        *  2. No targetObj was given - use targetPos instead:
        *  2.1.    Attack hostile found.
        *  2.2.    Look for hostiles while it moves towards targetPos.
        */
        if (targetActor && targetActor.GetComponent<Actor>())
        {
            caster.MoveToAttack(targetActor);
        }
        else
        {
            if (caster.GetAttackObj())
            {
                caster.MoveToAttack(caster.GetAttackObj());
            }
            else
            {
                //TODO USE OTHER SORTING METHOD?
                List<Actor> possibleTargets = ActorManager.Instance.SortByDistance(caster.GetHostiles(), caster.transform.position);
                if (possibleTargets.Count > 0)
                {
                    caster.SetAttackObj(possibleTargets[0]);
                }
            }
        }
        caster.UpdateDestinationPosition(targetPosition, targetActor);
    }

    public override bool CheckActionComplete(Actor caster, Vector3 targetPosition, Actor targetActor)
    {
        if (base.CheckActionComplete(caster, targetPosition, targetActor))
        {
            //As long as the targetObj is alive/exists, keep attacking it.
            if (targetActor) return false;
                //return targetActor.isDead;

            return caster.CheckDestinationReached(targetPosition);
        }
        return false;
    }
}
