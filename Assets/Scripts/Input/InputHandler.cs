using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private KeyCode escapeMenu = KeyCode.Escape;

    [Header("Cursor control")]
    [SerializeField] private string cursorX = "Mouse X";
    [SerializeField] private string cursorY = "Mouse Y";
    [SerializeField] private KeyCode cursorSelection = KeyCode.Mouse0;
    [SerializeField] private KeyCode cursorDecision = KeyCode.Mouse1;

    [Header("Camera control")]
    //[SerializeField] private string cameraX = "Horizontal";
    //[SerializeField] private string cameraY = "Vertical";
    [SerializeField] private KeyCode cameraUp = KeyCode.W;
    [SerializeField] private KeyCode cameraLeft = KeyCode.A;
    [SerializeField] private KeyCode cameraDown = KeyCode.S;
    [SerializeField] private KeyCode cameraRight = KeyCode.D;

    [Header("Command card")]
    [SerializeField] private KeyCode command00 = KeyCode.Q;
    [SerializeField] private KeyCode command10 = KeyCode.W;
    [SerializeField] private KeyCode command20 = KeyCode.E;
    [SerializeField] private KeyCode command30 = KeyCode.R;
    [SerializeField] private KeyCode command01 = KeyCode.A;
    [SerializeField] private KeyCode command11 = KeyCode.S;
    [SerializeField] private KeyCode command21 = KeyCode.D;
    [SerializeField] private KeyCode command31 = KeyCode.F;
    [SerializeField] private KeyCode command02 = KeyCode.Z;
    [SerializeField] private KeyCode command12 = KeyCode.X;
    [SerializeField] private KeyCode command22 = KeyCode.C;
    [SerializeField] private KeyCode command32 = KeyCode.V;

    #region General
    public bool EscapeMenu() { return Input.GetKeyDown(escapeMenu); }
    #endregion

    #region Cursor
    public Vector2 CursorMovement()
    {
        Vector3 result = new Vector3();
        result.x = Input.GetAxis(cursorX);
        result.y = Input.GetAxis(cursorY);
        //if (normalize) result.Normalize();
        return result;
    }

    public bool CursorSelection() { return Input.GetKey(cursorSelection); }
    public bool CursorSelectionDown() { return Input.GetKeyDown(cursorSelection); }
    public bool CursorSelectionUp() { return Input.GetKeyUp(cursorSelection); }
    public bool IsSelecting() { return CursorSelection() || CursorSelectionDown() || CursorSelectionUp(); }

    //public bool CursorDecision() { return Input.GetKey(cursorDecision); }
    public bool CursorDecisionDown() { return Input.GetKeyDown(cursorDecision); }
    //public bool CursorDecisionUp() { return Input.GetKeyUp(cursorDecision); }
    #endregion

    #region Camera
    public Vector2 CameraMovement()
    {
        Vector2 result = new Vector3();

        Vector2 axes = new Vector3();
        //axes.x = Input.GetAxis(cameraX);
        //axes.y = Input.GetAxis(cameraY);
        result += axes;

        Vector2 wasd = new Vector3();
        if (Input.GetKey(cameraLeft)) wasd.x--;
        if (Input.GetKey(cameraRight)) wasd.x++;
        if (Input.GetKey(cameraDown)) wasd.y--;
        if (Input.GetKey(cameraUp)) wasd.y++;
        result += wasd;

        //This one is now calculated in CursorHandler.
        //Vector2 edges = CheckCursorOverEdges();
        //result += edges;

        //if (normalize) result.Normalize();
        return result;
    }
    #endregion

    #region Command
    public CommandCardButton CommandCard()
    {
        CommandCardButton result = CommandCardButton.NONE;
        if (Input.GetKeyDown(command00)) result = CommandCardButton.BTN_00;
        if (Input.GetKeyDown(command10)) result = CommandCardButton.BTN_10;
        if (Input.GetKeyDown(command20)) result = CommandCardButton.BTN_20;
        if (Input.GetKeyDown(command30)) result = CommandCardButton.BTN_30;
        if (Input.GetKeyDown(command01)) result = CommandCardButton.BTN_01;
        if (Input.GetKeyDown(command11)) result = CommandCardButton.BTN_11;
        if (Input.GetKeyDown(command21)) result = CommandCardButton.BTN_21;
        if (Input.GetKeyDown(command31)) result = CommandCardButton.BTN_31;
        if (Input.GetKeyDown(command02)) result = CommandCardButton.BTN_02;
        if (Input.GetKeyDown(command12)) result = CommandCardButton.BTN_12;
        if (Input.GetKeyDown(command22)) result = CommandCardButton.BTN_22;
        if (Input.GetKeyDown(command32)) result = CommandCardButton.BTN_32;
        return result;
    }
    #endregion
}
