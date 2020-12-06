using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Actor : MonoBehaviour
{
    [Header("Decision: TEMPORARY")]
    public float sightRange = 10F;

    [Header("Decision: Parameters")]
    [SerializeField] private float timeBetweenDecisions = 0.5F;
    [SerializeField] private float timeSinceLastDecision;
    [SerializeField] private CombatStance combatStance;
    [SerializeField] private List<Actor> hostiles = new List<Actor>();
    [SerializeField] private List<Actor> friendlies = new List<Actor>();
    [SerializeField] private Vector3 attackPosition;
    [SerializeField] private Vector3 defendPosition;
    [SerializeField] private Actor attackActor;
    [SerializeField] private Actor defendActor;

    public List<Actor> GetHostiles() { return hostiles; }
    public List<Actor> GetFriendlies() { return friendlies; }
    public Vector3 GetAttackPosition() { return attackPosition; }
    public Vector3 GetDefendPosition() { return defendPosition; }
    public Actor GetAttackActor() { return attackActor; }
    public Actor GetDefendActor() { return defendActor; }

    private void SetAttackPosition(Vector3 attackPos) { attackPosition = attackPos; }
    private void SetDefendPosition(Vector3 defendPos) { defendPosition = defendPos; }
    private void SetAttackActor(Actor attackObj) { attackActor = attackObj; }
    private void SetDefendActor(Actor defendObj) { defendActor = defendObj; }

    private void UpdateKnownActors()
    {
        friendlies = new List<Actor>();
        hostiles = new List<Actor>();

        List<Actor> allKnownPlayerObjects = ActorManager.Instance.GetActors(transform.position, sightRange);
        allKnownPlayerObjects = ActorManager.Instance.SortByDistance(allKnownPlayerObjects, transform.position);
        //TODO: also sort by priority? here or after the foreach?

        foreach (var item in allKnownPlayerObjects)
        {
            if (item.GetOwner() == GetOwner())
            {
                friendlies.Add(item);
            }
            else
            {
                //TODO: not checking for allied players!
                hostiles.Add(item);
            }
        }
    }

    private bool ShouldMakeDecision()
    {
        if (attackTarget)
        {
            timeSinceLastDecision = 0.0f;
            return false;
        }

        if (timeSinceLastDecision < timeBetweenDecisions)
        {
            timeSinceLastDecision += Time.deltaTime;
            return false;
        }

        timeSinceLastDecision = 0.0f;
        return true;
    }
    
    private void MakeDecision()
    {
        switch (combatStance)
        {
            case CombatStance.NO_COMBAT:
                //Won't do anything if its currently in passive (noncombatant) stance.
                //(at least for now...)
                //TODO: retreat if attacked
                break;
            case CombatStance.DEFENSIVE:
                //if (!defendObj && friendlyPlayerObjects.Count > 0)
                //    defendObj = friendlyPlayerObjects[0];
                //InstantiateAction_Defend(defendPos, defendObj);
                break;
            case CombatStance.OFFENSIVE:
                if (!attackActor && hostiles.Count > 0)
                {
                    attackActor = hostiles[0];
                    SetAttackTarget(attackActor);
                }
                break;
        }
    }
}
