using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionHandler : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private ActionInstance prefabActionInstance;

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

    public void SetTarget(Vector3 targetPosition, Actor targetActor)
    {
        this.targetPosition = targetPosition;
        this.targetActor = targetActor;
    }

    public void ClearAction()
    {
        action = null;
        actionInputState = ActionInputState.NONE;
        targetPosition = Vector3.zero;
        targetActor = null;
    }

    public void SetAction(Action action, bool addToQueue)
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
            ExecuteAction(addToQueue);
        }
    }

    public bool ExecuteAction(bool addToQueue)
    {
        //TODO: add an check here before doing whatever Action!
        //if (false)
        //{
        //    return false;
        //}

        PlayerManager pm = PlayerManager.Instance;
        Player localPlayer = pm.GetLocalPlayer();
        List<Actor> actorList = localPlayer.GetSelection();

        //ActorManager am = ActorManager.Instance;
        foreach (Actor forActor in actorList)
        {
            ActionInstance newAI = CreateActionInstance(action, forActor, targetPosition, targetActor);
            forActor.SetActionInstance(newAI, addToQueue);
        }

        ClearAction();
        return true;
    }

    public void ExecuteRightClick(bool addToQueue)
    {
        PlayerManager pm = PlayerManager.Instance;
        Player localPlayer = pm.GetLocalPlayer();
        List<Actor> actorList = localPlayer.GetSelection();

        foreach (Actor forActor in actorList)
        {
            Action action = forActor.FindRightClickAction(targetPosition, targetActor);
            ActionInstance newAI = CreateActionInstance(action, forActor, targetPosition, targetActor);
            forActor.SetActionInstance(newAI, addToQueue);
        }
    }

    public ActionInstance CreateActionInstance(Action action, Actor actor, Vector3 targetPosition, Actor targetActor)
    {
        ActionInstance result = Instantiate(prefabActionInstance, actor.transform);
        result.Constructor(action, actor, targetPosition, targetActor);
        return result;
    }
}
