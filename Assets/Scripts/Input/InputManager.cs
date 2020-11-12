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
    [SerializeField] private ActionInput actionInput;
    [SerializeField] private Action action;

    private void Update()
    {
        UIManager uim = UIManager.Instance;
        UIPanel panelUnderCursor = uim.GetPanelUnderCursor();

        UpdateMenus();
        if (!uim.HasMenuOpen())
        {
            CursorActions(panelUnderCursor);

            //switch (actionInput)
            //{
            //    case ActionInput.NONE:
            //        UpdateNoAction();
            //        break;
            //    case ActionInput.SELECT_OPTION:
            //        UpdateActionSelectOption();
            //        break;
            //    case ActionInput.SELECT_TARGET:
            //        UpdateActionSelectTarget();
            //        break;
            //}
        }
    }

    //public bool IsActionInputNone() { return actionInput == ActionInput.NONE; }
    //public bool IsActionInputSelectOption() { return actionInput == ActionInput.SELECT_OPTION; }
    //public bool IsActionInputSelectTarget() { return actionInput == ActionInput.SELECT_TARGET; }

    private void UpdateMenus()
    {
        bool escapeMenu = inputHandler.EscapeMenu();
        //...

        MenuInput newMenuInput = MenuInput.NONE;
        if (escapeMenu) newMenuInput = MenuInput.ESCAPE;
        //...
        if (newMenuInput == MenuInput.NONE) return;

        UIManager uim = UIManager.Instance;
        uim.ToogleMenu(newMenuInput);
    }

    private void CursorActions(UIPanel panelUnderCursor)
    {
        UIManager uim = UIManager.Instance;
        PlayerManager pm = PlayerManager.Instance;
        ActorManager am = ActorManager.Instance;

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
            localPlayer.SetSelection(actorList);
            am.SetSelection(actorList);
        }
    }
}
