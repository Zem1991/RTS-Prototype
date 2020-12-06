using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Actor : MonoBehaviour
{
    [Header("Health: Parameters")]
    [SerializeField] private int health = 100;
    [SerializeField] private int healthCurrent = 100;

    public bool IsDead()
    {
        return healthCurrent <= 0;
    }
}
