using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Action : MonoBehaviour
{
    [Header("Costs")]
    [SerializeField] private float timeCost;
    [SerializeField] private int goldCost;
    [SerializeField] private int crystalCost;
    [SerializeField] private int supplyCost;
    [SerializeField] private int healthCost;
    [SerializeField] private int manaCost;
}
