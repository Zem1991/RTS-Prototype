using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanel_SelectionDetails : UIPanel
{
    [Header("Modes")]
    [SerializeField] private UIPanel_SelectionDetails_Single single;
    [SerializeField] private UIPanel_SelectionDetails_Multiple multiple;
    
    public void UpdatePanel(List<Actor> selectedActorList)
    {
        if (selectedActorList.Count <= 0)
        {
            single.gameObject.SetActive(false);
            multiple.gameObject.SetActive(false);
        }
        else if (selectedActorList.Count == 1)
        {
            multiple.gameObject.SetActive(false);
            single.UpdatePanel(selectedActorList[0]);
            single.gameObject.SetActive(true);
        }
        else
        {
            single.gameObject.SetActive(false);
            multiple.UpdatePanel(selectedActorList);
            multiple.gameObject.SetActive(true);
        }
    }
}
