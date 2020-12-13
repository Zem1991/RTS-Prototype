using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public partial class Actor : MonoBehaviour
{
    [Header("Base: Self references")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private NavMeshAgent navMeshAgent;
    //[SerializeField] private Collider2D collider;   //Obsolete. Use GetComponent<Collider>() instead
    
    [Header("Base: Identification")]
    [SerializeField] private string gameName;
    [SerializeField] private Player owner;

    [Header("Base: Graphics")]
    [SerializeField] private Sprite portraitImage;
    [SerializeField] private Sprite buttonImage;

    //[Header("Selection")]
    //[SerializeField] private SelectionIndicator selectionIndicator;

    public string GetGameName() { return gameName; }
    public Player GetOwner() { return owner; }

    public Sprite GetPortraitImage() { return portraitImage; }
    public Sprite GetButtonImage() { return buttonImage; }

    //public SelectionIndicator SelectionIndicator { get { return selectionIndicator; } set { selectionIndicator = value; } }

    private void Start()
    {
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
    }

    private void Update()
    {
        UpdateKnownActors();

        if (!ExecuteActions())
        {
            if (ShouldMakeDecision()) MakeDecision();
        }

        MakeAttack();
    }
}
