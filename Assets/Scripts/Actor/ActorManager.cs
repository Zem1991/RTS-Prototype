using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager : MonoBehaviour
{
    [Header("Lists")]
    [SerializeField] private List<Actor> actorList = new List<Actor>();

    private void Awake()
    {
        //TODO: have this happen when the level map is loaded
        Actor[] actorArray = FindObjectsOfType<Actor>();
        actorList.AddRange(actorArray);
    }
}
