using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : AbstractSingleton<UIManager>
{
    [Header("Self references")]
    [SerializeField] private UIHandler uiHandler;

    private void Update()
    {
        PlayerManager pm = PlayerManager.Instance;
        Player localPlayer = pm.GetLocalPlayer();
        uiHandler.UpdatePanels(localPlayer);
    }

    public bool HasMenuOpen() { return uiHandler.HasMenuOpen(); }
    public void ToogleMenu(MenuInput menuInput) { uiHandler.ToogleMenu(menuInput); }

    public void DrawSelectionBox(CursorHandler cursorHandler)
    {
        Canvas canvas = GetComponent<Canvas>();
        uiHandler.DrawSelectionBox(cursorHandler, canvas.scaleFactor);
    }
}
