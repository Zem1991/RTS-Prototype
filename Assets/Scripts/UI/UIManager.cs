using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : AbstractSingleton<UIManager>
{
    [Header("Self references")]
    [SerializeField] private UIHandler uiHandler;

    [Header("Menu/Panel processing")]
    //TODO: have this both variabels in UIHandler?
    [SerializeField] private UIPanel panelUnderCursor;
    [SerializeField] private MenuInputState currentMenuInputState;

    [Header("Selection processing")]
    [SerializeField] private List<UIPanel> selectionIndicatorList = new List<UIPanel>();

    private void Update()
    {
        uiHandler.UpdatePanels();
    }

    public UIPanel GetPanelUnderCursor() { return panelUnderCursor; }
    public bool HasMenuOpen() { return currentMenuInputState != MenuInputState.NONE; }

    public void ToogleMenu(MenuInputState menuInput)
    {
        //TODO: make this return an MenuInput and use this in UIManager
        uiHandler.ToogleMenu(menuInput);
    }
    public void DrawSelectionBox(CursorHandler cursorHandler)
    {
        Canvas canvas = GetComponent<Canvas>();
        uiHandler.DrawSelectionBox(cursorHandler, canvas.scaleFactor);
    }

    public void EnterPanelUnderCursor(UIPanel uiPanel)
    {
        panelUnderCursor = uiPanel;
    }
    public void ExitPanelUnderCursor(UIPanel uiPanel)
    {
        if (panelUnderCursor == uiPanel) panelUnderCursor = null;
    }

    public void MultipleSelectionButton(List<Actor> actorList)
    {
        ActorManager am = ActorManager.Instance;
        am.SetSelection(actorList);
    }

    public void CommandCardButton(Action action)
    {
        uiHandler.CommandCardButton(action);
    }
}
