using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Identification")]
    [SerializeField] private string playerName;
    [SerializeField] private Color playerColor;

    [Header("Selection")]
    [SerializeField] private List<Actor> selection;
    [SerializeField] private Actor selectionRelevantActor;

    public List<Actor> GetSelection() { return selection; }
    public Actor GetSelectionRelevantActor() { return selectionRelevantActor; }

    public void ClearSelection()
    {
        selection.Clear();
        selectionRelevantActor = null;
    }

    public void SetSelection(List<Actor> selection)
    {
        ClearSelection();
        if (selection.Count > 0)
        {
            //TODO: prioritize by being closer to the 'selectionStart' coordinates
            //TODO: do some form of sorting by unit tier/rank here, before setting the relevant one
            this.selection = selection;
            selectionRelevantActor = selection[0];
        }
    }
}
