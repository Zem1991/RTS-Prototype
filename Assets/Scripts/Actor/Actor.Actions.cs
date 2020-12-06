using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Actor : MonoBehaviour
{
    [Header("Actions: Parameters")]
    [SerializeField] private CommandCard commandCard;
    [SerializeField] private Queue<ActionInstance> actionInstanceQueue = new Queue<ActionInstance>();
    [SerializeField] private ActionInstance actionInstanceCurrent;

    public CommandCard GetCommandCard() { return commandCard; }
    public Queue<ActionInstance> GetActionInstanceQueue() { return actionInstanceQueue; }
    public ActionInstance GetActionInstanceCurrent() { return actionInstanceCurrent; }

    private bool SelectAndPerformAction()
    {
        if (!actionInstanceCurrent && actionInstanceQueue.Count > 0)
        {
            actionInstanceCurrent = actionInstanceQueue.Dequeue();
        }
        
        if (actionInstanceCurrent)
        {
            bool success = false;
            if (!actionInstanceCurrent.IsInExecution()) success = actionInstanceCurrent.Initialize();
            if (success) success = actionInstanceCurrent.Execute();
            if (success) success = actionInstanceCurrent.IsComplete();
            if (success) success = actionInstanceCurrent.Finalize();
            return true;
        }
        return false;
    }

    public void ClearActionCommands()
    {
        foreach (ActionInstance forAC in actionInstanceQueue)
        {
            Destroy(forAC.gameObject);
        }
        actionInstanceQueue.Clear();
        actionInstanceCurrent = null;
    }

    public void SetActionCommand(ActionInstance actionCommand)
    {
        ClearActionCommands();
        actionInstanceCurrent = actionCommand;
    }

    public void AddActionCommand(ActionInstance actionCommand)
    {
        actionInstanceQueue.Enqueue(actionCommand);
    }

    public void RemoveLastActionCommand()
    {
        actionInstanceQueue.Dequeue();
    }
}
