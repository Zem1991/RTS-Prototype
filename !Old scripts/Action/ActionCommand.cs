using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionCommand : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Actor caster;
    [SerializeField] private Action action;
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private Actor targetActor;

    [Header("Execution")]
    [SerializeField] private bool inExecution = false;

    public void Initialize(Actor caster, Action action, Vector3 targetPosition, Actor targetActor)
    {
        this.caster = caster;
        this.action = action;
        this.targetPosition = targetPosition;
        this.targetActor = targetActor;
    }
    
    protected virtual void Update()
    {
        ExecuteAction();
    }

    ////Copied from an old project.
    //public bool VerifyActionUsage()
    //{
    //    ActionCostError costsError;
    //    ActionRangeTypeError rangeTypeError;
    //    ActionTargetTypeError targetTypeError;
    //    ActionTargetDiplomacyError targetDiploError;
    //    ActionTargetOwnerError targetOwnerError;

    //    bool costs = ActionVerifier.CheckCosts(this, out costsError);
    //    bool range = ActionVerifier.CheckRangeType(this, out rangeTypeError);
    //    bool tType = ActionVerifier.CheckTargetType(this, out targetTypeError);
    //    bool tDiplo = ActionVerifier.CheckTargetDiplomacy(this, out targetDiploError);
    //    bool tOwner = ActionVerifier.CheckTargetOwner(this, out targetOwnerError);

    //    if (costs && range && tType && tDiplo && tOwner)
    //    {
    //        return true;
    //    }
    //    else
    //    {
    //        string errorMsg = "";
    //        FeedbackManagerTools.GetActionErrors(this,
    //            costsError, rangeTypeError, targetTypeError, targetDiploError, targetOwnerError,
    //            out errorMsg);
    //        SendErrorMessage(errorMsg);
    //        return false;
    //    }
    //}

    ////Copied from an old project.
    //public virtual void ApplyActionCosts()
    //{
    //    callingAction.cooldown_current = callingAction.cooldown_maximum;
    //    caster.GetOwnerOrController().money -= moneyCost;
    //    caster.ChangeHP(hpCost, false);
    //    caster.ChangeMP(mpCost, false);
    //}

    public bool InitializeActionExecution()
    {
        action.InitializeActionExecution(caster, targetPosition, targetActor);
        inExecution = true;
        return true;
    }

    public bool ExecuteAction()
    {
        action.ExecuteAction(caster, targetPosition, targetActor);
        return inExecution;
    }

    public bool CheckActionComplete()
    {
        return action.CheckActionComplete(caster, targetPosition, targetActor);
    }

    ////Copied from an old project.
    //public bool SendErrorMessage(string errorMsg)
    //{
    //    if (!fm)
    //        fm = FindObjectOfType<FeedbackManager>();

    //    if (fm)
    //    {
    //        fm.ContextMsg_ActionError(errorMsg);
    //        return true;
    //    }
    //    else
    //    {
    //        Debug.LogWarning("Action generated an error but no FeedbackManager was found to report it to the user.");
    //        return false;
    //    }
    //}
}
