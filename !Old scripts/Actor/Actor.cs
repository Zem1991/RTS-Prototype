using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public partial class Actor : MonoBehaviour
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
    [SerializeField] private Queue<ActionCommand> actionCommandQueue = new Queue<ActionCommand>();
    [SerializeField] private ActionCommand actionCommandCurrent;

    //[Header("Selection")]
    //[SerializeField] private SelectionIndicator selectionIndicator;

    private void Start()
    {
        //TODO: THIS SHOULD NOT BE NECESSARY
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void Update()
    {
        PerformAction();
    }
    
    private void PerformAction()
    {
        if (!actionCommandCurrent && actionCommandQueue.Count > 0)
        {
            actionCommandCurrent = actionCommandQueue.Dequeue();
        }
        
        if (actionCommandCurrent)
        {
            throw new NotImplementedException();
            //TODO: THIS, NOW!
        }
    }
    
    public string GetGameName() { return gameName; }
    public Player GetOwner() { return owner; }

    public Sprite GetPortraitImage() { return portraitImage; }
    public Sprite GetButtonImage() { return buttonImage; }

    public CommandCard GetCommandCard() { return commandCard; }
    public Queue<ActionCommand> GetActionCommandQueue() { return actionCommandQueue; }
    public ActionCommand GetActionCommandCurrent() { return actionCommandCurrent; }

    //public SelectionIndicator SelectionIndicator { get { return selectionIndicator; } set { selectionIndicator = value; } }

    public void ClearActionCommands()
    {
        foreach (ActionCommand forAC in actionCommandQueue)
        {
            Destroy(forAC.gameObject);
        }
        actionCommandQueue.Clear();
        //TODO: cancel current action?
    }

    public void SetActionCommand(ActionCommand actionCommand)
    {
        ClearActionCommands();
        actionCommandCurrent = actionCommand;
    }

    //public void AddActionCommand(ActionCommand actionCommand)
    //{
    //    actionCommandQueue.Enqueue(actionCommand);
    //}
}
