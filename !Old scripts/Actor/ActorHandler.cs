using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorHandler : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private ActionCommand prefabActionCommand;

    [Header("Actors")]
    [SerializeField] private Transform actorWrapper;
    [SerializeField] private List<Actor> actorList = new List<Actor>();

    public void ExecuteAction(List<Actor> actorList, Action action, Vector3 targetPosition, Actor targetActor)
    {
        foreach (Actor forActor in actorList)
        {
            ActionCommand newAC = Instantiate(prefabActionCommand, forActor.transform);
            newAC.Initialize(forActor, action, targetPosition, targetActor);
            forActor.SetActionCommand(newAC);
        }
    }

    //TODO: this, when its necessary
    //public void RemoveActors(List<Actor> actors)
    //{
    //    actorList.remo(actors);
    //}

    public List<Actor> GetActors(Vector2 selectionStart, Vector2 selectionEnd)
    {
        float posX = (selectionStart.x + selectionEnd.x) / 2F;
        float posY = (selectionStart.y + selectionEnd.y) / 2F;
        float width = Mathf.Abs(selectionStart.x - selectionEnd.x);
        float height = Mathf.Abs(selectionStart.y - selectionEnd.y);
        Vector2 point = new Vector2(posX, posY);
        Vector2 size = new Vector2(width, height);

        Collider2D[] colliders = Physics2D.OverlapBoxAll(point, size, 0);
        List<Actor> result = new List<Actor>();
        if (colliders.Length > 0)
        {
            foreach (Collider2D forCollider in colliders)
            {
                Actor forActor = forCollider.GetComponent<Actor>();
                if (forActor) result.Add(forActor);
            }
        }
        return result;
    }

    public List<Actor> GetActors(Vector2 position, float radius)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, radius);
        List<Actor> result = new List<Actor>();
        if (colliders.Length > 0)
        {
            foreach (Collider2D forCollider in colliders)
            {
                Actor forActor = forCollider.GetComponent<Actor>();
                result.Add(forActor);
            }
        }
        return result;
    }

    public void AddActors(List<Actor> actors)
    {
        //TODO: maybe add some checks here?
        actorList.AddRange(actors);
    }
}
