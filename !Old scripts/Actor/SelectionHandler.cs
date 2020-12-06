using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionHandler : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private SelectionIndicator smallUnitSIPrefab;

    [Header("Local player's selection")]
    //[SerializeField] private Dictionary<Actor, SelectionIndicator> selectionIndicatorMap = new Dictionary<Actor, SelectionIndicator>();
    [SerializeField] private List<SelectionIndicator> selectionIndicatorList = new List<SelectionIndicator>();

    private void ClearSelection()
    {
        foreach (SelectionIndicator forSI in selectionIndicatorList)
        {
            if (forSI) Destroy(forSI.gameObject);
        }
        selectionIndicatorList.Clear();
    }

    public void SetSelection(List<Actor> currentSelection)
    {
        ClearSelection();
        foreach (Actor forActor in currentSelection)
        {
            SelectionIndicator newSI = Instantiate(smallUnitSIPrefab, forActor.transform);
            //TODO: set size and stuff related to the actor
            selectionIndicatorList.Add(newSI);
        }
    }
}
