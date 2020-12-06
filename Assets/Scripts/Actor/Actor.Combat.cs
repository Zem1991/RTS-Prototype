using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Actor : MonoBehaviour
{
    [Header("Combat: Parameters")]
    [SerializeField] private float attackRange = 5F;
    [SerializeField] private float attackDelay = 8F;
    [SerializeField] private float attackDelayCurrent;
    [SerializeField] private Actor attackTarget;

    public void SetAttackTarget(Actor target)
    {
        attackTarget = target;
    }

    public bool MakeAttack()
    {
        if (attackDelayCurrent > 0) attackDelayCurrent -= Time.deltaTime;

        //TODO: make modifications to support two different attack types on each Actor.
        if (!attackTarget) return false;

        Vector3 targetPosition = attackTarget.transform.position;
        float distance = Vector3.Distance(transform.position, targetPosition);
        if (distance > attackRange)
        {
            //WarnFriendlies_EngagingTheEnemy(attackTarget);
            SetDestination(targetPosition, attackTarget, attackRange);
        }
        else
        {
            //WarnFriendlies_AttackingTheEnemy(attackTarget);
            if (attackDelayCurrent <= 0)
            {
                attackDelayCurrent = attackDelay;
                //target.ChangeHP(Mathf.RoundToInt(atkDamage), false);
                Debug.Log("an attack was made - pew! pew!");
            }
        }
        return true;
    }
}
