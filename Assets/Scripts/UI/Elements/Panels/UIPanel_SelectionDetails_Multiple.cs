using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanel_SelectionDetails_Multiple : MonoBehaviour
{
    [Header("Selection buttons")]
    [SerializeField] private List<UIButton_MultipleSelection> selectionButtonList;
    
    private void Awake()
    {
        //TODO: have this happen when the level map is loaded
        UIButton_MultipleSelection[] selectionButtonArray = GetComponentsInChildren<UIButton_MultipleSelection>();
        selectionButtonList.AddRange(selectionButtonArray);
    }

    public void UpdatePanel(List<Actor> selectedActorList)
    {
        for (int index = 0; index < selectionButtonList.Count; index++)
        {
            UIButton_MultipleSelection forBtn = selectionButtonList[index];
            if (index < selectedActorList.Count)
            {
                Actor forActor = selectedActorList[index];
                forBtn.SetActor(forActor);
            }
            else
            {
                forBtn.SetActor(null);
            }
        }
    }
}
