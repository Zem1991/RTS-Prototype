using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : MonoBehaviour
{
    [Header("Identification")]
    [SerializeField] private string gameName;

    [Header("Graphics")]
    [SerializeField] private Sprite buttonImage;

    [Header("Settings")]
    [SerializeField] private ActionType actionType;
    [SerializeField] private bool isSelfCast;

    public string GetGameName() { return gameName; }
    public Sprite GetButtonImage() { return buttonImage; }
    public ActionType GetActionType() { return actionType; }
    public bool IsSelfCast() { return isSelfCast; }
}
