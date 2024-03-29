﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanel_CommandCard : UIPanel
{
    [Header("Modes")]
    [SerializeField] private UIPanel_CommandCard_Actions actions;
    [SerializeField] private UIPanel_CommandCard_Options options;
    [SerializeField] private UIPanel_CommandCard_Targeting targeting;

    //[Header("Command buttons")]
    //[SerializeField] private UIButton_CommandCard command00;
    //[SerializeField] private UIButton_CommandCard command10;
    //[SerializeField] private UIButton_CommandCard command20;
    //[SerializeField] private UIButton_CommandCard command30;
    //[SerializeField] private UIButton_CommandCard command01;
    //[SerializeField] private UIButton_CommandCard command11;
    //[SerializeField] private UIButton_CommandCard command21;
    //[SerializeField] private UIButton_CommandCard command31;
    //[SerializeField] private UIButton_CommandCard command02;
    //[SerializeField] private UIButton_CommandCard command12;
    //[SerializeField] private UIButton_CommandCard command22;
    //[SerializeField] private UIButton_CommandCard command32;
    
    public void UpdatePanel(Actor actor, ActionInputState actionInputState, Action action)
    {
        if (!actor)
        {
            actions.gameObject.SetActive(false);
            options.gameObject.SetActive(false);
            targeting.gameObject.SetActive(false);
            return;
        }

        switch (actionInputState)
        {
            case ActionInputState.NONE:
                options.gameObject.SetActive(false);
                targeting.gameObject.SetActive(false);
                actions.UpdatePanel(actor);
                actions.gameObject.SetActive(true);
                break;
            case ActionInputState.SELECT_OPTION:
                actions.gameObject.SetActive(false);
                targeting.gameObject.SetActive(false);
                options.UpdatePanel(action);
                options.gameObject.SetActive(true);
                break;
            case ActionInputState.SELECT_TARGET:
                actions.gameObject.SetActive(false);
                options.gameObject.SetActive(false);
                targeting.UpdatePanel(action);
                targeting.gameObject.SetActive(true);
                break;
        }
    }
    
    //public UIButton_CommandCard GetButton(CommandCardButton commandCardButton)
    //{
    //    UIButton_CommandCard result = null;
    //    switch (commandCardButton)
    //    {
    //        case CommandCardButton.BTN_00:
    //            result = command00;
    //            break;
    //        case CommandCardButton.BTN_10:
    //            result = command10;
    //            break;
    //        case CommandCardButton.BTN_20:
    //            result = command20;
    //            break;
    //        case CommandCardButton.BTN_30:
    //            result = command30;
    //            break;
    //        case CommandCardButton.BTN_01:
    //            result = command01;
    //            break;
    //        case CommandCardButton.BTN_11:
    //            result = command11;
    //            break;
    //        case CommandCardButton.BTN_21:
    //            result = command21;
    //            break;
    //        case CommandCardButton.BTN_31:
    //            result = command31;
    //            break;
    //        case CommandCardButton.BTN_02:
    //            result = command02;
    //            break;
    //        case CommandCardButton.BTN_12:
    //            result = command12;
    //            break;
    //        case CommandCardButton.BTN_22:
    //            result = command22;
    //            break;
    //        case CommandCardButton.BTN_32:
    //            result = command32;
    //            break;
    //    }
    //    return result;
    //}
}
