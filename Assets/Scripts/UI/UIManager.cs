using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : AbstractSingleton<UIManager>
{
    [Header("Self references")]
    [SerializeField] private UIHandler uiHandler;

    [Header("Menu/Panel processing")]
    //TODO: have this both variabels in UIManger
    [SerializeField] private MenuInput currentMenuInput;
    [SerializeField] private UIPanel panelUnderCursor;

    [Header("Selection processing")]
    [SerializeField] private List<UIPanel> selectionIndicatorList = new List<UIPanel>();

    private void Update()
    {
        PlayerManager pm = PlayerManager.Instance;
        Player localPlayer = pm.GetLocalPlayer();
        uiHandler.UpdatePanels(localPlayer);

        UpdateSelection();
        UpdatePanels();

        //selectionIndicatorList
    }

    private void UpdateSelection() { }

    private void UpdatePanels() { }

    public UIPanel GetPanelUnderCursor() { return panelUnderCursor; }
    public bool HasMenuOpen() { return currentMenuInput != MenuInput.NONE; }

    public void ToogleMenu(MenuInput menuInput)
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
}
