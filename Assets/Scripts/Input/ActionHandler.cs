using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionHandler : MonoBehaviour
{
    //[Header("Default actions")]

    //[Header("Current action")]
    [Header("Action")]
    [SerializeField] private Action action;
    [SerializeField] private ActionInputState actionInputState;

    //[Header("Current target")]
    [Header("Target")]
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private Actor targetActor;
    
    public Action GetAction() { return action; }
    public ActionInputState GetActionInputState() { return actionInputState; }

    public void ClearAction()
    {
        action = null;
        actionInputState = ActionInputState.NONE;
        targetPosition = Vector3.zero;
        targetActor = null;
    }

    public void SetAction(Action action)
    {
        this.action = action;
        if (action.GetActionType() == ActionType.SHOW_OPTIONS)
        {
            actionInputState = ActionInputState.SELECT_OPTION;
        }
        else if (!action.IsSelfCast())
        {
            actionInputState = ActionInputState.SELECT_TARGET;
        }
        else
        {
            actionInputState = ActionInputState.NONE;
            ExecuteAction();
        }
    }

    public bool ExecuteAction()
    {
        //TODO: add an check here before doing whatever Action!
        //if (false)
        //{
        //    return false;
        //}

        PlayerManager pm = PlayerManager.Instance;
        Player localPlayer = pm.GetLocalPlayer();
        List<Actor> actorList = localPlayer.GetSelection();

        ActorManager am = ActorManager.Instance;
        //TODO: when pressing SHIFT, Actions can be queued on each selected Actor.
        am.ExecuteAction(action, actorList, targetPosition, targetActor);

        ClearAction();
        return true;
    }

    public void SetTarget(Vector3 targetPosition, Actor targetActor)
    {
        this.targetPosition = targetPosition;
        this.targetActor = targetActor;
    }
}
