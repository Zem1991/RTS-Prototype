using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    [Header("Self references")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    //[SerializeField] private Collider2D collider;   //Obsolete. Use GetComponent<Collider>() instead

    [Header("Identification")]
    [SerializeField] private string gameName;
    [SerializeField] private Player owner;

    [Header("Graphics")]
    [SerializeField] private Sprite portraitImage;
    [SerializeField] private Sprite buttonImage;

    [Header("Actions")]
    [SerializeField] private CommandCard commandCard;
    [SerializeField] private Queue<ActionCommand> actionCommandQueue;
    [SerializeField] private ActionCommand actionCommandCurrent;

    public string GetGameName() { return gameName; }
    public Player GetOwner() { return owner; }
    public Sprite GetPortraitImage() { return portraitImage; }
    public Sprite GetButtonImage() { return buttonImage; }
    public CommandCard GetCommandCard() { return commandCard; }
}
