using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    [Header("Selection box")]
    [SerializeField] private RectTransform selectionBox;  //TODO: make this its own class?
    [SerializeField] private Vector2 selectionBoxStart;
    [SerializeField] private Vector2 selectionBoxEnd;

    [Header("Static panels")]
    [SerializeField] private UIPanel_Minimap minimap;
    [SerializeField] private UIPanel_SelectionDetails selectionDetails;
    [SerializeField] private UIPanel_CommandCard commandCard;
    
    public bool HasMenuOpen()
    {
        //TODO: For now, no menu exists
        return false;
    }

    public void ToogleMenu(MenuInput menuInput)
    {
        if (!HasMenuOpen())
        {
            switch (menuInput)
            {
                case MenuInput.ESCAPE:
                    //TODO: open THIS menu
                    break;
                case MenuInput.CHAT:
                    //TODO: open THIS menu
                    break;
            }
        }
        else
        {
            if (menuInput == MenuInput.ESCAPE)
            {
                //TODO: close ANY menu
            }

            if (menuInput == MenuInput.CHAT)
            {
                //TODO: close THIS menu
            }
        }
    }

    public void DrawSelectionBox(CursorHandler cursorHandler, float canvasScaleFactor)
    {
        Camera cam = Camera.main;

        Vector3 currentPosScene = cursorHandler.GetCurrentPosScene();
        Vector3 initialPosScene = cursorHandler.GetInitialPosScene();
        if (currentPosScene == initialPosScene)
        {
            selectionBox.gameObject.SetActive(false);
            return;
        }

        selectionBoxStart = cam.WorldToScreenPoint(initialPosScene);
        selectionBoxStart /= canvasScaleFactor;
        selectionBoxEnd = cam.WorldToScreenPoint(currentPosScene);
        selectionBoxEnd /= canvasScaleFactor;

        float minX = Mathf.Min(selectionBoxStart.x, selectionBoxEnd.x);
        float minY = Mathf.Min(selectionBoxStart.y, selectionBoxEnd.y);
        float width = Mathf.Abs(selectionBoxStart.x - selectionBoxEnd.x);
        float height = Mathf.Abs(selectionBoxStart.y - selectionBoxEnd.y);

        Rect selectionBoxRect = new Rect(minX, minY, width, height);
        selectionBox.anchoredPosition = selectionBoxRect.center;
        selectionBox.sizeDelta = selectionBoxRect.size;
        selectionBox.gameObject.SetActive(true);
    }

    public void UpdatePanels(Player localPlayer)
    {
        List<Actor> selectedActorList = localPlayer.GetSelectedActorList();
        Actor selectedRelevantActor = localPlayer.GetSelectedRelevantActor();

        selectionDetails.UpdatePanel(selectedActorList);
        commandCard.UpdatePanel(selectedRelevantActor);
    }
}
