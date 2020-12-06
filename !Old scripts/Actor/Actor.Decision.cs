using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Actor : MonoBehaviour
{
    [Header("TEMPORARY")]
    public float sightRange = 10F;
    public float atkRange = 8F;

    [Header("Decision making")]
    [SerializeField] private float timeBetweenDecisions = 0.5F;
    [SerializeField] private float timeSinceLastDecision;
    [SerializeField] private CombatStance combatStance;
    //[SerializeField] private List<Actor> allKnownActors = new List<Actor>();
    [SerializeField] private List<Actor> friendlies = new List<Actor>();
    [SerializeField] private List<Actor> hostiles = new List<Actor>();
    [SerializeField] private Vector3 attackPos;
    [SerializeField] private Vector3 defendPos;
    [SerializeField] private Actor attackObj;
    [SerializeField] private Actor defendObj;

    public List<Actor> GetFriendlies() { return friendlies; }
    public List<Actor> GetHostiles() { return hostiles; }
    public Vector3 GetAttackPos() { return attackPos; }
    public Vector3 GetDefendPos() { return defendPos; }
    public Actor GetAttackObj() { return attackObj; }
    public Actor GetDefendObj() { return defendObj; }

    public void SetAttackPos(Vector3 attackPos) { this.attackPos = attackPos; }
    public void SetDefendPos(Vector3 defendPos) { this.defendPos = defendPos; }
    public void SetAttackObj(Actor attackObj) { this.attackObj = attackObj; }
    public void SetDefendObj(Actor defendObj) { this.defendObj = defendObj; }

    private bool ShouldMakeDecision()
    {
        //if (!attacking && !movingIntoPosition && !aiming)
        if (actionCommandQueue.Count == 0 && !actionCommandCurrent)
        {
            //We are not doing anything at the moment
            if (timeSinceLastDecision >= timeBetweenDecisions)
            {
                timeSinceLastDecision = 0.0f;
                return true;
            }
            timeSinceLastDecision += Time.deltaTime;
        }
        return false;
    }

    private void UpdateKnownActors()
    {
        List<Actor> allKnownActors = ActorManager.Instance.GetActors(transform.position, sightRange);
        friendlies = new List<Actor>();
        hostiles = new List<Actor>();

        foreach (var item in allKnownActors)
        {
            if (item.GetOwner() == GetOwner())
                friendlies.Add(item);
            else
                hostiles.Add(item);
        }
    }

    private void WarnFriendlies_EnemySighted(Actor enemy)
    {
        foreach (Actor item in friendlies)
            item.ReceiveWarning_EnemySighted(this, enemy);
    }

    private void WarnFriendlies_EnemyIsAttacking(Actor enemy)
    {
        foreach (Actor item in friendlies)
            item.ReceiveWarning_EnemyIsAttacking(this, enemy);
    }

    private void WarnFriendlies_AttackingTheEnemy(Actor enemy)
    {
        foreach (Actor item in friendlies)
            item.ReceiveWarning_AttackingTheEnemy(this, enemy);
    }

    public void ReceiveWarning_EnemySighted(Actor source, Actor enemy)
    {
        if ((combatStance == CombatStance.OFFENSIVE) && (attackObj == null))
            InstantiateAction_Attack(enemy.transform.position, enemy);
    }

    public void ReceiveWarning_EnemyIsAttacking(Actor source, Actor enemy)
    {
        if ((combatStance == CombatStance.DEFENSIVE) && (defendObj == source))
            InstantiateAction_Attack(enemy.transform.position, enemy);
        else if ((combatStance == CombatStance.OFFENSIVE) && (attackObj == null))
            InstantiateAction_Attack(enemy.transform.position, enemy);
    }

    public void ReceiveWarning_AttackingTheEnemy(Actor source, Actor enemy)
    {
        if ((combatStance == CombatStance.OFFENSIVE) && (attackObj == null))
            InstantiateAction_Attack(enemy.transform.position, enemy);
    }

    private void MakeDecision()
    {
        //Won't do anything if its currently in passive (noncombatant) stance.
        //(at least for now...)
        if (combatStance == CombatStance.NO_COMBAT)
            return;

        //if (!defendObj && friendlies.Count > 0)
        //    defendObj = friendlies[0];
        if (!attackObj && hostiles.Count > 0)
            attackObj = hostiles[0];

        switch (combatStance)
        {
            case CombatStance.DEFENSIVE:
                //InstantiateAction_Defend(defendPos, defendObj);
                break;
            case CombatStance.OFFENSIVE:
                InstantiateAction_Attack(attackPos, attackObj);
                break;
        }
    }
    private bool InstantiateAction_Move(Vector3 movePos, Actor moveObj)
    {
        Action action = commandCard.GetMove();
        if (action)
        {
            List<Actor> actorList = new List<Actor> { this };
            ActorManager.Instance.ExecuteAction(actorList, action, attackPos, attackObj);
            return true;
        }
        return false;
    }


    //private bool InstantiateAction_Defend(Vector3 defendPos, Actor defendObj)
    //{
    //    Action action = null;
    //    if (allActions[(int)ActionCommandType.DEFEND])
    //    {
    //        action = Instantiate(allActions[(int)ActionCommandType.DEFEND]);
    //        action.SetSourceAndTarget(action, this, defendPos, defendObj, null);
    //        action.transform.parent = transform;
    //    }
    //    if (action)
    //    {
    //        actionCommandQueue.Add(action);
    //        return true;
    //    }
    //    return false;
    //}

    private bool InstantiateAction_Attack(Vector3 attackPos, Actor attackObj)
    {
        Action action = commandCard.GetAttack();
        if (action)
        {
            List<Actor> actorList = new List<Actor> { this };
            ActorManager.Instance.ExecuteAction(actorList, action, attackPos, attackObj);
            return true;
        }
        return false;
    }

    public bool MoveToAttack(Actor target)
    {
        if (Vector3.Distance(transform.position, target.transform.position) > atkRange)
        {
            //TODO bool retreat should be TRUE when minimum attack range is not okay!
            UpdateDestinationPosition(target.transform.position, target, atkRange, false);
            return true;
        }
        else
        {
            return MakeAttack(target);
        }
    }

    public bool MakeAttack(Actor target)
    {
        if (!target)
            return false;

        if (Vector3.Distance(transform.position, target.transform.position) > atkRange)
            return false;

        //TODO ACTUAL ATTACK WITH ANIMATIONS, PROJECTILES AND SHIT.
        WarnFriendlies_AttackingTheEnemy(target);

        //if (atkWaitTime <= 0)
        //{
        //    atkWaitTime = atkSpeed;
        //    target.ChangeHP(Mathf.RoundToInt(atkDamage), false);
        //    Debug.Log("an attack was made - pew! pew!");
        //}
        return true;
    }
}
