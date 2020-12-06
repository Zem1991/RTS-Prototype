using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Action : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private ActionType actionType;
    [SerializeField] private bool isSelfCast;
    [SerializeField] private float minCastRange;
    [SerializeField] private float maxCastRange;
    [SerializeField] private float cooldownCurrent;
    [SerializeField] private float cooldownMaximum;

    public ActionType GetActionType() { return actionType; }
    public bool IsSelfCast() { return isSelfCast; }
}
