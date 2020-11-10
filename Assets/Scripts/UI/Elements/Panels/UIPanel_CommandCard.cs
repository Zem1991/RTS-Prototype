using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanel_CommandCard : UIPanel
{
    [Header("Command buttons")]
    [SerializeField] private UIButton_CommandCard command00;
    [SerializeField] private UIButton_CommandCard command10;
    [SerializeField] private UIButton_CommandCard command20;
    [SerializeField] private UIButton_CommandCard command30;
    [SerializeField] private UIButton_CommandCard command01;
    [SerializeField] private UIButton_CommandCard command11;
    [SerializeField] private UIButton_CommandCard command21;
    [SerializeField] private UIButton_CommandCard command31;
    [SerializeField] private UIButton_CommandCard command02;
    [SerializeField] private UIButton_CommandCard command12;
    [SerializeField] private UIButton_CommandCard command22;
    [SerializeField] private UIButton_CommandCard command32;
    
    public void UpdatePanel(Actor selectedRelevantActor)
    {
        Action stop = null;
        Action move = null;
        Action defend = null;
        Action attack = null;
        Action cancel = null;

        if (selectedRelevantActor)
        {
            CommandCard commandCard = selectedRelevantActor.GetCommandCard();
            stop = commandCard.GetStop();
            move = commandCard.GetMove();
            defend = commandCard.GetDefend();
            attack = commandCard.GetAttack();
            cancel = commandCard.GetCancel();
        }

        GetButton(CommandCardButton.BTN_00).SetAction(stop);
        GetButton(CommandCardButton.BTN_10).SetAction(move);
        GetButton(CommandCardButton.BTN_20).SetAction(defend);
        GetButton(CommandCardButton.BTN_30).SetAction(attack);
        GetButton(CommandCardButton.BTN_01).SetAction(null);
        GetButton(CommandCardButton.BTN_11).SetAction(null);
        GetButton(CommandCardButton.BTN_21).SetAction(null);
        GetButton(CommandCardButton.BTN_31).SetAction(null);
        GetButton(CommandCardButton.BTN_02).SetAction(null);
        GetButton(CommandCardButton.BTN_12).SetAction(null);
        GetButton(CommandCardButton.BTN_22).SetAction(null);
        GetButton(CommandCardButton.BTN_32).SetAction(cancel);
    }
    
    public UIButton_CommandCard GetButton(CommandCardButton commandCardButton)
    {
        UIButton_CommandCard result = null;
        switch (commandCardButton)
        {
            case CommandCardButton.BTN_00:
                result = command00;
                break;
            case CommandCardButton.BTN_10:
                result = command10;
                break;
            case CommandCardButton.BTN_20:
                result = command20;
                break;
            case CommandCardButton.BTN_30:
                result = command30;
                break;
            case CommandCardButton.BTN_01:
                result = command01;
                break;
            case CommandCardButton.BTN_11:
                result = command11;
                break;
            case CommandCardButton.BTN_21:
                result = command21;
                break;
            case CommandCardButton.BTN_31:
                result = command31;
                break;
            case CommandCardButton.BTN_02:
                result = command02;
                break;
            case CommandCardButton.BTN_12:
                result = command12;
                break;
            case CommandCardButton.BTN_22:
                result = command22;
                break;
            case CommandCardButton.BTN_32:
                result = command32;
                break;
        }
        return result;
    }
}
