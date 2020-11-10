using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : AbstractSingleton<InputManager>
{
    [Header("Self references")]
    [SerializeField] private CursorHandler cursorHandler;
    [SerializeField] private InputHandler inputHandler;

    [Header("Menu processing")]
    [SerializeField] private bool hasMenuOpen;
    [SerializeField] private UIPanel panelUnderCursor;

    [Header("Action processing")]
    [SerializeField] private ActionInput actionInput;
    [SerializeField] private Action action;

    private void Update()
    {
        UpdateMenus();
        if (!hasMenuOpen)
        {
            CursorActions();

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

    public bool IsActionInputNone() { return actionInput == ActionInput.NONE; }
    public bool IsActionInputSelectOption() { return actionInput == ActionInput.SELECT_OPTION; }
    public bool IsActionInputSelectTarget() { return actionInput == ActionInput.SELECT_TARGET; }

    public void EnterPanelUnderCursor(UIPanel uiPanel) { panelUnderCursor = uiPanel; }
    public void ExitPanelUnderCursor(UIPanel uiPanel) { if (panelUnderCursor == uiPanel) panelUnderCursor = null; }

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
        hasMenuOpen = uim.HasMenuOpen();
    }

    private void CursorActions()
    {
        UIManager uim = UIManager.Instance;
        PlayerManager pm = PlayerManager.Instance;

        bool selection = inputHandler.CursorSelection();
        bool selectionDown = inputHandler.CursorSelectionDown();
        bool selectionUp = inputHandler.CursorSelectionUp();
        cursorHandler.ReadCursor(panelUnderCursor, selection, selectionDown, selectionUp);

        uim.DrawSelectionBox(cursorHandler);
        if (cursorHandler.HasSelected())
        {
            Vector2 selectionStart = cursorHandler.GetInitialPosScene();
            Vector2 selectionEnd = cursorHandler.GetCurrentPosScene();
            pm.GetLocalPlayer().SetSelection(selectionStart, selectionEnd);
        }
    }
}
