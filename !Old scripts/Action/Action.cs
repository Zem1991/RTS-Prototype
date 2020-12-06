using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Action : MonoBehaviour
{
    [Header("Identification")]
    [SerializeField] private string gameName;
    [SerializeField] private Sprite buttonImage;

    [Header("Settings")]
    [SerializeField] private ActionType actionType;
    [SerializeField] private bool isSelfCast;
    [SerializeField] private float minCastRange;
    [SerializeField] private float maxCastRange;
    [SerializeField] private float cooldownCurrent;
    [SerializeField] private float cooldownMaximum;

    public string GetGameName() { return gameName; }
    public Sprite GetButtonImage() { return buttonImage; }

    public ActionType GetActionType() { return actionType; }
    public bool IsSelfCast() { return isSelfCast; }

    public virtual void InitializeActionExecution(Actor caster, Vector3 targetPosition, Actor targetActor)
    {
        //Specific details of whatever an action should before its execution are placed on the overridden functions.
    }

    public virtual void ExecuteAction(Actor caster, Vector3 targetPosition, Actor targetActor)
    {
        //Specific details of whatever an action should do during its execution are placed on the overridden functions.
    }

    public virtual bool CheckActionComplete(Actor caster, Vector3 targetPosition, Actor targetActor)
    {
        //Specific details of how an action is considered complete are placed on the overridden functions.
        return true;
    }
}
