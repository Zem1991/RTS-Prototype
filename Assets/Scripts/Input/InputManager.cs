using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : AbstractSingleton<InputManager>
{
    [Header("Self references")]
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private CursorHandler cursorHandler;
    [SerializeField] private ActionHandler actionHandler;

    private void Update()
    {
        UIManager uim = UIManager.Instance;

        UpdateMenus();
        if (!uim.HasMenuOpen())
        {
            CameraInteraction();
            CursorInteraction();

            CommandCard();

            ActionInputState actionInputState = GetActionInputState();
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

    private void CameraInteraction()
    {
        Vector2 inputCameraMovement = inputHandler.CameraMovement();
        Vector2 cursorEdgeCheck = cursorHandler.GetEdgeCheck();
        //TODO: readd this later!
        //Vector3 finalInput = inputCameraMovement + cursorEdgeCheck;
        Vector3 finalInput = inputCameraMovement;
        CameraManager cm = CameraManager.Instance;
        cm.MoveCamera(finalInput);
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
        bool decisionDown = inputHandler.CursorDecisionDown();
        bool hasAction = actionHandler.GetAction();

        cursorHandler.ReadCursor(panelUnderCursor, selection, selectionDown, selectionUp);
        if (hasAction)
        {
            if (selectionDown)
            {
                cursorHandler.CancelSelecting();
                actionHandler.ClearAction();
                return;
            }

            if (decisionDown)
            {
                if (!panelUnderCursor)
                {
                    Vector3 targetPosition = cursorHandler.GetCurrentPosScene();
                    Actor targetActor = cursorHandler.GetActorFound();
                    bool addToQueue = inputHandler.ShiftModifier();
                    actionHandler.SetTarget(targetPosition, targetActor);
                    actionHandler.ExecuteAction(addToQueue);
                }
                return;
            }
        }
        else
        {
            if (decisionDown)
            {
                if (cursorHandler.IsSelecting())
                {
                    cursorHandler.CancelSelecting();
                }
                else if (!panelUnderCursor)
                {
                    Vector3 targetPosition = cursorHandler.GetCurrentPosScene();
                    Actor targetActor = cursorHandler.GetActorFound();
                    bool addToQueue = inputHandler.ShiftModifier();
                    actionHandler.SetTarget(targetPosition, targetActor);
                    actionHandler.ExecuteRightClick(addToQueue);
                }
                return;
            }
        }

        uim.DrawSelectionBox(cursorHandler);
        if (cursorHandler.HasSelected())
        {
            actionHandler.ClearAction();

            Vector2 selectionStart = cursorHandler.GetInitialPosScene();
            Vector2 selectionEnd = cursorHandler.GetCurrentPosScene();
            List<Actor> actorList = am.GetActors(selectionStart, selectionEnd);

            Player localPlayer = pm.GetLocalPlayer();
            List<Actor> filteredActorList = new List<Actor>();
            foreach (Actor forActor in actorList)
            {
                if (forActor.GetOwner() == localPlayer) filteredActorList.Add(forActor);
            }
            //TODO: allow selecting an single enemy Actor
            am.SetSelection(actorList);
        }
    }

    //TODO: send this to an ActionHandler class?
    private void CommandCard()
    {
        bool useFullGrid = actionHandler.GetActionInputState() == ActionInputState.SELECT_OPTION;
        CommandCardButton ccBtn = inputHandler.CommandCard(useFullGrid);
        if (ccBtn == CommandCardButton.NONE) return;

        PlayerManager pm = PlayerManager.Instance;
        Player localPlayer = pm.GetLocalPlayer();
        Actor selectionRelevantActor = localPlayer.GetSelectionRelevantActor();
        if (!selectionRelevantActor) return;

        Action action = null;
        CommandCard commandCard = selectionRelevantActor.GetCommandCard();
        //TODO: use an different approach for Options
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
        if (action) SetAction(action);
    }

    public ActionInputState GetActionInputState() { return actionHandler.GetActionInputState(); }
    public Action GetAction() { return actionHandler.GetAction(); }

    public void SetAction(Action action)
    {
        bool addToQueue = inputHandler.ShiftModifier();
        actionHandler.SetAction(action, addToQueue);
    }
}
