using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defend : Action 
{
    public override void InitializeActionExecution(Actor caster, Vector3 targetPosition, Actor targetActor)
    {
        base.InitializeActionExecution(caster, targetPosition, targetActor);
        caster.SetDefendPos(targetPosition);
        caster.SetDefendObj(targetActor);
        caster.UpdateDestinationPosition(targetPosition, targetActor);
    }

    public override void ExecuteAction(Actor caster, Vector3 targetPosition, Actor targetActor)
    {
        base.ExecuteAction(caster, targetPosition, targetActor);
        /*  WE HAVE TWO WAYS TO DEAL WITH THIS ACTION, DEPENDING IF AN TARGET OBJECT WAS GIVEN.
        *  1. TargetObj was given - deal with its hostiles:
        *  1.1.    Attack hostile found by targetObj.
        *  1.2.    Look for hostiles (prioritize the ones given by targetObj).
        *  2. No targetObj was given - use targetPos instead:
        *  2.1.    Attack hostile found.
        *  2.2.    Look for hostiles while it moves towards targetPos.
        *  //TODO: add an HOLD GROUND stance when targetPos is reached, if its not already suggested in the code
        */
        if (targetActor && targetActor.GetComponent<Actor>())
        {
            if (caster.GetAttackObj())
            {
                //Only case where the caster will move towards an enemy while DEFENDING something.
                if ((targetActor as Actor).GetHostiles().IndexOf(caster.GetAttackObj()) != -1)
                    caster.MoveToAttack(caster.GetAttackObj()); 
            }
            AttackObj_FromTargetActor(caster, targetActor);
        }
        else
        {
            AttackObj_FromTargetPosition(caster, targetPosition);
        }

        if (caster.GetAttackObj())
            AttackObj_FromNearby(caster);

        if (!caster.MakeAttack(caster.GetAttackObj()))
            caster.UpdateDestinationPosition(targetPosition, targetActor);
    }

    public override bool CheckActionComplete(Actor caster, Vector3 targetPosition, Actor targetActor)
    {
        if (base.CheckActionComplete(caster, targetPosition, targetActor))
        {
            //As long as the targetObj is alive/exists, keep defending it.
            if (targetActor) return false;
                //return targetActor.isDead;

            /*  However, if no targetObj was given, the caster is expected to HOLD POSITION over its targetPos.
             *  Thats why we don't report the action as complete, or even check if the destination was reached.
             */ //return caster.CheckDestinationReached(targetPos);
        }
        return false;
    }

    private void AttackObj_FromNearby(Actor caster)
    {
        //TODO USE OTHER SORTING METHOD?
        List<Actor> possibleTargets = ActorManager.Instance.SortByDistance(caster.GetHostiles(), caster.transform.position);
        if (possibleTargets.Count > 0)
            caster.SetAttackObj(possibleTargets[0]);
    }

    private void AttackObj_FromTargetPosition(Actor caster, Vector3 targetPosition)
    {
        //Attempts to change caster.attackObj to one of the hostiles from targetPos.
        //Said targetPos must be within sight range of the caster and actually have hostiles.
        float distance = Vector3.Distance(caster.transform.position, targetPosition);
        if (distance <= caster.sightRange)
        {
            //TODO USE OTHER SORTING METHOD?
            List<Actor> possibleTargets = ActorManager.Instance.SortByDistance(caster.GetHostiles(), targetPosition);
            if (possibleTargets.Count > 0)
                caster.SetAttackObj(possibleTargets[0]);
        }
    }

    private void AttackObj_FromTargetActor(Actor caster, Actor targetActor)
    {
        //Attempts to change caster.attackObj to one of the hostiles from targetObj.
        //Said targetObj must be within sight range of the caster and actually have hostiles.
        float distance = Vector3.Distance(caster.transform.position, targetActor.transform.position);
        if (distance <= caster.sightRange)
        {
            //TODO USE OTHER SORTING METHOD?
            List<Actor> possibleTargets = ActorManager.Instance.SortByDistance(targetActor.GetHostiles(), targetActor.transform.position);
            if (possibleTargets.Count > 0)
                caster.SetAttackObj(possibleTargets[0]);
        }
    }
}
