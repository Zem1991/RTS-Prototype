using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandCard : MonoBehaviour
{
    [SerializeField] private Action stop;
    [SerializeField] private Action move;
    [SerializeField] private Action defend;
    [SerializeField] private Action attack;
    //[SerializeField] private Action cancel;

    public Action GetStop() { return stop; }
    public Action GetMove() { return move; }
    public Action GetDefend() { return defend; }
    public Action GetAttack() { return attack; }
    //public Action GetCancel() { return cancel; }

    public bool HasAction(Action action)
    {
        if (stop == action) return true;
        if (move == action) return true;
        if (defend == action) return true;
        if (attack == action) return true;
        //TODO: add more?
        return false;
    }
}
