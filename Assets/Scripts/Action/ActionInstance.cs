using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionInstance : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Action action;
    [SerializeField] private Actor caster;
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private Actor targetActor;

    [Header("States")]
    [SerializeField] private bool isInExecution = false;

    public Action GetAction() { return action; }

    public bool IsInExecution() { return isInExecution; }

    public void Constructor(Action action, Actor caster, Vector3 targetPosition, Actor targetActor)
    {
        this.action = action;
        this.caster = caster;
        this.targetPosition = targetPosition;
        this.targetActor = targetActor;
    }

    public bool Initialize()
    {
        isInExecution = action.Initialize(caster, targetPosition, targetActor);
        return isInExecution;
    }

    public bool Execute()
    {
        return action.Execute(caster, targetPosition, targetActor);
    }

    public bool IsComplete()
    {
        return action.IsComplete(caster, targetPosition, targetActor);
    }

    public bool Finalize()
    {
        return action.Finalize(caster, targetPosition, targetActor);
    }
}
