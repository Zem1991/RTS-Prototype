using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanel_CommandCard_Actions : MonoBehaviour
{
    [SerializeField] private UIButton_CommandCard command00;
    [SerializeField] private UIButton_CommandCard command10;
    [SerializeField] private UIButton_CommandCard command20;
    [SerializeField] private UIButton_CommandCard command30;
    [SerializeField] private UIButton_CommandCard command02;
    [SerializeField] private UIButton_CommandCard command12;
    [SerializeField] private UIButton_CommandCard command22;
    [SerializeField] private UIButton_CommandCard command32;

    public void UpdatePanel(Actor actor)
    {
        Action stop = null;
        Action move = null;
        Action defend = null;
        Action attack = null;
        //Action cancel = null;

        if (actor)
        {
            CommandCard commandCard = actor.GetCommandCard();
            stop = commandCard.GetStop();
            move = commandCard.GetMove();
            defend = commandCard.GetDefend();
            attack = commandCard.GetAttack();
            //cancel = commandCard.GetCancel();
        }

        GetButton(CommandCardButton.BTN_00).SetAction(stop);
        GetButton(CommandCardButton.BTN_10).SetAction(move);
        GetButton(CommandCardButton.BTN_20).SetAction(defend);
        GetButton(CommandCardButton.BTN_30).SetAction(attack);
        GetButton(CommandCardButton.BTN_02).SetAction(null);
        GetButton(CommandCardButton.BTN_12).SetAction(null);
        GetButton(CommandCardButton.BTN_22).SetAction(null);
        GetButton(CommandCardButton.BTN_32).SetAction(null);
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
