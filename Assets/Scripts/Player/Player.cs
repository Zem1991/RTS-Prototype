using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Identification")]
    [SerializeField] private string playerName;
    [SerializeField] private Color playerColor;

    [Header("Selection")]
    [SerializeField] private List<Actor> selectedActorList;
    [SerializeField] private Actor selectedRelevantActor;

    public void SetSelection(Vector2 selectionStart, Vector2 selectionEnd)
    {
        float posX = (selectionStart.x + selectionEnd.x) / 2F;
        float posY = (selectionStart.y + selectionEnd.y) / 2F;
        float width = Mathf.Abs(selectionStart.x - selectionEnd.x);
        float height = Mathf.Abs(selectionStart.y - selectionEnd.y);
        Vector2 point = new Vector2(posX, posY);
        Vector2 size = new Vector2(width, height);
        
        Collider2D[] colliders = Physics2D.OverlapBoxAll(point, size, 0);
        if (colliders.Length > 0)
        {
            List<Actor> tempActorList = new List<Actor>();
            foreach (Collider2D forCollider in colliders)
            {
                Actor forActor = forCollider.GetComponent<Actor>();
                if (forActor && forActor.GetOwner() == this) tempActorList.Add(forActor);
            }

            if (tempActorList.Count > 0)
            {
                //TODO: prioritize by being closer to the 'selectionStart' coordinates
                //TODO: do some form of sorting by unit tier/rank here, before setting the relevant one
                selectedActorList = tempActorList;
                selectedRelevantActor = selectedActorList[0];
            }
            else
            {
                ClearSelection();
            }
        }
        else
        {
            ClearSelection();
        }
    }

    public void ClearSelection()
    {
        selectedActorList.Clear();
        selectedRelevantActor = null;
    }

    public List<Actor> GetSelectedActorList() { return selectedActorList; }
    public Actor GetSelectedRelevantActor() { return selectedRelevantActor; }
}
