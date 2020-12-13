using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Actor : MonoBehaviour
{
    [Header("Actions: Parameters")]
    [SerializeField] private CommandCard commandCard;
    [SerializeField] private List<ActionInstance> actionInstanceQueue = new List<ActionInstance>();
    [SerializeField] private ActionInstance actionInstanceCurrent;

    public CommandCard GetCommandCard() { return commandCard; }
    public List<ActionInstance> GetActionInstanceQueue() { return actionInstanceQueue; }
    public ActionInstance GetActionInstanceCurrent() { return actionInstanceCurrent; }

    private bool ExecuteActions()
    {
        if (!actionInstanceCurrent && actionInstanceQueue.Count > 0)
        {
            actionInstanceCurrent = actionInstanceQueue[0];
            actionInstanceQueue.RemoveAt(0);
        }
        
        if (actionInstanceCurrent)
        {
            bool success = actionInstanceCurrent.IsInExecution();
            if (!success)
            {
                success = actionInstanceCurrent.Initialize();
                if (!success) ClearActionInstanceCurrent();
            }

            if (success) success = actionInstanceCurrent.Execute();
            if (success) success = actionInstanceCurrent.IsComplete();
            if (success)
            {
                actionInstanceCurrent.Finalize();
                ClearActionInstanceCurrent();
            }
            return true;
        }
        return false;
    }

    public void ClearActionInstances()
    {
        foreach (ActionInstance forAC in actionInstanceQueue)
        {
            Destroy(forAC.gameObject);
        }
        actionInstanceQueue.Clear();
        ClearActionInstanceCurrent();
    }

    public void ClearActionInstanceCurrent()
    {
        if (actionInstanceCurrent) Destroy(actionInstanceCurrent.gameObject);
        actionInstanceCurrent = null;
    }

    public bool SetActionInstance(ActionInstance actionInstance, bool addToQueue)
    {
        Action action = actionInstance.GetAction();
        if (!commandCard.HasAction(action)) return false;

        if (addToQueue)
        {
            actionInstanceQueue.Add(actionInstance);
        }
        else
        {
            ClearActionInstances();
            actionInstanceCurrent = actionInstance;
        }
        return true;
    }

    //TODO: add an remove specific action instance?

    public void RemoveLastActionInstance()
    {
        actionInstanceQueue.RemoveAt(0);
    }

    public Action FindRightClickAction(Vector3 targetPosition, Actor targetActor)
    {
        if (!targetActor)
        {
            //TODO: maybe check if targetPosition is valid
            //TODO: use SET RALLY POINT for buildings, besides using MOVE TO for units.
            return commandCard.GetMove();
        }

        PlayerManager pm = PlayerManager.Instance;
        Player localPlayer = pm.GetLocalPlayer();
        if (targetActor.GetOwner() == localPlayer)
        {
            //TODO: some units may have special abilities that could override this action.
            return commandCard.GetDefend();
        }
        else
        {
            //TODO: some units may have special abilities that could override this action.
            return commandCard.GetAttack();
        }
    }
}
