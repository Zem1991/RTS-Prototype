using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : AbstractSingleton<PlayerManager>
{
    [SerializeField] private List<Player> playerList;
    [SerializeField] private Player localPlayer;
    
    public Player GetLocalPlayer()
    {
        return localPlayer;
    }
    
    //public Actor GetLocalPlayerSelectionRelevantActor()
    //{
    //    Actor result = localPlayer.GetSelectionRelevantActor();
    //    return result;
    //}
}
