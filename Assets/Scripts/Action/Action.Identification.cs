using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Action : MonoBehaviour
{
    [Header("Identification")]
    [SerializeField] private string gameName;
    [SerializeField] private Sprite buttonImage;

    public string GetGameName() { return gameName; }
    public Sprite GetButtonImage() { return buttonImage; }
}
