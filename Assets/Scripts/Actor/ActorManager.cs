using System.Collections;
using System.Collections.Generic;
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

    public List<Actor> GetActors(Vector2 selectionStart, Vector2 selectionEnd, Player owner) { return actorHandler.GetActors(selectionStart, selectionEnd, owner); }

    public void SetSelection(List<Actor> currentSelection)
    {
        PlayerManager pm = PlayerManager.Instance;
        Player localPlayer = pm.GetLocalPlayer();
        localPlayer.SetSelection(currentSelection);
        selectionHandler.SetSelection(currentSelection);
    }
}
