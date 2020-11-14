using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : AbstractSingleton<InputManager>
{
    [Header("Self references")]
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private CursorHandler cursorHandler;

    [Header("Action processing")]
    [SerializeField] private Action action;
    [SerializeField] private ActionInputState actionInputState;

    private void Update()
    {
        UIManager uim = UIManager.Instance;

        UpdateMenus();
        if (!uim.HasMenuOpen())
        {
            CursorInteraction();

            CommandCard();

            /*
            //Those require all but their own locks.
            Windows(im, pm);
            Chat(im, pm);
            CandidateAction(im, pm);
            SelectionBox(im, pm);
            Construction(im, pm);

            //Those require all locks at once.
            QuickSelections(im, pm);
            BasicSelectionAndCommands(im, pm);
            */

            switch (actionInputState)
            {
                case ActionInputState.NONE:
                    //UpdateNoAction();
                    break;
                case ActionInputState.SELECT_OPTION:
                    //UpdateActionSelectOption();
                    break;
                case ActionInputState.SELECT_TARGET:
                    //UpdateActionSelectTarget();
                    break;
            }
        }
    }

    private void UpdateMenus()
    {
        bool escapeMenu = inputHandler.EscapeMenu();
        //...

        MenuInputState newMenuInputState = MenuInputState.NONE;
        if (escapeMenu) newMenuInputState = MenuInputState.ESCAPE;
        //...
        if (newMenuInputState == MenuInputState.NONE) return;

        UIManager uim = UIManager.Instance;
        uim.ToogleMenu(newMenuInputState);
    }

    private void CursorInteraction()
    {
        UIManager uim = UIManager.Instance;
        PlayerManager pm = PlayerManager.Instance;
        ActorManager am = ActorManager.Instance;

        UIPanel panelUnderCursor = uim.GetPanelUnderCursor();
        bool selection = inputHandler.CursorSelection();
        bool selectionDown = inputHandler.CursorSelectionDown();
        bool selectionUp = inputHandler.CursorSelectionUp();
        cursorHandler.ReadCursor(panelUnderCursor, selection, selectionDown, selectionUp);

        uim.DrawSelectionBox(cursorHandler);
        if (cursorHandler.HasSelected())
        {
            Vector2 selectionStart = cursorHandler.GetInitialPosScene();
            Vector2 selectionEnd = cursorHandler.GetCurrentPosScene();
            Player localPlayer = pm.GetLocalPlayer();
            List<Actor> actorList = am.GetActors(selectionStart, selectionEnd, localPlayer);
            am.SetSelection(actorList);
        }
    }

    //TODO: send this to an ActionHandler class?
    private void CommandCard()
    {
        CommandCardButton ccBtn = inputHandler.CommandCard();
        if (ccBtn == CommandCardButton.NONE) return;

        PlayerManager pm = PlayerManager.Instance;
        Player localPlayer = pm.GetLocalPlayer();
        Actor selectionRelevantActor = localPlayer.GetSelectionRelevantActor();
        if (!selectionRelevantActor) return;

        CommandCard commandCard = selectionRelevantActor.GetCommandCard();
        switch (ccBtn)
        {
            case CommandCardButton.BTN_00:
                action = commandCard.GetStop();
                break;
            case CommandCardButton.BTN_10:
                action = commandCard.GetMove();
                break;
            case CommandCardButton.BTN_20:
                action = commandCard.GetDefend();
                break;
            case CommandCardButton.BTN_30:
                action = commandCard.GetAttack();
                break;
            case CommandCardButton.BTN_01:
                break;
            case CommandCardButton.BTN_11:
                break;
            case CommandCardButton.BTN_21:
                break;
            case CommandCardButton.BTN_31:
                break;
            case CommandCardButton.BTN_02:
                break;
            case CommandCardButton.BTN_12:
                break;
            case CommandCardButton.BTN_22:
                break;
            case CommandCardButton.BTN_32:
                break;
        }
        if (action) CallAction(action);
    }

    //TODO: send this to an ActionHandler class?
    public void CallAction(Action action)
    {
        this.action = action;
        Debug.Log(action.GetGameName());
        if (!action.IsSelfCast()) actionInputState = ActionInputState.SELECT_TARGET;
        else actionInputState = ActionInputState.NONE;
        //TODO: more!
        //this.action = null;
    }

    public Action GetAction() { return action; }
    public ActionInputState GetActionInputState() { return actionInputState; }
}
