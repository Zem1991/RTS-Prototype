using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionCommand : MonoBehaviour
{
    [SerializeField] private Action action;
    //[SerializeField] private Actor caster;
    [SerializeField] private Vector3 targetPoint;
    [SerializeField] private Actor targetActor;
}
