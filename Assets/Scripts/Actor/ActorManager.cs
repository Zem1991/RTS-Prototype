using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActorManager : AbstractSingleton<ActorManager>
{
    [Header("Self references")]
    [SerializeField] private ActorHandler actorHandler;
    [SerializeField] private SelectionHandler selectionHandler;
    
    public override void Awake()
    {
        base.Awake();

        //TODO: have this happen when the level map is loaded
        //and add them with transform.parent = actorWrapper
        Actor[] actorArray = FindObjectsOfType<Actor>();
        List<Actor> actorList = new List<Actor>(actorArray);
        actorHandler.AddActors(actorList);
    }
    
    //public void ExecuteAction(Action action, Actor actor, Vector3 targetPosition, Actor targetActor)
    //{
    //    actorHandler.ExecuteAction(action, actor, targetPosition, targetActor);
    //}

    public List<Actor> GetActors(Vector2 selectionStart, Vector2 selectionEnd) { return actorHandler.GetActors(selectionStart, selectionEnd); }

    public List<Actor> GetActors(Vector2 position, float radius) { return actorHandler.GetActors(position, radius); }

    public void SetSelection(List<Actor> currentSelection)
    {
        PlayerManager pm = PlayerManager.Instance;
        Player localPlayer = pm.GetLocalPlayer();
        localPlayer.SetSelection(currentSelection);
        selectionHandler.SetSelection(currentSelection);
    }
    
    public List<Actor> SortByDistance(List<Actor> actors, Vector3 position)
    {
        return actors.OrderBy(x => Vector2.Distance(position, x.transform.position)).ToList();
    }
}
